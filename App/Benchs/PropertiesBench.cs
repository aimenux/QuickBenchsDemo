using App.Shared;
using BenchmarkDotNet.Attributes;

namespace App.Benchs;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class PropertiesBench
{
    private Person _somePerson;

    [GlobalSetup]
    public void Setup()
    {
        _somePerson = new Person();
    }

    [Benchmark]
    public string UsingInitializedProperty()
    {
        return _somePerson.FullNameOne;
    }

    [Benchmark]
    public string UsingExpressionBodiedProperty()
    {
        return _somePerson.FullNameTwo;
    }

    public class Person
    {
        public string FullNameOne { get; } = "John SNOW";

        public string FullNameTwo => "John SNOW";
    }
}