using System.Diagnostics;
using BenchmarkDotNet.Attributes;

namespace App;

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