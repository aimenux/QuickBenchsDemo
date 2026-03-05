using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using App.Shared;
using BenchmarkDotNet.Attributes;
using static App.Benchs.RemoveDiacriticsConstants;

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
        var normalized = _input.Normalize(NormalizationForm.FormD);
        
        return new string(normalized.Where(c => !IsDiacriticalMark(c)).ToArray());
    }
    
    [Benchmark]
    public string WithStringBuilder()
    {
        var normalized = _input.Normalize(NormalizationForm.FormD);
        
        var sb = new StringBuilder(normalized.Length);

        foreach (var c in normalized.Where(c => !IsDiacriticalMark(c)))
        {
            sb.Append(c);
        }

        return sb.ToString();
    }
    
    [Benchmark]
    public string WithRegex()
    {
        var normalized = _input.Normalize(NormalizationForm.FormD);
        
        return DiacriticalMarksRegex().Replace(normalized, string.Empty);
    }
    
    private static bool IsDiacriticalMark(char c)
    {
        return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark;
    }
}

public static partial class RemoveDiacriticsConstants
{
    private const int MaxTimeInMilliseconds = 1000;

    private const RegexOptions Options = RegexOptions.Compiled;

    [GeneratedRegex(@"[\u0300-\u036F]", Options, MaxTimeInMilliseconds)]
    public static partial Regex DiacriticalMarksRegex();
}