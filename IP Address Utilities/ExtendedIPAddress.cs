using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IPScanApp
{
    public class ExtendedIPAddress : IPAddress
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
            BigInteger difference;

            difference = CompareIPAddresses(a, b);

            return (difference > 0);
        }

        public static bool operator >=(ExtendedIPAddress a, ExtendedIPAddress b)
        {
            BigInteger difference;

            difference = CompareIPAddresses(a, b);

            return (difference >=  0);
        }

        public static bool operator <(ExtendedIPAddress a, ExtendedIPAddress b)
        {
            BigInteger difference;

            difference = CompareIPAddresses(a, b);

            return (difference < 0);
        }


        public static bool operator <=(ExtendedIPAddress a, ExtendedIPAddress b)
        {
            BigInteger difference;

            difference = CompareIPAddresses(a, b);

            return (difference <= 0);
        }


        public static ExtendedIPAddress operator ++(ExtendedIPAddress a)
        {
            return IncrementIPAddress(a, 1);
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

        /// <summary>
        /// Increments provided IP address to the next address
        /// </summary>
        /// <param name="address">IP Address to be incremented</param>
        /// <returns></returns>
        private static ExtendedIPAddress IncrementIPAddress(ExtendedIPAddress address, BigInteger incrementAmount)
        {
            int tetCount = address.GetAddressBytes().Count();
            ExtendedIPAddress incrementedAddress;

            BigInteger addressInt = ConvertIPAddressBits(address);
            addressInt += incrementAmount;
            incrementedAddress = ConvertBitsToAddress(addressInt, tetCount);
            return incrementedAddress;
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
    }
}
