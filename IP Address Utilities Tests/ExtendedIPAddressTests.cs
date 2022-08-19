using IP_Address_Utilities_Tests;
using IPAddressUtilities;
using Xunit;
using static Xunit.Assert;

namespace IPAddressUtilitiesTests
{

    public class ExtendedIPAddressTests
    {
        [Theory]
        [MemberData(nameof(ExtendedIpAddressTestData.NotEqualTestData), MemberType = typeof(ExtendedIpAddressTestData))]
        public void NotEqualTest(ExtendedIPAddress upper, ExtendedIPAddress lower)
        {
            NotEqual(upper, lower);
            True(upper != lower);
        }

        [Theory]
        [MemberData(nameof(ExtendedIpAddressTestData.EqualsTestData), MemberType = typeof(ExtendedIpAddressTestData))]
        public void EqualTest(ExtendedIPAddress upper, ExtendedIPAddress lower)
        {
            Equal(upper, lower);
            True(upper == lower);
        }

        [Theory]
        [MemberData(nameof(ExtendedIpAddressTestData.IncrementTestData), MemberType = typeof(ExtendedIpAddressTestData))]
        public void IncrementTest(ExtendedIPAddress upper, ExtendedIPAddress lower)
        {
            lower++;
            Equal(upper, lower);
        }

        [Theory]
        [MemberData(nameof(ExtendedIpAddressTestData.DecrementTestData), MemberType = typeof(ExtendedIpAddressTestData))]
        public void DecrementTest(ExtendedIPAddress upper, ExtendedIPAddress lower)
        {
            upper--;
            Equal(upper, lower);
        }
    }
}
