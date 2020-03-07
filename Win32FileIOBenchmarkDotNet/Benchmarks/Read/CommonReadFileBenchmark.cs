using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using Win32FileIOBenchmarkDotNet.Benchmarks.Common;

namespace Win32FileIOBenchmarkDotNet.Benchmarks.Read
{
    /// <inheritdoc />
    /// <summary>
    /// Define common class to test read file perfomance
    /// </summary>
    public abstract class CommonReadFileBenchmark : CommonFileBenchmark
    {
        #region Constructor

        protected CommonReadFileBenchmark()
            => ShortFileNames = Directory
                .GetFiles($@"{DataPath}\Read\", "*.txt", SearchOption.TopDirectoryOnly)
                .Select(Path.GetFileName);

        #endregion
        
        #region BenchMarks

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void ProduceFileName(string shortFileName)
            => GetFullFileName(shortFileName);

        #region File.ReadXXX

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestReadAllLines(string shortFileName)
            => FileEfficientTests.TestReadAllLines(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestReadAllText(string shortFileName)
            => FileEfficientTests.TestReadAllText(GetFullFileName(shortFileName));

        #endregion

        #region Read bytes

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestReadAllBytes(string shortFileName)
            => FileEfficientTests.TestReadAllBytes(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestBinaryReader1(string shortFileName)
            => FileEfficientTests.TestBinaryReader1(GetFullFileName(shortFileName));

        #endregion

        #region StreamReader

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestStreamReader1(string shortFileName)
            => FileEfficientTests.TestStreamReader1(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestStreamReader2(string shortFileName)
            => FileEfficientTests.TestStreamReader2(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestStreamReader3(string shortFileName)
            => FileEfficientTests.TestStreamReader3(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestStreamReader4(string shortFileName)
            => FileEfficientTests.TestStreamReader4(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestStreamReader5(string shortFileName)
            => FileEfficientTests.TestStreamReader5(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestStreamReader6(string shortFileName)
            => FileEfficientTests.TestStreamReader6(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestStreamReader7(string shortFileName)
            => FileEfficientTests.TestStreamReader7(GetFullFileName(shortFileName));

        #endregion

        #region FileStreamRead

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestFileStreamRead1(string shortFileName)
            => FileEfficientTests.TestFileStreamRead1(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestFileStreamRead2(string shortFileName)
            => FileEfficientTests.TestFileStreamRead2(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestFileStreamRead2A(string shortFileName)
            => FileEfficientTests.TestFileStreamRead2A(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestFileStreamRead3(string shortFileName) =>
            FileEfficientTests.TestFileStreamRead3(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestFileStreamRead4(string shortFileName)
            => FileEfficientTests.TestFileStreamRead4(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestFileStreamRead5(string shortFileName)
            => FileEfficientTests.TestFileStreamRead5(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestFileStreamRead6(string shortFileName)
            => FileEfficientTests.TestFileStreamRead6(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestFileStreamRead7(string shortFileName)
            => FileEfficientTests.TestFileStreamRead7(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestFileStreamRead8(string shortFileName)
            => FileEfficientTests.TestFileStreamRead8(GetFullFileName(shortFileName));

        #endregion

        #region WinAPI

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestReadFileWinApi1(string shortFileName)
            => FileEfficientTests.TestReadFileWinApi1(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestReadFileWinApi2(string shortFileName)
            => FileEfficientTests.TestReadFileWinApi2(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestReadFileWinApi3(string shortFileName)
            => FileEfficientTests.TestReadFileWinApi3(GetFullFileName(shortFileName));

        #endregion

        #region NoOpenClose

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestBinaryReader1NoOpenClose(string shortFileName)
            => FileEfficientTests.TestBinaryReader1NoOpenClose(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestStreamReader2NoOpenClose(string shortFileName)
            => FileEfficientTests.TestStreamReader2NoOpenClose(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestFileStreamRead1NoOpenClose(string shortFileName)
            => FileEfficientTests.TestFileStreamRead1NoOpenClose(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestReadFileWinApiNoOpenClose(string shortFileName)
            => FileEfficientTests.TestReadFileWinApiNoOpenClose(GetFullFileName(shortFileName));

        #endregion

        #endregion
        
        #region Benchmark Setup

        protected override void CopyFilesForBenchMarksGlobal()
            => CopyFilesForBenchMarks(ClassName, $@"{DataPath}\Read");

        #endregion
    }
}