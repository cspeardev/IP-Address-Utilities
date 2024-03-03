using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace IPAddressUtilities;

/// <summary>
/// Extension of <see cref="IPAddress"/> that adds extra functionality
/// </summary>
public class ExtendedIPAddress : IPAddress, IComparable<ExtendedIPAddress>, ICloneable
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

    public ExtendedIPAddress(IPAddress inAddress) : base(inAddress.GetAddressBytes())
    {
        if (inAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
        {
            ScopeId = inAddress.ScopeId;
        }
    }
    #endregion constructors

    #region operators
    /// <summary>
    /// Greater than operator.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator >(ExtendedIPAddress a, ExtendedIPAddress b) => a.CompareTo(b) > 0;
    /// <summary>
    /// Greater than or equal to operator.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator >=(ExtendedIPAddress a, ExtendedIPAddress b) => a.CompareTo(b) > 0 || a.CompareTo(b) == 0;

    /// <summary>
    /// Less than operator.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator <(ExtendedIPAddress a, ExtendedIPAddress b) => a.CompareTo(b) < 0;
    /// <summary>
    /// Less than or equal to operator.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator <=(ExtendedIPAddress a, ExtendedIPAddress b) => a.CompareTo(b) < 0 || a.CompareTo(b) == 0;
    /// <summary>
    /// Increment operator.
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static ExtendedIPAddress operator ++(ExtendedIPAddress a)
    {
        ArgumentNullException.ThrowIfNull(a);
        int tetCount = a.GetAddressBytes().Length;
        ExtendedIPAddress incrementedAddress;

        UInt128 addressInt = ConvertIPAddressBits(a);
        addressInt++;
        incrementedAddress = ConvertBitsToAddress(addressInt, tetCount);
        if (a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
        {
            incrementedAddress.ScopeId = a.ScopeId;
        }
        return incrementedAddress;
    }
    /// <summary>
    /// Decrement operator.
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static ExtendedIPAddress operator --(ExtendedIPAddress a)
    {
        ArgumentNullException.ThrowIfNull(a);
        int tetCount = a.GetAddressBytes().Length;
        ExtendedIPAddress decrementedAddress;

        UInt128 addressInt = ConvertIPAddressBits(a);
        addressInt--;
        decrementedAddress = ConvertBitsToAddress(addressInt, tetCount);
        return decrementedAddress;
    }
    /// <summary>
    /// Converts provided bits into new instance of <see cref="ExtendedIPAddress"/>.
    /// </summary>
    /// <param name="bits"></param>
    /// <param name="tetCount"></param>
    /// <returns></returns>
    private static ExtendedIPAddress ConvertBitsToAddress(UInt128 bits, int tetCount)
    {
        byte[] addressBytes = new byte[tetCount];
        for (int i = 0; i < tetCount; i++)
        {
            addressBytes[i] = (byte)(bits
                >> 8 * (tetCount - (i + 1)) & 0xFF);
        }
        return new ExtendedIPAddress(addressBytes);
    }
    /// <summary>
    /// Equal to operator.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(ExtendedIPAddress left, ExtendedIPAddress right)
    {
        if (left is null)
        {
            return right is null;
        }
        return left.Equals(right);
    }
    /// <summary>
    /// Not equal to operator.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(ExtendedIPAddress left, ExtendedIPAddress right) => !(left == right);
    #endregion

    /// <summary>
    /// Sums the bits of <paramref name="address"/> into a UInt128.
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    private static UInt128 ConvertIPAddressBits(ExtendedIPAddress address)
    {
        int tetCount = address.GetAddressBytes().Length;
        int tetBits = address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork ? 8 : 16;
        int groupBytesSize = tetBits / 8;

        byte[] addressBytes = address.GetAddressBytes();

        UInt128 addressBits = 0;

        for (int i = 0; i < tetCount / groupBytesSize; i++)
        {
            UInt128 groupBits = GetGroupBits(addressBytes, groupBytesSize, i);
            addressBits |= (groupBits << tetBits * (tetCount / groupBytesSize - (i + 1)));
        }
        return addressBits;
    }
    private static UInt128 GetGroupBits(byte[] addressBytes, int groupByteSize, int startIndex)
    {
        UInt128 groupBytes = 0;

        startIndex *= groupByteSize;

        for (int i = 0; i < groupByteSize; i++)
        {
            groupBytes |= (UInt128)(addressBytes[startIndex + groupByteSize - (i + 1)] << (8 * i));
        }
        return groupBytes;
    }

    /// <summary>
    /// Compares two IP addresses.
    /// </summary>
    /// <param name="firstAddress"></param>
    /// <param name="secondAddress"></param>
    /// <exception cref="ArgumentNullException"/>
    /// <returns>The number of addresses between <paramref name="firstAddress"/> and <paramref name="secondAddress"/></returns>
    private static UInt128 CompareIPAddresses(ExtendedIPAddress firstAddress, ExtendedIPAddress secondAddress)
    {
        ArgumentNullException.ThrowIfNull(firstAddress, nameof(firstAddress));
        ArgumentNullException.ThrowIfNull(secondAddress, nameof(secondAddress));

        UInt128 difference;

        UInt128 firstAddressBits = ConvertIPAddressBits(firstAddress);
        UInt128 secondAddressBits = ConvertIPAddressBits(secondAddress);

        UInt128 upper = UInt128.Max(firstAddressBits, secondAddressBits);
        UInt128 lower = UInt128.Min(firstAddressBits, secondAddressBits);

        difference = upper - lower;

        return difference;
    }
    public int CompareTo(ExtendedIPAddress? other)
    {
        ArgumentNullException.ThrowIfNull(other);
        long scopeDifference = 0;
        if (IsIpV6() && other.IsIpV6())
        {
            scopeDifference = ScopeId - other.ScopeId;
        }

        if (scopeDifference > 0)
        {
            return 1;
        }
        else if (scopeDifference < 0)
        {
            return -1;
        }

        UInt128 thisBits = ConvertIPAddressBits(this);
        UInt128 otherBits = ConvertIPAddressBits(other);
        
        if (thisBits > otherBits)
        {
            return 1;
        }
        else if (thisBits < otherBits)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    public static new ExtendedIPAddress Parse(string input)
    {
        IPAddress address = IPAddress.Parse(input);

        return new(address);
    }

    public static bool TryParse(string ipString, [NotNullWhen(true)] out ExtendedIPAddress? address)
    {
        bool result = TryParse(ipString, out IPAddress? tempAddress);

        if (tempAddress != null)
        {
            address = new(tempAddress);
        }
        else
        {
            address = null;
        }
        return result;
    }

    public object Clone() => new ExtendedIPAddress(GetAddressBytes(), ScopeId);

    public override bool Equals(object? comparand)
    {
        if (ReferenceEquals(this, comparand))
        {
            return true;
        }

        if (!base.Equals(comparand))
        {
            return false;
        }

        if (comparand is not ExtendedIPAddress comparandAddress)
        {
            return false;
        }

        if (!GetAddressBytes().SequenceEqual(comparandAddress.GetAddressBytes()))
        {
            return false;
        }

        if (comparandAddress.IsIpV6() && IsIpV6() && ScopeId != comparandAddress.ScopeId)
        {
            return false;
        }

        return true;
    }

    private bool IsIpV6()
    {
        return AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6;
    }

    public override int GetHashCode() => base.GetHashCode();

    public static List<IPAddress> CalculateIPRange(ExtendedIPAddress start, ExtendedIPAddress end)
    {
        List<IPAddress> targetAddresses = new();
        UInt128 addressCount;
        addressCount = CompareIPAddresses(start, end) + 1;
        ExtendedIPAddress currentAddress = start;
        for (UInt128 i = 0; i < addressCount; i++)
        {
            targetAddresses.Add(currentAddress);
            currentAddress++;
        }
        return targetAddresses;
    }
}
