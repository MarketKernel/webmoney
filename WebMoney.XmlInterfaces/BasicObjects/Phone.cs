using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WebMoney.XmlInterfaces.BasicObjects
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    public struct Phone : IXmlSerializable
    {
        private const string PhonePattern = @"^\+(?<countryCode>\d+)(?<localNumber>\d{10})$";

        private uint _countryCode;
        private ulong _localNumber;
        
        public string AccessKey => ToString();

        Phone(ulong number)
        {
            var countryCode = (uint) (number/10000000000U);
            ulong localNumber = number - (countryCode*10000000000U);

            if (countryCode > 999)
                throw new ArgumentOutOfRangeException(nameof(number));

            if (localNumber > 9999999999U)
                throw new ArgumentOutOfRangeException(nameof(number));

            _countryCode = countryCode;
            _localNumber = localNumber;
        }

        Phone(uint countryCode, ulong localNumber)
        {
            if (countryCode > 999)
                throw new ArgumentOutOfRangeException(nameof(countryCode));

            if (localNumber > 9999999999U)
                throw new ArgumentOutOfRangeException(nameof(localNumber));

            _countryCode = countryCode;
            _localNumber = localNumber;
        }

        public static explicit operator Phone(ulong value)
        {
            return new Phone(value);
        }

        public static implicit operator ulong(Phone value)
        {
            return (value._countryCode * 10000000000U) + value._localNumber;
        }

        public static bool operator ==(Phone lhs, Phone rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Phone lhs, Phone rhs)
        {
            return !(lhs == rhs);
        }

        public static Phone Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            Phone phone;

            if (!TryParse(value, out phone))
                throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The string '{0}' is not a valid Phone value.", value));

            return phone;
        }

        public static bool TryParse(string value, out Phone phone)
        {
            if (string.IsNullOrEmpty(value))
            {
                phone = default(Phone);
                return false;
            }

            var match = Regex.Match(value, PhonePattern);

            if (match.Value != value || !match.Groups["countryCode"].Success || !match.Groups["localNumber"].Success)
            {
                phone = default(Phone);
                return false;
            }

            uint countryCode = uint.Parse(match.Groups["countryCode"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat);
            ulong localNumber = ulong.Parse(match.Groups["localNumber"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat);

            if (countryCode > 999 || localNumber > 9999999999U)
            {
                phone = default(Phone);
                return false;
            }

            phone = new Phone(countryCode, localNumber);
            return true;
        }

        public bool VerifyAccessKey(string accessKey)
        {
            Phone otherPhone;

            if (!TryParse(accessKey, out otherPhone))
                return false;

            return Equals(otherPhone);
        }

        public string ToFriendlyString()
        {
            ulong areaCode = _localNumber/10000000L;
            ulong group1 = (_localNumber - areaCode*10000000L)/10000L;
            ulong group2 = (_localNumber - areaCode*10000000L - group1*10000L)/100L;
            ulong group3 = (_localNumber - areaCode*10000000L - group1*10000L - group2*100L);

            return string.Format(CultureInfo.InvariantCulture, "{0:000}{1:000}{2:00}{3:00}", areaCode, group1,
                                 group2, group3);
        }

        public string ToLocalString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:0000000000}", _localNumber);
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "+{0}{1:0000000000}", _countryCode, _localNumber);
        }

        public bool Equals(Phone other)
        {
            return other._countryCode == _countryCode && other._localNumber == _localNumber;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (obj.GetType() != typeof(Phone))
                return false;

            return Equals((Phone)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_countryCode.GetHashCode() * 397) ^ _localNumber.GetHashCode();
            }
        }

        #region IXmlSerializable Members

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            Phone phone = Parse(reader.ReadElementContentAsString());

            _countryCode = phone._countryCode;
            _localNumber = phone._localNumber;
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(ToString());
        }

        #endregion
    }
}