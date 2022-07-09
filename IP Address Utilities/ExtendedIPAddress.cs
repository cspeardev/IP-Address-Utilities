using System.Net;
using System.Numerics;

namespace IPScanApp
{
    public class ExtendedIPAddress : IPAddress, IEquatable<ExtendedIPAddress>, IComparable<ExtendedIPAddress>
    {
        #region constructors
        public ExtendedIPAddress(byte[] address) : base(address)
        {
        }

        public ExtendedIPAddress(byte[] address, long scopeid) : base(address, scopeid)
        {
        }
        public ExtendedIPAddress(long newAddress) : base(newAddress)
        {
        }
        #endregion constructors

        public static bool operator >(ExtendedIPAddress a, ExtendedIPAddress b)
        {
            return a.CompareTo(b) > 0;
        }

        public static bool operator >=(ExtendedIPAddress a, ExtendedIPAddress b)
        {
            return (a.CompareTo(b) > 0 || a.CompareTo(b) == 0);
        }

        public static bool operator <(ExtendedIPAddress a, ExtendedIPAddress b)
        {
            return (a.CompareTo(b) < 0);
        }

        public static bool operator <=(ExtendedIPAddress a, ExtendedIPAddress b)
        {
            return (a.CompareTo(b) < 0 || a.CompareTo(b) == 0);
        }

        public static ExtendedIPAddress operator ++(ExtendedIPAddress a)
        {
            int tetCount = a.GetAddressBytes().Count();
            ExtendedIPAddress incrementedAddress;

            BigInteger addressInt = ConvertIPAddressBits(a);
            addressInt++;
            incrementedAddress = ConvertBitsToAddress(addressInt, tetCount);
            return incrementedAddress;
        }

        private static ExtendedIPAddress ConvertBitsToAddress(BigInteger bits, int tetCount)
        {
            byte[] addressBytes = new byte[tetCount];
            for (int i = 0; i < tetCount; i++)
            {
                addressBytes[i] = (byte)((bits
                    >> (8 * (tetCount - (i + 1)))) & 0xFF);
            }
            return new ExtendedIPAddress(addressBytes);
        }

        private static BigInteger ConvertIPAddressBits(ExtendedIPAddress address)
        {
            int tetCount = address.GetAddressBytes().Count();
            int tetBits = address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork ? 8 : 16;
            int groupBytesSize = tetBits / 8;

            byte[] addressBytes = address.GetAddressBytes();

            BigInteger addressBits = 0;

            for (int i = 0; i < (tetCount / groupBytesSize); i++)
            {
                BigInteger groupBits = GetGroupBits(addressBytes, groupBytesSize, i);
                addressBits |= groupBits << (tetBits * ((tetCount / groupBytesSize) - (i + 1)));
            }
            return addressBits;
        }

        private static BigInteger GetGroupBits(byte[] addressBytes, int groupByteSize, int startIndex)
        {
            BigInteger groupBytes = 0;

            startIndex *= groupByteSize;

            for (int i = 0; i < groupByteSize; i++)
            {
                groupBytes |= addressBytes[((startIndex) + groupByteSize) - (i + 1)] << (8 * (i));
            }
            return groupBytes;
        }
        
        private static BigInteger CompareIPAddresses(ExtendedIPAddress firstAddress, ExtendedIPAddress secondAddress)
        {
            BigInteger difference;

            BigInteger firstAddressBits = ConvertIPAddressBits(firstAddress);
            BigInteger secondAddressBits = ConvertIPAddressBits(secondAddress);

            difference = firstAddressBits - secondAddressBits;

            return difference;
        }

        public bool Equals(ExtendedIPAddress? other)
        {
            if(other == null)
            {
                return false;
            }var comparison = CompareIPAddresses(this, other);
            return comparison == 0;
        }

        public int CompareTo(ExtendedIPAddress? other)
        {
            ArgumentNullException.ThrowIfNull(other);
            var comparison = CompareIPAddresses(this, other);
            if(comparison > 0)
            {
                return 1;
            }
            else if(comparison < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
