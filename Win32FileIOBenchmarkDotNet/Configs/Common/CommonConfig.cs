using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;

namespace Win32FileIOBenchmarkDotNet.Configs.Common
{
    /// <summary>
    /// Implement class with common benchmark settings 
    /// </summary>
    public class CommonConfig : ManualConfig
    {
        #region Constructor

        public CommonConfig()
        {
            UnionRule = ConfigUnionRule.Union;

            Add(TargetMethodColumn.Method,
                StatisticColumn.Mean,
                StatisticColumn.Min,
                StatisticColumn.Max,
                StatisticColumn.Error,
                StatisticColumn.StdDev);

            Add(CsvExporter.Default);
            Add(CsvMeasurementsExporter.Default);
            Add(HtmlExporter.Default);
            Add(AsciiDocExporter.Default);
            Add(MarkdownExporter.Default);
        }

        #endregion
    }
}
