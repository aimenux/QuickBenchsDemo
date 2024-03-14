using App.Shared;
using BenchmarkDotNet.Attributes;

namespace App.Benchs;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class NumberPaddingBench
{
    public const int Number = 1;

    [Benchmark]
    public string UsingPadLeft()
    {
        return Number.ToString().PadLeft(4, '0');
    }

    [Benchmark]
    public string UsingToStringFormat1()
    {
        return Number.ToString("0000");
    }

    [Benchmark]
    public string UsingToStringFormat2()
    {
        return Number.ToString("D4");
    }

    [Benchmark]
    public string UsingStringInterpolation1()
    {
        return $"{Number:0000}";
    }

    [Benchmark]
    public string UsingStringInterpolation2()
    {
        return $"{Number:D4}";
    }

    [Benchmark]
    public string UsingStringFormat1()
    {
        return string.Format("{0:0000}", Number);
    }

    [Benchmark]
    public string UsingStringFormat2()
    {
        return string.Format("{0:D4}", Number);
    }

    [Benchmark]
    public string UsingStringFormat3()
    {
        return string.Format("{0,5}", Number);
    }
}