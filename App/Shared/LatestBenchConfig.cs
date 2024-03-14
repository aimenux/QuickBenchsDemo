using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;

namespace App.Shared;

public class LatestBenchConfig : ManualConfig
{
    public LatestBenchConfig()
    {
        HideColumns(Column.RatioSD);
        AddColumn(RankColumn.Arabic);
        AddColumn(StatisticColumn.Min);
        AddColumn(StatisticColumn.Max);
        AddColumn(CategoriesColumn.Default);
        AddColumnProvider(DefaultColumnProviders.Instance);
        AddLogger(ConsoleLogger.Default);
        AddExporter(RPlotExporter.Default);
        AddExporter(CsvMeasurementsExporter.Default);
        AddExporter(HtmlExporter.Default);
        AddExporter(MarkdownExporter.GitHub);
        AddExporter(MarkdownExporter.Default);
        AddDiagnoser(MemoryDiagnoser.Default);
        AddDiagnoser(ExceptionDiagnoser.Default);
        AddLogicalGroupRules(BenchmarkLogicalGroupRule.ByParams);
        WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest));
        WithSummaryStyle(SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend));
        AddJob(Job.Default.WithRuntime(CoreRuntime.Core80).WithId(nameof(RuntimeMoniker.Net80)));
    }
}