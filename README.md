[![.NET](https://github.com/aimenux/QuickBenchsDemo/actions/workflows/ci.yml/badge.svg)](https://github.com/aimenux/QuickBenchsDemo/actions/workflows/ci.yml)

# QuickBenchsDemo
```
Quick benchmarks for various things encountered in my dev journey
```

In this demo, i m using [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) library in order to quick benchmark various things encountered in my dev journey.
>

In order to run benchmarks, type this command in your favorite terminal :
>
> :writing_hand: `dotnet run --project .\App\ -c release -f net6.0 --filter *IntegerToEnumBench*`
>
> :writing_hand: `dotnet run --project .\App\ -c release -f net8.0 --filter *IntegerToEnumBench*`

>
**`Tools`** : net 6.0, net 7.0, net 8.0, benchmark-dotnet