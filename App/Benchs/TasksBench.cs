using System.Diagnostics;
using App.Shared;
using BenchmarkDotNet.Attributes;

namespace App.Benchs;

[Config(typeof(LatestBenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class TasksBench
{
    [Params(10, 100, 500)] 
    public int Delay { get; set; }
    
    [Benchmark]
    public async Task WithTryCatch()
    {
        try
        {
            await Task.Delay(Delay);
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    [Benchmark]
    public async Task WithoutTryCatch()
    {
        await Task.Delay(Delay);
    }
}