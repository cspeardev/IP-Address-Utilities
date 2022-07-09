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

        public ExtendedIPAddress(ReadOnlySpan<byte> address) : base(address)
        {
        }

        public ExtendedIPAddress(ReadOnlySpan<byte> address, long scopeid) : base(address, scopeid)
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
            int tetCount = a.GetAddressBytes().Length;
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
            int tetCount = address.GetAddressBytes().Length;
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
            if (other is null)
            {
                return false;
            }
            return CompareIPAddresses(this, other) == 0;
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

        public override bool Equals(object? comparand)
        {
            if (ReferenceEquals(this, comparand))
            {
                return true;
            }

            if (comparand is null)
            {
                return false;
            }

            if(comparand.GetType() != typeof(ExtendedIPAddress))
            {
                return false;
            }

            var other = (ExtendedIPAddress)comparand;

            return CompareTo(other) == 0;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(ExtendedIPAddress left, ExtendedIPAddress right)
        {
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }

        public static bool operator !=(ExtendedIPAddress left, ExtendedIPAddress right)
        {
            return !(left == right);
        }
    }
}
