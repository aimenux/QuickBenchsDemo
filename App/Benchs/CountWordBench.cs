using System.Text.RegularExpressions;
using App.Shared;
using BenchmarkDotNet.Attributes;

namespace App.Benchs;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class CountWordBench
{
    private string _word;
    private string _file;

    [Params(100, 1000, 10_000)]
    public int Size { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        var items = Enumerable
            .Range(0, Size)
            .Select(_ => Randomize.RandomString(10))
            .OrderBy(_ => Guid.NewGuid())
            .ToList();
        
        _file = $"./file-{Size}.txt";
        _word = items.ElementAt(Size / 2);
        File.WriteAllLines(_file, items);
    }

#if NET7_0_OR_GREATER
    [Benchmark]
    public int CountWordV1() => Regex.Count(File.ReadAllText(_file), $@"\b{_word}\b", RegexOptions.IgnoreCase);
#endif
    
    [Benchmark]
    public int CountWordV2() => Regex.Matches(File.ReadAllText(_file), $@"\b{_word}\b", RegexOptions.IgnoreCase).Count;
        
    [Benchmark]
    public int CountWordV3() => File.ReadLines(_file).Select(line => Regex.Matches(line, $@"\b{_word}\b", RegexOptions.IgnoreCase).Count).Sum();

    [Benchmark]
    public int CountWordV4() => File.ReadAllText(_file).Split(' ', '.', ',', ';', ':', '?', '\n', '\r', '\t').Count(x => string.Equals(x, _word, StringComparison.OrdinalIgnoreCase));
    
    [Benchmark]
    public int CountWordV5() => Array.FindAll(File.ReadAllText(_file).Split(' ', '.', ',', ';', ':', '?', '\n', '\r', '\t'), x => string.Equals(x, _word, StringComparison.OrdinalIgnoreCase)).Length;
}