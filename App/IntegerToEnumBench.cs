using BenchmarkDotNet.Attributes;

namespace App;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class IntegerToEnumBench
{
    [Params(1, 10, 100)]
    public int Code { get; set; }

    [Benchmark]
    public SomeEnum UsingEnumParse()
    {
        return Enum.Parse<SomeEnum>(Code.ToString());
    }

    [Benchmark]
    public SomeEnum UsingCasting()
    {
        return (SomeEnum)Code;
    }

    [Benchmark]
    public SomeEnum UsingGenericEnumParse()
    {
        return GenericConverter<SomeEnum>.UsingEnumParse(Code);
    }

    [Benchmark]
    public SomeEnum UsingGenericCasting()
    {
        return GenericConverter<SomeEnum>.UsingCasting(Code);
    }

    public class GenericConverter<TEnum> where TEnum : struct, Enum
    {
        public static TEnum UsingEnumParse(int code)
        {
            return Enum.Parse<TEnum>(code.ToString());
        }

        public static TEnum UsingCasting(int code)
        {
            return (TEnum)(object)code;
        }
    }

    public enum SomeEnum
    {
        A = 1,
        B = 10,
        C = 100
    }
}