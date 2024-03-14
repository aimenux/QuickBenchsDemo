using App.Shared;
using BenchmarkDotNet.Attributes;

namespace App.Benchs;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class NullCheckingBench
{
    [Params(default(string), default(SomeClass), default(object))]
    public object Value { get; set; }

    [Benchmark]
    public bool UsingEqualsNull()
    {
        return Value == null;
    }

    [Benchmark]
    public bool UsingIsNull()
    {
        return Value is null;
    }

    public class SomeClass
    {
        public string SomeProperty { get; set; }
    }
}