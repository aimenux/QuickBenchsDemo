using App.Shared;
using BenchmarkDotNet.Attributes;

namespace App.Benchs;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class CopyArrayBench
{
    private string[] _array;

    [Params(100, 1000, 10_000)]
    public int Size { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        _array = Enumerable
            .Range(1, Size)
            .Select(_ => Randomize.RandomString(10))
            .OrderBy(_ => Guid.NewGuid())
            .ToArray();
    }
    
    [Benchmark]
    public string[] CopyArrayV1() => _array.ToArray();

    [Benchmark]
    public string[] CopyArrayV2()
    {
        var copy = new string[_array.Length];
        Array.Copy(_array, copy, _array.Length);
        return copy;
    }
    
#if NET8_0_OR_GREATER    
    [Benchmark]
    public string[] CopyArrayV3() => [.._array];
#endif
}