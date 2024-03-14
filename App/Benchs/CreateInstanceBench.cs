using System.Linq.Expressions;
using App.Shared;
using BenchmarkDotNet.Attributes;

namespace App.Benchs;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class CreateInstanceBench
{
    [Benchmark(Baseline = true)]
    public object UsingLambda() => CreateInstance(typeof(Employee));
    
    [Benchmark]
    public object UsingActivator() => Activator.CreateInstance(typeof(Employee));

    private static readonly Func<Type, object> CreateInstance = type => Expression.Lambda<Func<object>>(Expression.New(type)).Compile()();
    
    private class Employee
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Address { get; init; }
    }
}