using App.Shared;
using BenchmarkDotNet.Attributes;

namespace App.Benchs;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class CancellationTokenBench
{
    [Benchmark]
    public CancellationToken UsingNew()
    {
        return new CancellationToken();
    }

    [Benchmark]
    public CancellationToken UsingNone()
    {
        return CancellationToken.None;
    }

    [Benchmark]
    public CancellationToken UsingDefault()
    {
        return default(CancellationToken);
    }
}