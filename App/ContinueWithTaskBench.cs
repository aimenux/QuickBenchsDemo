using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace App;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class ContinueWithTaskBench
{
    private HttpClient _httpClient;

    [GlobalSetup]
    public void Setup()
    {
        var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
        var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
        _httpClient = httpClientFactory.CreateClient();
    }

    [Arguments("https://www.google.com")]
    [Benchmark]
    public Task<string> ContinueWithTask(string url)
    {
        return GetContentStringV1Async(url);
    }

    [Arguments("https://www.google.com")]
    [Benchmark]
    public Task<string> AsyncAwaitTask(string url)
    {
        return GetContentStringV2Async(url);
    }

    public async Task<string> GetContentStringV1Async(string url)
    {
        var request = await _httpClient.GetAsync(url);
        var response = await request.Content.ReadAsStringAsync();
        return response;
    }

    public async Task<string> GetContentStringV2Async(string url) 
    { 
        var request = _httpClient.GetAsync(url); 
        var response = request
            .ContinueWith(message => message.Result.Content.ReadAsStringAsync()); 
        return await response.Unwrap(); 
    }
}