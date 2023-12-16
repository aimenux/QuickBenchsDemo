using BenchmarkDotNet.Attributes;

namespace App;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class EnumToStringBench
{
    [Benchmark(Baseline = true)]
    public string UsingToString() => SomeEnumType.SomeEnumValue.ToString();
    
    [Benchmark]
    public string UsingNameof() => nameof(SomeEnumType.SomeEnumValue);
    
    private enum SomeEnumType
    {
        SomeEnumValue
    }
}