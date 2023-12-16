using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace App;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class StringEnumerableBench
{
    private IEnumerable<string> _tags;
    private IEnumerable<IEnumerable<string>> _tagsGroups;

    [Params(10, 100, 1000)] 
    public int Size { get; set; }

    public int Length => Size / 2;
    
    [GlobalSetup]
    public void Setup()
    {
        _tags = GetTags(Size, Length);
        _tagsGroups = GetTagsGroups(Size, Length);
    }
    
    [Benchmark]
    public int UsingContains()
    {
        var results = _tagsGroups.Where(x => _tags.All(y => x.Contains(y, StringComparer.OrdinalIgnoreCase)));
        return results.Count();
    }

    [Benchmark]
    public int UsingHashSet()
    {
        var hashset = new HashSet<string>(_tags, StringComparer.OrdinalIgnoreCase);
        var results = _tagsGroups.Where(x => hashset.IsSubsetOf(x) );
        return results.Count();
    }

    private static IEnumerable<string> GetTags(int size, int length)
    {
        return Enumerable.Range(0, size).Select(_ => Randomize.RandomString(length));
    }
    
    private static IEnumerable<IEnumerable<string>> GetTagsGroups(int size, int length)
    {
        return Enumerable.Range(0, size).Select(_ => GetTags(size, length));
    }
}