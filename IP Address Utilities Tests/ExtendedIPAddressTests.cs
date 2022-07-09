using IPAddressUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace IPAddressUtilitiesTests
{
    [TestClass]
    public class ExtendedIPAddressTests
    {
        [TestMethod]
        public void NotEqualTest()
        {
            ExtendedIPAddress lower = new("10.0.0.1");
            ExtendedIPAddress upper = new("10.0.0.2");
            
            AreNotEqual(upper, lower);
        }


        [TestMethod]
        public void EqualTest()
        {
            ExtendedIPAddress address = new("10.0.0.1");
            ExtendedIPAddress address2 = new("10.0.0.1");

            AreEqual(address, address2);
            IsTrue(address == address2);
        }


        [TestMethod]
        public void IncrementTest()
        {
            ExtendedIPAddress address = new("10.0.0.1");
            address++;
            AreEqual(new("10.0.0.2"), address);

            address = new("10.0.0.255");
            address++;
            AreEqual(new("10.0.1.0"), address);
        }

        [TestMethod]
        public void DecrementTest()
        {

            ExtendedIPAddress address = new("10.0.0.2");
            address--;
            AreEqual(address, new("10.0.0.1"));
        }

    }
}
