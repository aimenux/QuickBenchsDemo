using System.Globalization;
using System.Text;
using App.Shared;
using BenchmarkDotNet.Attributes;

namespace App.Benchs;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class RemoveDiacriticsBench
{
    private string _input;

    [Params(10, 30, 50, 100)]
    public int Size { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        _input = Randomize.RandomString(Size);
    }
    
    [Benchmark(Baseline = true)]
    public string WithLinq()
    {
        var chars = _input
            .Normalize(NormalizationForm.FormD)
            .Where(c => !IsDiacriticalMark(c))
            .ToArray();

        return new string(chars);
    }
    
    [Benchmark]
    public string WithStringBuilder()
    {
        var sb = new StringBuilder(_input.Length);

        var chars = _input
            .Normalize(NormalizationForm.FormD)
            .Where(c => !IsDiacriticalMark(c));

        foreach(var c in chars)
        {
            sb.Append(c);
        }

        return sb.ToString();
    }
    
    private static bool IsDiacriticalMark(char c)
    {
        return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark;
    }
}