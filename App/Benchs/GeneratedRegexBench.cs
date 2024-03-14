using App.Shared;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace App.Benchs;

#if NET7_0_OR_GREATER

[Config(typeof(BenchConfig))]
[SimpleJob(RuntimeMoniker.Net70)]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class GeneratedRegexBench
{
    private string _input;

    [Params(100, 500, 1000)]
    public int Size { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        _input = Randomize.RandomString(Size);
    }
    
    [Benchmark(Baseline = true)]
    public bool ContainsDigitsV1()
    {
        return DigitsRegexStyles.OldStyleRegex.IsMatch(_input);
    }
    
    [Benchmark]
    public bool ContainsDigitsV2()
    {
        return DigitsRegexStyles.NewStyleRegex.IsMatch(_input);
    }
}

public static partial class DigitsRegexStyles
{
    private const string RegexPattern = @"^[0-9]+$";
    
    [GeneratedRegex(RegexPattern, RegexOptions.Compiled)]
    private static partial Regex DigitsGeneratedRegex();
    
    public static readonly Regex OldStyleRegex = new Regex(RegexPattern, RegexOptions.Compiled);
    
    public static readonly Regex NewStyleRegex = DigitsGeneratedRegex();
}

#endif