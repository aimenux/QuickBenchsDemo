using System.Text;
using BenchmarkDotNet.Attributes;

namespace App;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class StringComparisonBench
{
    private string _left, _right;

    [Params(100, 1000, 10_000)]
    public int Size { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _left = Randomize.RandomString(Size);
        _right = BuildStringThatDifferByOneCharacter(_left);
    }

    [Benchmark]
    public bool UsingToLowerInvariant()
    {
        return _left.ToLowerInvariant() == _right.ToLowerInvariant();
    }

    [Benchmark]
    public bool UsingToUpperInvariant()
    {
        return _left.ToUpperInvariant() == _right.ToUpperInvariant();
    }

    [Benchmark]
    public bool UsingOrdinalIgnoreCase()
    {
        return string.Equals(_left, _right, StringComparison.OrdinalIgnoreCase);
    }

    [Benchmark]
    public bool UsingCurrentCultureIgnoreCase()
    {
        return string.Equals(_left, _right, StringComparison.CurrentCultureIgnoreCase);
    }

    [Benchmark]
    public bool UsingInvariantCultureIgnoreCase()
    {
        return string.Equals(_left, _right, StringComparison.InvariantCultureIgnoreCase);
    }

    private static string BuildStringThatDifferByOneCharacter(string input)
    {
        var min = input.Length / 2;
        var max = input.Length - 1;
        var index = Randomize.RandomNumber(min, max);
        var output = new StringBuilder(input)
        {
            [index] = Randomize.RandomChar()
        }.ToString();
        return output;
    }
}