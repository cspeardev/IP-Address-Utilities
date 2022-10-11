namespace IPAddressUtilities.Tests;

internal class ExtendedIpAddressTestData
{
    public static IEnumerable<object[]> NotEqualTestData
    {
        get
        {
            yield return new object[] { new ExtendedIPAddress(new byte[] { 10, 0, 0, 1 }), new ExtendedIPAddress(new byte[] { 10, 0, 0, 2 }) };
            yield return new object[] { new ExtendedIPAddress(new byte[] { 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 }, 5), new ExtendedIPAddress(new byte[] { 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 }, 3), };
        }
    }

    public static IEnumerable<object[]> IncrementTestData
    {
        get
        {
            yield return new object[] { new ExtendedIPAddress(new byte[] { 10, 0, 0, 1 }), new ExtendedIPAddress(new byte[] { 10, 0, 0, 2 }) };
            yield return new object[] { new ExtendedIPAddress(new byte[] { 10, 0, 0, 255 }), new ExtendedIPAddress(new byte[] { 10, 0, 1, 0 }) };
            yield return new object[] { new ExtendedIPAddress(new byte[] { 10, 0, 255, 255 }), new ExtendedIPAddress(new byte[] { 10, 1, 0, 0 }) };
            yield return new object[] { new ExtendedIPAddress(new byte[] { 10, 255, 255, 255 }), new ExtendedIPAddress(new byte[] { 11, 0, 0, 0 }) };
            yield return new object[] { new ExtendedIPAddress(new byte[] { 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }), new ExtendedIPAddress(new byte[] { 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 }) };
            yield return new object[] { new ExtendedIPAddress(new byte[] { 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 5), new ExtendedIPAddress(new byte[] { 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 }, 5), };
        }
    }

    public static IEnumerable<object[]> DecrementTestData
    {
        get
        {
            yield return new object[] { new ExtendedIPAddress(new byte[] { 10, 0, 0, 1 }), new ExtendedIPAddress(new byte[] { 10, 0, 0, 0 }) };
        }
    }

    public static IEnumerable<object[]> EqualsTestData
    {
        get
        {
            yield return new object[] { new ExtendedIPAddress(new byte[] { 10, 0, 0, 1 }), new ExtendedIPAddress(new byte[] { 10, 0, 0, 1 }) };

            yield return new object[] { new ExtendedIPAddress(new byte[] { 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }), new ExtendedIPAddress(new byte[] { 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }) };

            ExtendedIPAddress sameReferenceTest = new(new byte[] { 10, 0, 0, 60 });

            yield return new object[] { sameReferenceTest, sameReferenceTest };
        }
    }

    public static IEnumerable<object[]> GreaterThanTestData
    {
        get
        {
            yield return new object[] { new ExtendedIPAddress(new byte[] { 10, 0, 0, 2 }), new ExtendedIPAddress(new byte[] { 10, 0, 0, 1 }) };
            yield return new object[] { new ExtendedIPAddress(new byte[] { 10, 0, 1, 2 }), new ExtendedIPAddress(new byte[] { 10, 0, 1, 1 }) };
            yield return new object[] { new ExtendedIPAddress(new byte[] { 10, 1, 0, 2 }), new ExtendedIPAddress(new byte[] { 10, 1, 0, 1 }) };
            yield return new object[] { new ExtendedIPAddress(new byte[] { 11, 0, 0, 2 }), new ExtendedIPAddress(new byte[] { 10, 0, 0, 1 }) };
            yield return new object[] { new ExtendedIPAddress(new byte[] { 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }), new ExtendedIPAddress(new byte[] { 10, 0, 0, 1 }) };
        }
    }

    public static IEnumerable<object[]> LessThanTestData
    {
        get
        {
            yield return new object[] { new ExtendedIPAddress(new byte[] { 10, 0, 0, 1 }), new ExtendedIPAddress(new byte[] { 10, 0, 0, 2 }) };
        }
    }

    public static IEnumerable<object[]> ParseTestData
    {
        get
        {
            yield return new object[] { "10.0.0.2", new ExtendedIPAddress(new byte[] { 10, 0, 0, 2 }) };
            yield return new object[] { "255.255.255.255", new ExtendedIPAddress(new byte[] { 255, 255, 255, 255 }) };
            yield return new object[] { "10.0.0.2", new ExtendedIPAddress(new byte[] { 10, 0, 0, 2 }) };
            yield return new object[] { "a00::1", new ExtendedIPAddress(new byte[] { 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }) };
            yield return new object[] { "a00::1%3", new ExtendedIPAddress(new byte[] { 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 3) };
        }
    }
}
