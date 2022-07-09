using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;

namespace IPScanApp
{
    public static class IPUtilities
    {
        /// <summary>
        /// Increments provided IP address to the next address
        /// </summary>
        /// <param name="address">IP Address to be incremented</param>
        /// <returns></returns>
        public static IPAddress IncrementIPAddress(IPAddress address, BigInteger incrementAmount)
        {
            int tetCount = address.GetAddressBytes().Count();
            IPAddress incrementedAddress;

            BigInteger addressInt = ConvertIPAddressBits(address);
            addressInt += incrementAmount;
            incrementedAddress = ConvertBitsToAddress(addressInt,tetCount);
            return incrementedAddress;
        }

        private static IPAddress ConvertBitsToAddress(BigInteger bits, int tetCount)
        {
            byte[] addressBytes = new byte[tetCount];
            for (int i = 0; i < tetCount; i++)
            {
                addressBytes[i] = (byte)((bits
                    >> (8 * (tetCount - (i + 1)))) & 0xFF);
            }
            return new IPAddress(addressBytes);
        }

        private static BigInteger ConvertIPAddressBits(IPAddress address)
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

        /// <summary>
        /// Compares two IP addressses, returns an integer of how many addresses are between them.
        /// </summary>
        /// <param name="firstAddress">First IP address to be compared</param>
        /// <param name="secondAddress">Second IP address to be compared</param>
        /// <returns></returns>
        public static BigInteger CompareIPAddresses(IPAddress firstAddress, IPAddress secondAddress)
        {
            BigInteger difference;

            BigInteger firstAddressBits = ConvertIPAddressBits(firstAddress);
            BigInteger secondAddressBits = ConvertIPAddressBits(secondAddress);

            difference = secondAddressBits - firstAddressBits;

            return difference;
        }

        public static List<IPAddress> CalculateIPRange(IPAddress start, IPAddress end)
        {
            List<IPAddress> targetAddresses = new List<IPAddress>();
            BigInteger addressCount;
            addressCount = IPUtilities.CompareIPAddresses(start, end) + 1;
            IPAddress currentAddress = start;
            for (int i = 0; i < addressCount; i++)
            {
                targetAddresses.Add(currentAddress);
                currentAddress = IPUtilities.IncrementIPAddress(currentAddress,1);
            }
            return targetAddresses;
        }
    }
}