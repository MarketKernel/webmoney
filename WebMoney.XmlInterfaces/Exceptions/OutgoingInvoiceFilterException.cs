﻿using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Exceptions
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable, ComVisible(true)]
    public class OutgoingInvoiceFilterException : WmException
    {
        public OutgoingInvoiceFilterException(string message)
            : base(message)
        {
        }

        public OutgoingInvoiceFilterException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public OutgoingInvoiceFilterException(int errorNumber, string message)
            : base(errorNumber, message)
        {
        }

        public OutgoingInvoiceFilterException(int errorNumber, string message, Exception innerException)
            : base(errorNumber, message, innerException)
        {
        }

        protected OutgoingInvoiceFilterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            base.GetObjectData(info, context);
        }

        public override string TranslateDescription(Language language)
        {
            return LocalizationUtility.GetErrorDescription("X4", ErrorNumber, language);
        }
    }
}
