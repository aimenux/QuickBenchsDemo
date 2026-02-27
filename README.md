[![.NET](https://github.com/aimenux/QuickBenchsDemo/actions/workflows/ci.yml/badge.svg)](https://github.com/aimenux/QuickBenchsDemo/actions/workflows/ci.yml)

# QuickBenchsDemo
```
Quick benchmarks for various things encountered in my dev journey
```

In this demo, i m using [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) library in order to quick benchmark various things encountered in my dev journey.
>

In order to run benchmarks, type this kind of commands in your favorite terminal :
>
> :writing_hand: `dotnet run --project .\App\ -c Release --framework net9.0 --runtimes net8.0 --filter *RemoveDiacriticsBench*`
>
> :writing_hand: `dotnet run --project .\App\ -c Release --framework net9.0 --runtimes net9.0 --filter *RemoveDiacriticsBench*`
>
> :writing_hand: `dotnet run --project .\App\ -c Release --framework net9.0 --runtimes net10.0 --filter *RemoveDiacriticsBench*`
>
> :writing_hand: `dotnet run --project .\App\ -c Release --framework net9.0 --runtimes net8.0 net9.0 net10.0 --filter *RemoveDiacriticsBench*`
> 
**`Tools`** : net 8.0, net 9.0, net 10.0, benchmark-dotnet