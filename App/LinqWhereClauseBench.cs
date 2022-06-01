using BenchmarkDotNet.Attributes;

namespace App;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class LinqWhereClauseBench
{
    private List<int> _items;

    [Params(100, 1000, 10_000)]
    public int Size { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var random = new Random(Guid.NewGuid().GetHashCode());
        _items = Enumerable
            .Range(0, Size)
            .Select(_ => random.Next(0, int.MaxValue))
            .OrderBy(_ => Guid.NewGuid())
            .ToList();
    }

    [Benchmark]
    public List<int> FilterUsingSingleWhereClause()
    {
        return _items
            .Where(x => x.IsDividableBy(2) && x.IsDividableBy(3) && x.IsDividableBy(5))
            .ToList();
    }

    [Benchmark]
    public List<int> FilterUsingMultipleWhereClauses()
    {
        return _items
            .Where(x => x.IsDividableBy(2))
            .Where(x => x.IsDividableBy(3))
            .Where(x => x.IsDividableBy(5))
            .ToList();
    }
}

public static class LinqWhereClauseBenchExtensions
{
    public static bool IsDividableBy(this int item, int number) => item % number == 0;
}