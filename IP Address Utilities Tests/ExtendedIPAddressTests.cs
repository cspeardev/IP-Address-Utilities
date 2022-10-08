using IP_Address_Utilities_Tests;
using IPAddressUtilities;
using System.Net;
using Xunit;
using static Xunit.Assert;

namespace IPAddressUtilitiesTests
{

    public class ExtendedIPAddressTests
    {
        [Theory]
        [MemberData(nameof(ExtendedIpAddressTestData.NotEqualTestData), MemberType = typeof(ExtendedIpAddressTestData))]
        public void NotEqualTest(ExtendedIPAddress first, ExtendedIPAddress second)
        {
            True(first != second);
        }

        [Theory]
        [MemberData(nameof(ExtendedIpAddressTestData.EqualsTestData), MemberType = typeof(ExtendedIpAddressTestData))]
        public void EqualTest(ExtendedIPAddress first, ExtendedIPAddress second)
        {
            True(first == second);
            Equal(first.GetHashCode(), second.GetHashCode());
        }

        [Theory]
        [MemberData(nameof(ExtendedIpAddressTestData.IncrementTestData), MemberType = typeof(ExtendedIpAddressTestData))]
        public void IncrementTest(ExtendedIPAddress incremented, ExtendedIPAddress expected)
        {
            incremented++;
            Equal(incremented, expected);
        }

        [Theory]
        [MemberData(nameof(ExtendedIpAddressTestData.DecrementTestData), MemberType = typeof(ExtendedIpAddressTestData))]
        public void DecrementTest(ExtendedIPAddress decremented, ExtendedIPAddress expected)
        {
            decremented--;
            Equal(decremented, expected);
        }

        [Theory]
        [MemberData(nameof(ExtendedIpAddressTestData.GreaterThanTestData), MemberType = typeof(ExtendedIpAddressTestData))]
        public void GreaterThanTest(ExtendedIPAddress upper, ExtendedIPAddress lower)
        {
            True(upper > lower);
        }

        [Theory]
        [MemberData(nameof(ExtendedIpAddressTestData.LessThanTestData), MemberType = typeof(ExtendedIpAddressTestData))]
        public void LessThanTest(ExtendedIPAddress upper, ExtendedIPAddress lower)
        {
            True(lower < upper);
        }

        [Theory]
        [MemberData(nameof(ExtendedIpAddressTestData.GreaterThanTestData), MemberType = typeof(ExtendedIpAddressTestData))]
        [MemberData(nameof(ExtendedIpAddressTestData.EqualsTestData), MemberType = typeof(ExtendedIpAddressTestData))]
        public void GreaterThanOrEqualTest(ExtendedIPAddress upper, ExtendedIPAddress lower)
        {
            True(upper >= lower);
        }

        [Theory]
        [MemberData(nameof(ExtendedIpAddressTestData.LessThanTestData), MemberType = typeof(ExtendedIpAddressTestData))]
        [MemberData(nameof(ExtendedIpAddressTestData.EqualsTestData), MemberType = typeof(ExtendedIpAddressTestData))]
        public void LessThanOrEqualTest(ExtendedIPAddress upper, ExtendedIPAddress lower)
        {
            True(lower <= upper);
        }

        [Theory]
        [MemberData(nameof(ExtendedIpAddressTestData.ParseTestData), MemberType = typeof(ExtendedIpAddressTestData))]
        public void ParseTest(string toParse, ExtendedIPAddress expected)
        {
            ExtendedIPAddress address = ExtendedIPAddress.Parse(toParse);
            NotNull(address);
            Equal(expected, address);
        }
    }
}
