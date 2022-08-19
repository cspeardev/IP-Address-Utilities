using IPAddressUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_Address_Utilities_Tests
{
    internal class ExtendedIpAddressTestData
    {
        public static IEnumerable<object[]> NotEqualTestData
        {
            get
            {
                yield return new object[] { new ExtendedIPAddress("10.0.0.1"), new ExtendedIPAddress("10.0.0.2") };
                yield return new object[] { new ExtendedIPAddress("192.168.0.1"), new ExtendedIPAddress("10.0.0.2") };
                yield return new object[] { new ExtendedIPAddress("10.0.0.1"), new ExtendedIPAddress("ae63:2234:5f92:d24:bfa8:4ae7:1559:cf8f") };
                yield return new object[] { new ExtendedIPAddress("dbd9:7e0a:5fb5:71b4:4bfd:76cd:552f:212b"), new ExtendedIPAddress("10.0.0.2") };
            }
        }

        public static IEnumerable<object[]> IncrementTestData
        {
            get
            {
                yield return new object[] { new ExtendedIPAddress("10.0.0.2"), new ExtendedIPAddress("10.0.0.1") };
                //yield return new object[] { new ExtendedIPAddress("192.168.0.1"), new ExtendedIPAddress("10.0.0.2") };
                //yield return new object[] { new ExtendedIPAddress("10.0.0.1"), new ExtendedIPAddress("ae63:2234:5f92:d24:bfa8:4ae7:1559:cf8f") };
                //yield return new object[] { new ExtendedIPAddress("dbd9:7e0a:5fb5:71b4:4bfd:76cd:552f:212b"), new ExtendedIPAddress("10.0.0.2") };
            }
        }
    }
}
