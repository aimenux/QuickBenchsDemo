using App.Shared;
using BenchmarkDotNet.Attributes;

namespace App.Benchs;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class StringPaddingBench
{
    public const int Width = 20;

    [Benchmark]
    public string UsingPadLeft()
    {
        return nameof(StringPaddingBench).PadLeft(Width);
    }

    [Benchmark]
    public string UsingPadRight()
    {
        return nameof(StringPaddingBench).PadRight(Width);
    }

    [Benchmark]
    public string UsingStringInterpolation()
    {
        return $"{nameof(StringPaddingBench),Width}";
    }
}