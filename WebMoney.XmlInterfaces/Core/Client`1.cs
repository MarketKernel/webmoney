using System;
using System.IO;
using System.Net;

namespace WebMoney.XmlInterfaces.Core
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif

    public class Client<TResponse>
        where TResponse : Response, new()
    {
        private readonly Connection _connection;
        private byte _attemptsLimit;

        public byte AttemptsLimit
        {
            get { return _attemptsLimit; }
            set
            {
                if (value < 1 || value > 10)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _attemptsLimit = value;
            }
        }

        public bool ContinueOnServerError { get; set; }

        public Client(Connection connection = null)
        {
            if (null == connection)
                connection = new Connection();

            _connection = connection;
            _attemptsLimit = 3;
            ContinueOnServerError = false;
        }

        public TResponse Submit(Request<TResponse> request)
        {
            if (null == request)
                throw new ArgumentNullException(nameof(request));

            int attempt = 1;

            while (true)
            {
                try
                {
                    return InternalSubmit(request);
                }
                catch (WebException)
                {
                    if (attempt >= AttemptsLimit)
                        throw;

                    attempt++;
                }
            }
        }

        private TResponse InternalSubmit(Request<TResponse> request)
        {
            Connection connection = Connect(request);

            if (!request.IsEmpty)
            {
                using (Stream stream = connection.CaptureRequestStream())
                {
                    request.WriteContent(stream);
                }
            }

            Stream responseStream = null;

            var response = new TResponse();
            response.ApplyContext(request.Context);

            try
            {
                responseStream = connection.CaptureResponseStream();
                response.ReadContent(responseStream);
            }
            catch (WebException webException)
            {
                if (!ContinueOnServerError)
                    throw;

                responseStream = webException.Response.GetResponseStream();
                response.ReadContent(responseStream);
            }
            finally
            {
                responseStream?.Close();
            }

            return response;
        }

        protected virtual Connection Connect(Request<TResponse> request)
        {
            if (null == request)
                throw new ArgumentNullException(nameof(request));

            _connection.Proxy = request.Proxy;
            _connection.ContentType = request.ContentType;

            foreach (var requestHeader in request.Headers)
            {
                _connection.Headers.Add(requestHeader);
            }

            _connection.Connect(request.Url, request.Certificate);

            return _connection;
        }
    }
}