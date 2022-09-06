using System.Net;
using System.Numerics;

namespace IPAddressUtilities
{
    public static class IPUtilities
    {
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
        private static BigInteger CompareIPAddresses(ExtendedIPAddress firstAddress, ExtendedIPAddress secondAddress)
        {
            BigInteger difference;

            BigInteger firstAddressBits = ConvertIPAddressBits(firstAddress);
            BigInteger secondAddressBits = ConvertIPAddressBits(secondAddress);

            difference = secondAddressBits - firstAddressBits;

            return difference;
        }

        public static List<IPAddress> CalculateIPRange(ExtendedIPAddress start, ExtendedIPAddress end)
        {
            List<IPAddress> targetAddresses = new List<IPAddress>();
            BigInteger addressCount;
            addressCount = IPUtilities.CompareIPAddresses(start, end) + 1;
            ExtendedIPAddress currentAddress = start;
            for (int i = 0; i < addressCount; i++)
            {
                targetAddresses.Add(currentAddress);
                currentAddress++;
            }
            return targetAddresses;
        }
    }
}