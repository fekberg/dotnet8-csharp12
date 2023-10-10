using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text;
using static System.Console;


var user = new User { Username = null! };



#region List & Slice Patterns

byte[] payload = new byte[] { 0x02, 0xf1, 0xaa, 0xf2, 0x23, 0xff };

#region Pattern matching

var result = payload switch
{
    [ 0x02, .. var slice ] data => Process(data, slice),
    [ 0x03, _, .. var slice] data => Process(data, slice),
    [ 0x04, _, _, .. var slice ] data => Process(data, slice),
    [] or [..] => 0x00
};


#endregion

#region Completed
//    [0x02, .. var slice] data => Process(data, slice),
//    [0x03, _, .. var slice] data => Process(data, slice),
//    [0x04, _, _, .. var slice] data => Process(data, slice),
//    [] or [..] => 0x00,
//    _ => throw new NotImplementedException()
#endregion

WriteLine($"0x{result:x}");

ReadLine();

byte Process(Span<byte> collection, Span<byte> slice)
{
    return slice[0];
}

#endregion

#region Span Pattern

ReadOnlySpan<char> input = "Filip";

if(input is "Filip")
{

}

#endregion

#region UTF-8 String Literal

ReadOnlySpan<byte> fullName = "Filip Ekberg"u8;

int space = fullName.IndexOf(" "u8);

#region Slice

ReadOnlySpan<byte> first = fullName[..space];

ReadOnlySpan<byte> last = fullName[++space..];

WriteLine($"{Encoding.UTF8.GetString(first).ToLowerInvariant()}-{Encoding.UTF8.GetString(last).ToLowerInvariant()}");

#endregion

#endregion

#region Generic Math

#region Demo
var x = new Addable();
var y = new Addable();

var sum = Add(x, y);
var sum2 = Add(1, 2);
var sum3 = Add(100m, 10.5m);
#endregion

static T Add<T>(T left, T right) where T : INumber<T>
{
    return left + right;
}

class Addable : INumber<Addable>
{
    public static Addable One => throw new NotImplementedException();

    public static int Radix => throw new NotImplementedException();

    public static Addable Zero => throw new NotImplementedException();

    public static Addable AdditiveIdentity => throw new NotImplementedException();

    public static Addable MultiplicativeIdentity => throw new NotImplementedException();

    public static Addable Abs(Addable value)
    {
        throw new NotImplementedException();
    }

    public static bool IsCanonical(Addable value)
    {
        throw new NotImplementedException();
    }

    public static bool IsComplexNumber(Addable value)
    {
        throw new NotImplementedException();
    }

    public static bool IsEvenInteger(Addable value)
    {
        throw new NotImplementedException();
    }

    public static bool IsFinite(Addable value)
    {
        throw new NotImplementedException();
    }

    public static bool IsImaginaryNumber(Addable value)
    {
        throw new NotImplementedException();
    }

    public static bool IsInfinity(Addable value)
    {
        throw new NotImplementedException();
    }

    public static bool IsInteger(Addable value)
    {
        throw new NotImplementedException();
    }

    public static bool IsNaN(Addable value)
    {
        throw new NotImplementedException();
    }

    public static bool IsNegative(Addable value)
    {
        throw new NotImplementedException();
    }

    public static bool IsNegativeInfinity(Addable value)
    {
        throw new NotImplementedException();
    }

    public static bool IsNormal(Addable value)
    {
        throw new NotImplementedException();
    }

    public static bool IsOddInteger(Addable value)
    {
        throw new NotImplementedException();
    }

    public static bool IsPositive(Addable value)
    {
        throw new NotImplementedException();
    }

    public static bool IsPositiveInfinity(Addable value)
    {
        throw new NotImplementedException();
    }

    public static bool IsRealNumber(Addable value)
    {
        throw new NotImplementedException();
    }

    public static bool IsSubnormal(Addable value)
    {
        throw new NotImplementedException();
    }

    public static bool IsZero(Addable value)
    {
        throw new NotImplementedException();
    }

    public static Addable MaxMagnitude(Addable x, Addable y)
    {
        throw new NotImplementedException();
    }

    public static Addable MaxMagnitudeNumber(Addable x, Addable y)
    {
        throw new NotImplementedException();
    }

    public static Addable MinMagnitude(Addable x, Addable y)
    {
        throw new NotImplementedException();
    }

    public static Addable MinMagnitudeNumber(Addable x, Addable y)
    {
        throw new NotImplementedException();
    }

    public static Addable Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static Addable Parse(string s, NumberStyles style, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static Addable Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static Addable Parse(string s, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Addable result)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Addable result)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Addable result)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Addable result)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<Addable>.TryConvertFromChecked<TOther>(TOther value, out Addable result)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<Addable>.TryConvertFromSaturating<TOther>(TOther value, out Addable result)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<Addable>.TryConvertFromTruncating<TOther>(TOther value, out Addable result)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<Addable>.TryConvertToChecked<TOther>(Addable value, out TOther result)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<Addable>.TryConvertToSaturating<TOther>(Addable value, out TOther result)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<Addable>.TryConvertToTruncating<TOther>(Addable value, out TOther result)
    {
        throw new NotImplementedException();
    }

    public int CompareTo(object? obj)
    {
        throw new NotImplementedException();
    }

    public int CompareTo(Addable? other)
    {
        throw new NotImplementedException();
    }

    public bool Equals(Addable? other)
    {
        throw new NotImplementedException();
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        throw new NotImplementedException();
    }

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static Addable operator +(Addable value)
    {
        throw new NotImplementedException();
    }

    public static Addable operator +(Addable left, Addable right)
    {
        throw new NotImplementedException();
    }

    public static Addable operator -(Addable value)
    {
        throw new NotImplementedException();
    }

    public static Addable operator -(Addable left, Addable right)
    {
        throw new NotImplementedException();
    }

    public static Addable operator ++(Addable value)
    {
        throw new NotImplementedException();
    }

    public static Addable operator --(Addable value)
    {
        throw new NotImplementedException();
    }

    public static Addable operator *(Addable left, Addable right)
    {
        throw new NotImplementedException();
    }

    public static Addable operator /(Addable left, Addable right)
    {
        throw new NotImplementedException();
    }

    public static Addable operator %(Addable left, Addable right)
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(Addable? left, Addable? right)
    {
        throw new NotImplementedException();
    }

    public static bool operator !=(Addable? left, Addable? right)
    {
        throw new NotImplementedException();
    }

    public static bool operator <(Addable left, Addable right)
    {
        throw new NotImplementedException();
    }

    public static bool operator >(Addable left, Addable right)
    {
        throw new NotImplementedException();
    }

    public static bool operator <=(Addable left, Addable right)
    {
        throw new NotImplementedException();
    }

    public static bool operator >=(Addable left, Addable right)
    {
        throw new NotImplementedException();
    }
}

#endregion

#region Required Properties

class User
{
    public required string Username { get; init; }
}

#endregion