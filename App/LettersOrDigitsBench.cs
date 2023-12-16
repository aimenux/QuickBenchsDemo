using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace App;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class LettersOrDigitsBench
{
    private static readonly Regex LettersOrDigitsRegex = new Regex(@"^[a-zA-Z0-9]+$", RegexOptions.Compiled);
    
    private string _input;

    [Params(100, 1000, 10_000)]
    public int Size { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        _input = Randomize.RandomString(Size);
    }
    
    [Benchmark(Baseline = true)]
    public bool ContainsLettersOrDigitsV1()
    {
        return _input.All(char.IsLetterOrDigit);
    }
    
    [Benchmark]
    public bool ContainsLettersOrDigitsV2()
    {
        foreach (var item in _input.AsSpan())
        {
            if (!char.IsLetterOrDigit(item)) return false;
        }

        return true;
    }
    
    [Benchmark]
    public bool ContainsLettersOrDigitsV3()
    {
        return Regex.IsMatch(_input, @"^[a-zA-Z0-9]+$");
    }
    
    [Benchmark]
    public bool ContainsLettersOrDigitsV4()
    {
        return LettersOrDigitsRegex.IsMatch(_input);
    }
}