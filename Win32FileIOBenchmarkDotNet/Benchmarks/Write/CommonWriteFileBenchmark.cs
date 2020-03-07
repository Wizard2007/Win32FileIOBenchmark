using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using Win32FileIOBenchmarkDotNet.Benchmarks.Common;

namespace Win32FileIOBenchmarkDotNet.Benchmarks.Write
{
    /// <inheritdoc />
    /// <summary>
    /// Define common class to test write file perfomance
    /// </summary>
    public abstract class CommonWriteFileBenchmark : CommonFileBenchmark
    {
        #region Constructor

        protected CommonWriteFileBenchmark()
            => ShortFileNames = Directory
                .GetFiles($@"{DataPath}\Write\", "*.txt", SearchOption.TopDirectoryOnly)
                .Select(Path.GetFileName);

        #endregion

        #region BenchMarks

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void ProduceFileName(string shortFileName)
            => GetFullFileName(shortFileName);

        #region File.WriteXXX

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestWriteAllLines(string shortFileName)
            => FileEfficientTests.TestWriteAllLines(GetFullFileName(shortFileName), (string[])FileEfficientTests.StringsContents[shortFileName]);

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestWriteAllText(string shortFileName)
            => FileEfficientTests.TestWriteAllText(GetFullFileName(shortFileName), FileEfficientTests.StringContents[shortFileName]);

        #endregion

        #region Write bytes

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestWriteAllBytes(string shortFileName)
            => FileEfficientTests.TestWriteAllBytes(GetFullFileName(shortFileName));

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestBinaryWriter1(string shortFileName)
            => FileEfficientTests.TestBinaryWriter1(GetFullFileName(shortFileName), FileEfficientTests.BytesInFiles[shortFileName]);

        #endregion

        #region StreamWriter

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestStreamWriter1(string shortFileName)
            => FileEfficientTests.TestStreamWriter1(GetFullFileName(shortFileName), FileEfficientTests.BytesInFiles[shortFileName]);

        #endregion

        #region FileStreamWrite

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestFileStreamWrite1(string shortFileName)
            => FileEfficientTests.TestFileStreamWrite1(GetFullFileName(shortFileName), FileEfficientTests.BytesInFiles[shortFileName]);

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestFileStreamWrite2(string shortFileName)
            => FileEfficientTests.TestFileStreamWrite2(GetFullFileName(shortFileName), FileEfficientTests.BytesInFiles[shortFileName]);

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestFileStreamWrite3(string shortFileName)
            => FileEfficientTests.TestFileStreamWrite3(GetFullFileName(shortFileName), FileEfficientTests.BytesInFiles[shortFileName]);

        #endregion
        
        #region WinAPI
        
        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestWriteFileWinApi1(string shortFileName)
            => FileEfficientTests.TestWriteFileWinApi1(GetFullFileName(shortFileName), FileEfficientTests.BytesInFiles[shortFileName]);

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ArgumentsSource(nameof(ShortFileNames))]
        public void TestWriteFileWinApi2(string shortFileName)
            => FileEfficientTests.TestWriteFileWinApi2(GetFullFileName(shortFileName), FileEfficientTests.BytesInFiles[shortFileName]);
                    
        #endregion

        #endregion

        #region Public methods

        public override void GlobalSetUp()
        {
            base.GlobalSetUp();

            foreach (var shortFileName in ShortFileNames)
            {
                var fullFileName = $@"{DataPath}\Write\{shortFileName}";

                if (!FileEfficientTests.StringsContents.ContainsKey(shortFileName))
                {
                    FileEfficientTests.StringsContents[shortFileName] = File.ReadAllLines(fullFileName);
                }

                if (!FileEfficientTests.StringContents.ContainsKey(shortFileName))
                {
                    FileEfficientTests.StringContents[shortFileName] = File.ReadAllText(fullFileName);
                }

                if (!FileEfficientTests.BytesInFiles.ContainsKey(shortFileName))
                {
                    FileEfficientTests.BytesInFiles[shortFileName] =
                        FileEfficientTests.StringContents[shortFileName].Length;
                }
            }
        }

        #endregion

        #region Protected methods

        protected override void CopyFilesForBenchMarksGlobal()
            => CopyFilesForBenchMarks(ClassName, $@"{DataPath}\Write");

        #endregion
    }
}
