#if (DEBUG)
using BenchmarkDotNet.Configs;
#endif
using BenchmarkDotNet.Running;
using Win32FileIOBenchmarkDotNet.Benchmarks.Read;
using Win32FileIOBenchmarkDotNet.Benchmarks.Write;

namespace Win32FileIOBenchmarkDotNet
{
    class Program
    {
#if (DEBUG)
        static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
#else
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<SingleReadFileClearIoCacheBenchmark>();
            BenchmarkRunner.Run<SingleReadFileBenchmark>();
            BenchmarkRunner.Run<AverageReadFileBenchmark>();

            BenchmarkRunner.Run<SingleWriteFileClearIoCacheBenchmark>();
            BenchmarkRunner.Run<SingleWriteFileBenchmark>();
            BenchmarkRunner.Run<AverageWriteFileBenchmark>();
        }
#endif
    }
}
