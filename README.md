# Win32FileIOBenchmarkDotNet
## .NET Benchmark for Win32FileIO samples

BenchmarkDotNet implementation for IO tests of .NET operations described in the [article](https://designingefficientsoftware.wordpress.com/2011/03/03/efficient-file-io-from-csharp/) written by Robert G. Bryan in Feb, 2011. This implementation uses a slightly modified [original code](https://drive.google.com/open?id=0BzbayMkzmN-pdGNKMVZLNHhVX0k) form [article](https://designingefficientsoftware.wordpress.com/2011/03/03/efficient-file-io-from-csharp/) adapted for building tests using BenchmarkDotNet.
In this implementation, I pay attention to test the cold start of IO operations with clearing the IO cache ([SingleReadFileClearIoCacheBenchmark](Win32FileIOBenchmarkDotNet/Benchmarks/Read/SingleReadFileClearIoCacheBenchmark.cs), [SingleWriteFileClearIoCacheBenchmark](Win32FileIOBenchmarkDotNet/Benchmarks/Write/SingleWriteFileClearIoCacheBenchmark.cs)) and reading / writing the same file several times per iteration ([AverageReadFileBenchmark](Win32FileIOBenchmarkDotNet/Benchmarks/Read/AverageReadFileBenchmark.cs), [AverageWriteFileBenchmark](Win32FileIOBenchmarkDotNet/Benchmarks/Write/AverageWriteFileBenchmark.cs)). Also, the previous tests runs for a cold start are less affected without IO cleaning ([SingleReadFileBenchmark](Win32FileIOBenchmarkDotNet/Benchmarks/Read/SingleReadFileBenchmark.cs), [SingleWriteFileClearIoCacheBenchmark](Win32FileIOBenchmarkDotNet/Benchmarks/Write/SingleWriteFileClearIoCacheBenchmark.cs))) by creating dedicated files for each test set.

## Usage

 To run the tests you need at least 7.77 Gb of free space to create test text files in the directory <br>

`.\Win32FileIOBenchmarkDotNet\Win32FileIOBenchmarkDotNet\data\`

For IO tests, all * .txt files from the root of the directory will be used

`.\Win32FileIOBenchmarkDotNet\Win32FileIOBenchmarkDotNet\data\Read\`

`.\Win32FileIOBenchmarkDotNet\Win32FileIOBenchmarkDotNet\data\Write\`

respectively. You can add your files to the appropriate directory for testing without changing the source code.
These files will be copied to the folder 

`.\Win32FileIOBenchmarkDotNet\Win32FileIOBenchmarkDotNet\data\BenchmarkClassName\TestMethodName`

To run all the tests, you should  run 

`.\Win32FileIOBenchmarkDotNet\Win32FileIOBenchmarkDotNet\bin\Release\Win32FileIOBenchmarkDotNet.exe`

You can use this code to test the performance of various IO .NET features.

## Summary 

I got the following results.

### Write

* [SingleWriteFileClearIoCacheBenchmark](BenchmarkResults/Win32FileIOBenchmarkDotNet.Benchmarks.Write.SingleWriteFileClearIoCacheBenchmark-report-github.md)
  <details>
      <summary>
        01 MB.txt Top 5 results SingleWriteFileClearIoCacheBenchmark
      </summary>

      | Method           | Size | Mean (us) | Error (us) | StdDev (us) | Median (us) | Min (us) | Max (us) |
      | ---------------- | ---: | --------: | ---------: | ----------: | ----------: | -------: | -------: |
      | FileStreamWrite1 | 01MB |   2219.10 |     190.82 |      126.21 |     2179.50 |  2128.70 |  2566.20 |
      | BinaryWriter1    | 01MB |   2561.40 |     304.53 |      201.43 |     2501.60 |  2461.70 |  3130.20 |
      | WriteFileWinApi1 | 01MB |   3516.10 |     326.17 |      215.74 |     3400.10 |  3345.30 |  3935.90 |
      | WriteFileWinApi2 | 01MB |   3898.60 |      61.57 |       40.73 |     3911.20 |  3839.80 |  3955.50 |
      | FileStreamWrite2 | 01MB |  10002.10 |     639.94 |      423.28 |     9996.50 |  9391.20 | 10571.90 |

    - <details>
      <summary>FileStreamWrite1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data to a file with the FileStream class.
        /// The write function is tested here.
        /// This first test writes out the file with the FileOptions set to none.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void FileStreamWrite1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>BinaryWriter1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data from a file with the BinaryReader class.
        /// This function uses a char buf.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void BinaryWriter1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>WriteFileWinApi1</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function WriteFile.  This function tests the WriteFile function
        /// by writing out the entire file with one call.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void WriteFileWinApi1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>WriteFileWinApi2</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function WriteFile.  
        /// This function tests the WriteFile function
        /// by writing out the file in blocks. 
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>>
        public void WriteFileWinApi2(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>FileStreamWrite2</summary>

      ```cs
        /// <summary>
        /// This function tests writing data to a file with the FileStream class.
        /// The write function is tested here.
        /// This test writes out the file with the FileOptions set to WriteThrough.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void FileStreamWrite2(string pathToFile, int bytesInFile)
      ```
      </details>
  </details>

  <details>
      <summary>
        10 MB.txt Top 5 results SingleWriteFileClearIoCacheBenchmark
      </summary>
      
      | Method           | Size | Mean (us) | Error (us) | StdDev (us) | Median (us) | Min (us) | Max (us) |
      | ---------------- | ---: | --------: | ---------: | ----------: | ----------: | -------: | -------: |
      | FileStreamWrite1 | 10MB |   8536.70 |      75.49 |       49.93 |     8518.80 |  8481.00 |  8641.50 |
      | BinaryWriter1    | 10MB |   8988.70 |     367.58 |      243.13 |     8850.30 |  8773.10 |  9504.60 |
      | WriteFileWinApi1 | 10MB |   9894.90 |     519.34 |      343.51 |     9775.10 |  9695.40 | 10836.00 |
      | WriteFileWinApi2 | 10MB |  11863.70 |     704.96 |      466.29 |    11624.40 | 11532.00 | 13021.50 |
      | WriteAllText     | 10MB |  40674.50 |    2420.75 |     1601.18 |    40664.00 | 37812.00 | 43260.80 |

    - <details>
      <summary>FileStreamWrite1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data to a file with the FileStream class.
        /// The write function is tested here.
        /// This first test writes out the file with the FileOptions set to none.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void FileStreamWrite1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>BinaryWriter1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data from a file with the BinaryReader class.
        /// This function uses a char buf.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void BinaryWriter1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>WriteFileWinApi1</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function WriteFile.  This function tests the WriteFile function
        /// by writing out the entire file with one call.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void WriteFileWinApi1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>WriteFileWinApi2</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function WriteFile.  This function tests the WriteFile function
        /// by writing out the file in blocks. 
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>>
        public void WriteFileWinApi2(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>WriteAllText</summary>

      ```cs
        /// <summary>
        /// This function tests out how quickly the File.WriteAllText method returns all the lines in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="content">Content to write.</param>
        public void WriteAllText(string pathToFile, string content)
      ```
      </details>
  </details>

  <details>
      <summary>
        50 MB.txt Top 5 results SingleWriteFileClearIoCacheBenchmark
      </summary>
      
      | Method        | Size | Mean (us) | Error (us) | StdDev (us) | Median (us) | Min (us) |  Max (us) |
      | ------------- | ---: | --------: | ---------: | ----------: | ----------: | -------: | --------: |
      | WriteAllLines | 50MB | 1680155.3 | 4273387.14 |     2826581 |    257912.7 | 240146.6 | 7992609.2 |
      | WriteAllText  | 50MB |  918435.1 | 3318239.87 |  2194810.22 |    196926.4 | 179699.8 | 7159727.3 |
      | WriteAllBytes | 50MB |  350087.1 |   59206.15 |     39161.2 |    339577.8 | 299050.2 |  416302.8 |
      | BinaryWriter1 | 50MB |  527574.8 |  974968.99 |   644881.62 |    306752.5 | 276182.7 | 2353039.7 |
      | StreamWriter1 | 50MB |  208328.7 |    14220.6 |     9406.05 |    208102.7 | 189956.3 |  222119.5 |

    - <details>
      <summary>WriteAllLines</summary>

      ```cs
        /// <summary>
        /// This function tests out how quickly the File.WriteAllLines method returns all the lines in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="content">Content to write.</param>
        public void WriteAllLines(string pathToFile, string[] content)
      ```
      </details>

    - <details>
      <summary>WriteAllText</summary>

      ```cs
        /// <summary>
        /// This function tests out how quickly the File.WriteAllText method returns all the lines in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="content">Content to write.</param>
        public void WriteAllText(string pathToFile, string content)
      ```
      </details>

    - <details>
      <summary>WriteAllBytes</summary>

      ```cs
        /// <summary>
        /// This function tests out how quickly the File.WriteAllBytes method returns all the lines in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        public void WriteAllBytes(string pathToFile)
      ```
      </details>

    - <details>
      <summary>BinaryWriter1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data from a file with the BinaryReader class.
        /// This function uses a char buf.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void BinaryWriter1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>StreamWriter1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data to a file with the StreamReader class.
        /// The write function is tested here.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void StreamWriter1(string pathToFile, int bytesInFile)
      ```
      </details>
  </details>

* [SingleWriteFileBenchmark](BenchmarkResults/Win32FileIOBenchmarkDotNet.Benchmarks.Write.SingleWriteFileBenchmark-report-github.md)
  <details>
      <summary>
        01 MB.txt Top 5 results SingleWriteFileBenchmark
      </summary>
  
      | Method           | Size | Mean (us) | Error (us) | StdDev (us) | Median (us) | Min (us) | Max (us) |
      | ---------------- | ---: | --------: | ---------: | ----------: | ----------: | -------: | -------: |
      | FileStreamWrite1 | 01MB |   2314.40 |      85.39 |       56.48 |     2293.10 |  2258.80 |  2445.90 |
      | BinaryWriter1    | 01MB |   3912.20 |    5807.67 |     3841.41 |     2612.00 |  2568.10 | 14822.20 |
      | WriteAllText     | 01MB |   3923.60 |     146.85 |       97.13 |     3923.80 |  3717.10 |  4050.20 |
      | WriteFileWinApi1 | 01MB |   4544.70 |     126.42 |       83.62 |     4524.20 |  4434.60 |  4693.50 |
      | WriteAllLines    | 01MB |   4720.00 |     838.16 |      554.39 |     4560.30 |  4424.80 |  6291.70 |

    - <details>
      <summary>FileStreamWrite1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data to a file with the FileStream class.
        /// The write function is tested here.
        /// This first test writes out the file with the FileOptions set to none.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void FileStreamWrite1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>BinaryWriter1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data from a file with the BinaryReader class.
        /// This function uses a char buf.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void BinaryWriter1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>WriteAllText</summary>

      ```cs
        /// <summary>
        /// This function tests out how quickly the File.WriteAllText method returns all the lines in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="content">Content to write.</param>
        public void WriteAllText(string pathToFile, string content)
      ```
      </details>

    - <details>
      <summary>WriteFileWinApi1</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function WriteFile.  This function tests the WriteFile function
        /// by writing out the entire file with one call.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void WriteFileWinApi1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>WriteAllLines</summary>

      ```cs
        /// <summary>
        /// This function tests out how quickly the File.WriteAllLines method returns all the lines in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="content">Content to write.</param>
        public void WriteAllLines(string pathToFile, string[] content)
      ```
      </details>
  </details>

  <details>
      <summary>
        10 MB.txt Top 5 results SingleWriteFileBenchmark
      </summary>
    
      | Method           | Size | Mean (us) | Error (us) | StdDev (us) | Median (us) | Min (us) |   Max (us) |
      | ---------------- | ---: | --------: | ---------: | ----------: | ----------: | -------: | ---------: |
      | BinaryWriter1    | 10MB |  10175.50 |    1217.32 |      805.18 |     9996.00 |  9113.20 |   12282.10 |
      | WriteFileWinApi1 | 10MB |  11926.40 |     487.73 |      322.61 |    11975.90 | 11096.30 |   12406.70 |
      | WriteFileWinApi2 | 10MB |  14067.60 |    1943.67 |     1285.62 |    13768.20 | 12643.20 |   17568.60 |
      | FileStreamWrite2 | 10MB |  87567.80 |    4478.38 |     2962.17 |    86482.90 | 84144.80 |   93218.30 |
      | FileStreamWrite1 | 10MB | 211413.50 |  964096.71 |   637690.28 |     9685.30 |  9596.00 | 2026311.80 |

    - <details>
      <summary>BinaryWriter1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data from a file with the BinaryReader class.
        /// This function uses a char buf.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void BinaryWriter1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>WriteFileWinApi1</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function WriteFile.  This function tests the WriteFile function
        /// by writing out the entire file with one call.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void WriteFileWinApi1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>WriteFileWinApi2</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function WriteFile.  This function tests the WriteFile function
        /// by writing out the file in blocks. 
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>>
        public void WriteFileWinApi2(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>FileStreamWrite2</summary>

      ```cs
        /// <summary>
        /// This function tests writing data to a file with the FileStream class.
        /// The write function is tested here.
        /// This test writes out the file with the FileOptions set to WriteThrough.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void FileStreamWrite2(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>FileStreamWrite1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data to a file with the FileStream class.
        /// The write function is tested here.
        /// This first test writes out the file with the FileOptions set to none.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void FileStreamWrite1(string pathToFile, int bytesInFile)
      ```
      </details>
  </details>

  <details>
      <summary>
        50 MB.txt Top 5 results SingleWriteFileBenchmark
      </summary>
      
      | Method           | Size | Mean (us) | Error (us) | StdDev (us) | Median (us) |  Min (us) |   Max (us) |
      | ---------------- | ---: | --------: | ---------: | ----------: | ----------: | --------: | ---------: |
      | WriteFileWinApi2 | 50MB | 170213.20 |  572169.31 |   378454.57 |    51224.70 |  45536.40 | 1247301.90 |
      | BinaryWriter1    | 50MB | 319342.80 |   17115.85 |    11321.07 |   318113.20 | 303101.80 |  334163.60 |
      | WriteAllBytes    | 50MB | 628875.50 | 1176325.45 |   778066.45 |   354542.80 | 320899.00 | 2830180.30 |
      | FileStreamWrite2 | 50MB | 687819.30 | 1166354.96 |   771471.59 |   428375.70 | 416123.20 | 2881940.40 |
      | WriteFileWinApi1 | 50MB | 759861.70 | 1048295.86 |   693382.80 |   348417.10 | 301008.60 | 1961758.30 |

    - <details>
      <summary>WriteFileWinApi2</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function WriteFile.  This function tests the WriteFile function
        /// by writing out the file in blocks. 
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>>
        public void WriteFileWinApi2(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>BinaryWriter1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data from a file with the BinaryReader class.
        /// This function uses a char buf.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void BinaryWriter1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>WriteAllBytes</summary>

      ```cs
        /// <summary>
        /// This function tests out how quickly the File.WriteAllBytes method returns all the lines in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        public void WriteAllBytes(string pathToFile)
      ```
      </details>

    - <details>
      <summary>FileStreamWrite2</summary>

      ```cs
        /// <summary>
        /// This function tests writing data to a file with the FileStream class.
        /// The write function is tested here.
        /// This test writes out the file with the FileOptions set to WriteThrough.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void FileStreamWrite2(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>WriteFileWinApi1</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function WriteFile.  This function tests the WriteFile function
        /// by writing out the entire file with one call.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void WriteFileWinApi1(string pathToFile, int bytesInFile)
      ```
      </details>
  </details>

* [AverageWriteFileBenchmark](BenchmarkResults/Win32FileIOBenchmarkDotNet.Benchmarks.Write.AverageWriteFileBenchmark-report-github.md)
  <details>
      <summary>
        01 MB.txt Top 5 results AverageWriteFileBenchmark
      </summary>
      
      | Method           | Size | Mean (us) | Error (us) | StdDev (us) | Median (us) | Min (us) | Max (us) |
      | ---------------- | ---: | --------: | ---------: | ----------: | ----------: | -------: | -------: |
      | BinaryWriter1    | 01MB |    653.25 |      43.19 |       28.57 |      648.91 |   615.79 |   712.35 |
      | FileStreamWrite1 | 01MB |    653.71 |      31.99 |       21.16 |      658.24 |   615.32 |   678.76 |
      | WriteFileWinApi2 | 01MB |    779.86 |      38.57 |       25.51 |      775.80 |   749.67 |   820.11 |
      | WriteAllText     | 01MB |   2468.41 |      78.51 |       51.93 |     2454.05 |  2415.56 |  2583.03 |
      | WriteAllLines    | 01MB |   3293.99 |     492.10 |      325.50 |     3188.56 |  3143.77 |  4206.94 |

    - <details>
      <summary>BinaryWriter1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data from a file with the BinaryReader class.
        /// This function uses a char buf.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void BinaryWriter1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>FileStreamWrite1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data to a file with the FileStream class.
        /// The write function is tested here.
        /// This first test writes out the file with the FileOptions set to none.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void FileStreamWrite1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>WriteFileWinApi2</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function WriteFile.  This function tests the WriteFile function
        /// by writing out the file in blocks. 
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>>
        public void WriteFileWinApi2(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>WriteAllText</summary>

      ```cs
        /// <summary>
        /// This function tests out how quickly the File.WriteAllText method returns all the lines in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="content">Content to write.</param>
        public void WriteAllText(string pathToFile, string content)
      ```
      </details>

    - <details>
      <summary>WriteAllLines</summary>

      ```cs
        /// <summary>
        /// This function tests out how quickly the File.WriteAllLines method returns all the lines in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="content">Content to write.</param>
        public void WriteAllLines(string pathToFile, string[] content)
      ```
      </details>
  </details>

  <details>
      <summary>
        10 MB.txt Top 5 results AverageWriteFileBenchmark
      </summary>
      
      | Method           | shortFileName |     Mean |   Error |  StdDev |   Median |      Min |      Max |
      | ---------------- | ------------: | -------: | ------: | ------: | -------: | -------: | -------: |
      | FileStreamWrite1 |     10 MB.txt |  6022.76 |  403.78 |  267.07 |  5950.03 |  5829.44 |  6772.71 |
      | BinaryWriter1    |     10 MB.txt |  6169.29 |  253.31 |  167.55 |  6100.24 |  6040.30 |  6572.11 |
      | WriteFileWinApi2 |     10 MB.txt |  7417.33 |  791.89 |  523.79 |  7197.93 |  7067.54 |  8762.82 |
      | StreamWriter1    |     10 MB.txt | 33195.19 | 5249.93 | 3472.50 | 32011.76 | 31317.83 | 42849.35 |
      | WriteAllText     |     10 MB.txt | 33622.64 | 1244.21 |  822.97 | 33395.18 | 32919.34 | 35500.04 |

    - <details>
      <summary>FileStreamWrite1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data to a file with the FileStream class.
        /// The write function is tested here.
        /// This first test writes out the file with the FileOptions set to none.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void FileStreamWrite1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>BinaryWriter1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data from a file with the BinaryReader class.
        /// This function uses a char buf.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void BinaryWriter1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>WriteFileWinApi2</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function WriteFile.  This function tests the WriteFile function
        /// by writing out the file in blocks. 
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>>
        public void WriteFileWinApi2(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>StreamWriter1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data to a file with the StreamReader class.
        /// The write function is tested here.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void StreamWriter1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>WriteAllText</summary>

      ```cs
        /// <summary>
        /// This function tests out how quickly the File.WriteAllText method returns all the lines in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="content">Content to write.</param>
        public void WriteAllText(string pathToFile, string content)
      ```
      </details>
  </details>

  <details>
      <summary>
        50 MB.txt Top 5 results AverageWriteFileBenchmark
      </summary>
      
      | Method        | Size |  Mean (us) | Error (us) | StdDev (us) | Median (us) |  Min (us) |   Max (us) |
      | ------------- | ---: | ---------: | ---------: | ----------: | ----------: | --------: | ---------: |
      | WriteAllLines | 50MB |  224794.42 |   15818.02 |    10462.64 |   219490.27 | 216702.44 |  247293.93 |
      | WriteAllText  | 50MB |  169526.03 |   12540.60 |     8294.83 |   166084.16 | 164446.96 |  190636.05 |
      | WriteAllBytes | 50MB | 1131659.83 | 2352696.00 |  1556162.73 |   379703.01 | 325478.65 | 5144793.14 |
      | BinaryWriter1 | 50MB |  367866.02 |   88355.42 |    58441.64 |   368482.64 | 302041.94 |  463004.58 |
      | StreamWriter1 | 50MB |  846513.28 | 2272739.97 |  1503276.77 |   170460.44 | 152909.38 | 4576251.29 |

    - <details>
      <summary>WriteAllLines</summary>

      ```cs
        /// <summary>
        /// This function tests out how quickly the File.WriteAllLines method returns all the lines in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="content">Content to write.</param>
        public void WriteAllLines(string pathToFile, string[] content)
      ```
      </details>

    - <details>
      <summary>WriteAllText</summary>

      ```cs
        /// <summary>
        /// This function tests out how quickly the File.WriteAllText method returns all the lines in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="content">Content to write.</param>
        public void WriteAllText(string pathToFile, string content)
      ```
      </details>

    - <details>
      <summary>WriteAllBytes</summary>

      ```cs
        /// <summary>
        /// This function tests out how quickly the File.WriteAllBytes method returns all the lines in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        public void WriteAllBytes(string pathToFile)
      ```
      </details>

    - <details>
      <summary>BinaryWriter1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data from a file with the BinaryReader class.
        /// This function uses a char buf.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void BinaryWriter1(string pathToFile, int bytesInFile)
      ```
      </details>

    - <details>
      <summary>StreamWriter1</summary>

      ```cs
        /// <summary>
        /// This function tests writing data to a file with the StreamReader class.
        /// The write function is tested here.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        public void StreamWriter1(string pathToFile, int bytesInFile)
      ```
      </details>
  </details>

### Read

* [SingleReadFileClearIoCacheBenchmark](BenchmarkResults/Win32FileIOBenchmarkDotNet.Benchmarks.Read.SingleReadFileClearIoCacheBenchmark-report-github.md)
  <details>
      <summary>
        01 MB.txt Top 5 results SingleReadFileClearIoCacheBenchmark
      </summary>
    
      | Method                   | Size | Mean (us) | Error (us) | StdDev (us) | Median (us) | Min (us) | Max (us) |
      | ------------------------ | ---: | --------: | ---------: | ----------: | ----------: | -------: | -------: |
      | ReadAllBytes             | 01MB |   7804.60 |    1712.92 |     1132.99 |     7569.30 |  7149.60 | 10960.10 |
      | ReadAllLines             | 01MB |  10998.20 |     922.46 |      610.15 |    10984.50 | 10119.90 | 11905.20 |
      | StreamReader1            | 01MB |  11260.70 |    1099.13 |      727.01 |    11292.40 | 10387.20 | 12169.20 |
      | StreamReader6            | 01MB |  11318.40 |     839.33 |      555.17 |    11293.10 | 10467.40 | 12272.80 |
      | BinaryReader1NoOpenClose | 01MB |  11369.40 |     756.60 |      500.44 |    11509.60 | 10313.00 | 12046.50 |

    - <details>
      <summary>ReadAllBytes</summary>

      ```cs
        /// <summary>
        /// using the ReadAllBytes function.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadAllBytes(string pathToFile)
      ```
      </details>

    - <details>
      <summary>ReadAllLines</summary>

      ```cs
        /// <summary>
        /// This function tests out how quickly the File.ReadAllLines method returns all the lines in a file
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ReadAllLines(string pathToFile)
      ```
      </details>

    - <details>
      <summary>StreamReader1</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the StreamReader class.  The Read function is tested here.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void StreamReader1(string pathToFile)
      ```
      </details>

    - <details>
      <summary>StreamReader6</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the StreamReader class.  The ReadLine function is tested here
        /// using the ReadLine function.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void StreamReader6(string pathToFile)
      ```
      </details>

    - <details>
      <summary>BinaryReader1NoOpenClose</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the BinaryReader class.
        /// This function is exactly the same as BinaryReader1, except it measures the time taken after
        /// opening the file and before closing it.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void BinaryReader1NoOpenClose(string pathToFile)
      ```
      </details>
  </details>

  <details>
      <summary>
        10 MB.txt Top 5 results SingleReadFileClearIoCacheBenchmark
      </summary>
      
      | Method                    | Size | Mean (us) | Error (us) | StdDev (us) | Median (us) | Min (us) |  Max (us) |
      | ------------------------- | ---: | --------: | ---------: | ----------: | ----------: | -------: | --------: |
      | ReadAllBytes              | 10MB |  66965.80 |    6688.36 |     4423.94 |    66947.30 | 60573.70 |  73943.30 |
      | FileStreamRead3           | 10MB |  72316.90 |    3598.15 |     2379.95 |    73046.90 | 67850.30 |  74801.20 |
      | FileStreamRead1           | 10MB |  73348.80 |    4526.79 |     2994.19 |    74033.80 | 68742.70 |  77763.50 |
      | ReadFileWinApi1           | 10MB |  74936.30 |    3883.23 |     2568.52 |    75717.60 | 70396.40 |  78342.40 |
      | ReadFileWinApiNoOpenClose | 10MB |  77988.10 |   16743.18 |    11074.58 |    75644.80 | 70208.40 | 108690.80 |

    - <details>
      <summary>ReadAllBytes</summary>

      ```cs
        /// <summary>
        /// using the ReadAllBytes function.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadAllBytes(string pathToFile)
      ```
      </details>

    - <details>
      <summary>FileStreamRead3</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the FileStream class.  The Read function is tested here.
        /// This test reads in the entire file using the RandomAccess option in the constructor.  No parsing is done here.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void FileStreamRead3(string pathToFile)
      ```
      </details>

    - <details>
      <summary>FileStreamRead1</summary>

      ```cs
        /// <summary>
        // This function tests reading data from a file with the FileStream class.  The Read function is tested here.
        // This first test reads in the entire file using the Sequential Scan option in the constructor.  No parsing of
        // lines is done here.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void FileStreamRead1(string pathToFile)
      ```
      </details>

    - <details>
      <summary>ReadFileWinApi1</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function ReadFile.  This function tests the ReadFile function
        /// by reading in the entire file with one call.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadFileWinApi1(string pathToFile)
      ```
      </details>

    - <details>
      <summary>ReadFileWinApiNoOpenClose</summary>

      ```cs
        /// <summary>
        /// This function is similar to ReadFileWinAPI, except that it times only the reading of the file.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadFileWinApiNoOpenClose(string pathToFile)
      ```
      </details>
  </details>

  <details>
      <summary>
        50 MB.txt Top 5 results SingleReadFileClearIoCacheBenchmark
      </summary>
  
      | Method                     | Size | Mean (us) | Error (us) | StdDev (us) | Median (us) | Min (us) | Max (us) |
      | -------------------------- | ---: | --------: | ---------: | ----------: | ----------: | -------: | -------: |
      | FileStreamRead3            | 50MB |  273887.3 |    7532.64 |     4982.38 |      274256 | 267308.4 | 280156.4 |
      | FileStreamRead1            | 50MB |  278235.6 |   12258.46 |     8108.21 |    275216.1 | 271777.0 |   296443 |
      | ReadAllBytes               | 50MB |  278368.3 |    3912.62 |     2587.95 |    279020.4 | 274651.6 | 282323.7 |
      | FileStreamRead1NoOpenClose | 50MB |  280308.5 |    9152.91 |     6054.08 |    279104.4 | 272117.6 | 290348.1 |
      | ReadFileWinApiNoOpenClose  | 50MB |  281276.8 |    7345.35 |     4858.49 |    279934.1 | 274836.3 | 291136.5 |

    - <details>
      <summary>FileStreamRead3</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the FileStream class.  The Read function is tested here.
        /// This test reads in the entire file using the RandomAccess option in the constructor.  No parsing is done here.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void FileStreamRead3(string pathToFile)
      ```
      </details>

    - <details>
      <summary>FileStreamRead1</summary>

      ```cs
        /// <summary>
        // This function tests reading data from a file with the FileStream class.  The Read function is tested here.
        // This first test reads in the entire file using the Sequential Scan option in the constructor.  No parsing of
        // lines is done here.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void FileStreamRead1(string pathToFile)
      ```
      </details>

    - <details>
      <summary>ReadAllBytes</summary>

      ```cs
        /// <summary>
        /// using the ReadAllBytes function.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadAllBytes(string pathToFile)
      ```
      </details>

    - <details>
      <summary>FileStreamRead1NoOpenClose</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the FileStream class.
        /// This function is exactly the same as FileStreamRead1, except it measures the time taken after
        /// opening the file and before closing it.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void FileStreamRead1NoOpenClose(string pathToFile)
      ```
      </details>

    - <details>
      <summary>ReadFileWinApiNoOpenClose</summary>

      ```cs
        /// <summary>
        /// This function is similar to ReadFileWinAPI, except that it times only the reading of the file.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadFileWinApiNoOpenClose(string pathToFile)
      ```
      </details>
  </details>

* [SingleReadFileBenchmark](BenchmarkResults/Win32FileIOBenchmarkDotNet.Benchmarks.Read.SingleReadFileBenchmark-report-github.md)
  <details>
      <summary>
        01 MB.txt Top 5 results SingleReadFileBenchmark
      </summary>
      
      | Method                   | Size | Mean (us) | Error (us) | StdDev (us) | Median (us) | Min (us) | Max (us) |
      | ------------------------ | ---: | --------: | ---------: | ----------: | ----------: | -------: | -------: |
      | ReadAllBytes             | 01MB |   2310.00 |    3428.91 |     2268.01 |     1588.40 |  1536.70 |  8763.30 |
      | StreamReader3            | 01MB |   4030.60 |    3417.19 |     2260.26 |     3301.20 |  3216.10 | 10459.50 |
      | StreamReader5            | 01MB |   4116.60 |    3936.54 |     2603.78 |     3280.70 |  3210.50 | 11523.10 |
      | StreamReader2NoOpenClose | 01MB |   4127.80 |    4026.72 |     2663.42 |     3269.00 |  3222.10 | 11706.50 |
      | StreamReader2            | 01MB |   4208.90 |    3532.28 |     2336.39 |     3382.60 |  3249.20 | 10784.60 |

    - <details>
      <summary>ReadAllBytes</summary>

      ```cs
        /// <summary>
        /// using the ReadAllBytes function.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadAllBytes(string pathToFile)
      ```
      </details>

    - <details>
      <summary>StreamReader3</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the StreamReader class.  The ReadBlock function is tested here.
        /// Get how fast it takes to read in the .683MB, 10MB, and 50MB files into memory.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void StreamReader3(string pathToFile)
      ```
      </details>

    - <details>
      <summary>StreamReader5</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the StreamReader class.  The Read function is tested here
        /// using a smaller buffer size and reading into memory just BlockSize bytes each time until the entire file is
        /// read into memory.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void StreamReader5(string pathToFile)
      ```
      </details>

    - <details>
      <summary>StreamReader2NoOpenClose</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the StreamReader class.
        /// This function is exactly the same as StreamReader2, except it measures the time taken after
        /// opening the file and before closing it.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void StreamReader2NoOpenClose(string pathToFile)
      ```
      </details>

    - <details>
      <summary>StreamReader2</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the StreamReader class.  The Read function is tested here.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void StreamReader2(string pathToFile)
      ```
      </details>
  </details>

  <details>
      <summary>
        10 MB.txt Top 5 results SingleReadFileBenchmark
      </summary>
      
      | Method          | Size | Mean (us) | Error (us) | StdDev (us) | Median (us) | Min (us) |  Max (us) |
      | --------------- | ---: | --------: | ---------: | ----------: | ----------: | -------: | --------: |
      | ReadAllBytes    | 10MB |  13849.40 |   27035.97 |    17882.62 |     8162.40 |  8069.10 |  64742.90 |
      | FileStreamRead3 | 10MB |  21100.50 |   25342.47 |    16762.48 |    15667.10 | 15568.20 |  68798.20 |
      | ReadFileWinApi3 | 10MB |  21803.20 |   47024.97 |    31104.11 |    11900.30 | 11579.60 | 110323.10 |
      | ReadFileWinApi2 | 10MB |  21942.40 |   48630.84 |    32166.29 |    11797.90 | 11454.50 | 113486.50 |
      | FileStreamRead4 | 10MB |  22120.90 |   36099.16 |    23877.36 |    14502.90 | 13522.10 |  90052.10 |

    - <details>
      <summary>ReadAllBytes</summary>

      ```cs
        /// <summary>
        /// using the ReadAllBytes function.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadAllBytes(string pathToFile)
      ```
      </details>

    - <details>
      <summary>FileStreamRead3</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the FileStream class.  The Read function is tested here.
        /// This test reads in the entire file using the RandomAccess option in the constructor.  No parsing is done here.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void FileStreamRead3(string pathToFile)
      ```
      </details>

    - <details>
      <summary>ReadFileWinApi3</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function ReadFile.  This test tests out the ReadUntilEOF method
        /// in the WinFileIO class.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadFileWinApi3(string pathToFile)
      ```
      </details>

    - <details>
      <summary>ReadFileWinApi2</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function ReadFile.  This test tests out the ReadUntilEOF method
        /// in the WinFileIO class.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadFileWinApi2(string pathToFile)
      ```
      </details>

    - <details>
      <summary>FileStreamRead4</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the FileStream class.  The async BeginRead function is tested here.
        /// This test reads in each file asynchronously reading AsyncBufSize bytes at a time.  No parsing is done.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void FileStreamRead4(string pathToFile)
      ```
      </details>
  </details>

  <details>
      <summary>
        50 MB.txt Top 5 results SingleReadFileBenchmark
      </summary>
      
      | Method                     | Size | Mean (us) | Error (us) | StdDev (us) | Median (us) | Min (us) |  Max (us) |
      | -------------------------- | ---: | --------: | ---------: | ----------: | ----------: | -------: | --------: |
      | FileStreamRead3            | 50MB |  51835.10 |  116935.03 |    77345.28 |    26936.70 | 26565.60 | 271922.60 |
      | ReadAllBytes               | 50MB |  61713.50 |  120498.37 |    79702.21 |    36568.30 | 35995.50 | 288548.30 |
      | FileStreamRead1            | 50MB |  62348.40 |  111738.12 |    73907.85 |    37954.80 | 37335.70 | 272578.00 |
      | FileStreamRead1NoOpenClose | 50MB |  63138.10 |  121750.67 |    80530.53 |    37636.60 | 36812.80 | 292325.60 |
      | ReadFileWinApiNoOpenClose  | 50MB |  63172.20 |  114893.13 |    75994.69 |    38964.10 | 38558.40 | 279451.50 |

    - <details>
      <summary>FileStreamRead3</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the FileStream class.  The Read function is tested here.
        /// This test reads in the entire file using the RandomAccess option in the constructor.  No parsing is done here.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void FileStreamRead3(string pathToFile)
      ```
      </details>

    - <details>
      <summary>ReadAllBytes</summary>

      ```cs
        /// <summary>
        /// using the ReadAllBytes function.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadAllBytes(string pathToFile)
      ```
      </details>

    - <details>
      <summary>FileStreamRead1</summary>

      ```cs
        /// <summary>
        // This function tests reading data from a file with the FileStream class.  The Read function is tested here.
        // This first test reads in the entire file using the Sequential Scan option in the constructor.  No parsing of
        // lines is done here.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void FileStreamRead1(string pathToFile)
      ```
      </details>

    - <details>
      <summary>FileStreamRead1NoOpenClose</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the FileStream class.
        /// This function is exactly the same as FileStreamRead1, except it measures the time taken after
        /// opening the file and before closing it.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void FileStreamRead1NoOpenClose(string pathToFile)
      ```
      </details>

    - <details>
      <summary>ReadFileWinApiNoOpenClose</summary>

      ```cs
        /// <summary>
        /// This function is similar to ReadFileWinAPI, except that it times only the reading of the file.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadFileWinApiNoOpenClose(string pathToFile)
      ```
      </details>
  </details>

* [AverageReadFileBenchmark](BenchmarkResults/Win32FileIOBenchmarkDotNet.Benchmarks.Read.AverageReadFileBenchmark-report-github.md)
  <details>
      <summary>
        01 MB.txt Top 5 results AverageReadFileBenchmark
      </summary>
    
      | Method                    | Size | Mean (us) | Error (us) | StdDev (us) | Median (us) | Min (us) | Max (us) |
      | ------------------------- | ---: | --------: | ---------: | ----------: | ----------: | -------: | -------: |
      | ReadFileWinApi3           | 01MB |    229.29 |      43.51 |       28.78 |      217.39 |   201.53 |   289.23 |
      | ReadFileWinApi2           | 01MB |    238.62 |      46.37 |       30.67 |      227.19 |   202.46 |   295.76 |
      | ReadAllBytes              | 01MB |    342.69 |      50.32 |       33.28 |      328.19 |   313.02 |   414.26 |
      | ReadFileWinApiNoOpenClose | 01MB |    495.80 |      65.06 |       43.03 |      486.33 |   454.38 |   578.00 |
      | ReadFileWinApi1           | 01MB |    505.92 |      86.57 |       57.26 |      477.93 |   453.91 |   612.52 |

    - <details>
      <summary>ReadFileWinApi3</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function ReadFile.  This test tests out the ReadUntilEOF method
        /// in the WinFileIO class.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadFileWinApi3(string pathToFile)
      ```
      </details>

    - <details>
      <summary>ReadFileWinApi2</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function ReadFile.  This test tests out the ReadUntilEOF method
        /// in the WinFileIO class.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadFileWinApi2(string pathToFile)
      ```
      </details>

    - <details>
      <summary>ReadAllBytes</summary>

      ```cs
        /// <summary>
        /// using the ReadAllBytes function.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadAllBytes(string pathToFile)
      ```
      </details>

    - <details>
      <summary>ReadFileWinApiNoOpenClose</summary>

      ```cs
        /// <summary>
        /// This function is similar to ReadFileWinAPI, except that it times only the reading of the file.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadFileWinApiNoOpenClose(string pathToFile)
      ```
      </details>

    - <details>
      <summary>ReadFileWinApi1</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function ReadFile.  This function tests the ReadFile function
        /// by reading in the entire file with one call.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadFileWinApi1(string pathToFile)
      ```
      </details>
  </details>

  <details>
      <summary>
        10 MB.txt Top 5 results AverageReadFileBenchmark
      </summary>
      
      | Method        | Size | Mean (us) | Error (us) | StdDev (us) | Median (us) | Min (us) | Max (us) |
      | ------------- | ---: | --------: | ---------: | ----------: | ----------: | -------: | -------: |
      | ReadAllLines  | 10MB |   2999.02 |     284.89 |      188.44 |     2936.88 |  2931.05 |  3534.24 |
      | ReadAllText   | 10MB |   5084.20 |     255.34 |      168.89 |     5020.29 |  4960.34 |  5433.38 |
      | ReadAllBytes  | 10MB |   5165.23 |     389.86 |      257.87 |     5026.58 |  4959.41 |  5631.64 |
      | BinaryReader1 | 10MB |   5183.24 |     368.60 |      243.81 |     5099.83 |  4981.80 |  5739.40 |
      | StreamReader1 | 10MB |   5256.80 |     459.90 |      304.19 |     5153.24 |  5089.56 |  6098.14 |

    - <details>
      <summary>ReadAllLines</summary>

      ```cs
        /// <summary>
        /// This function tests out how quickly the File.ReadAllLines method returns all the lines in a file
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadAllLines(string pathToFile)
      ```
      </details>

    - <details>
      <summary>ReadAllText</summary>

      ```cs
        /// <summary>
        /// This function tests out how quickly the File.ReadAllText method returns all text in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadAllText(string pathToFile)
      ```
      </details>

    - <details>
      <summary>ReadAllBytes</summary>

      ```cs
        /// <summary>
        /// using the ReadAllBytes function.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadAllBytes(string pathToFile)
      ```
      </details>

    - <details>
      <summary>BinaryReader1NoOpenClose</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the BinaryReader class.
        /// This function is exactly the same as BinaryReader1, except it measures the time taken after
        /// opening the file and before closing it.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void BinaryReader1NoOpenClose(string pathToFile)
      ```
      </details>

    - <details>
      <summary>StreamReader1</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the StreamReader class.  The Read function is tested here.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void StreamReader1(string pathToFile)
      ```
      </details>
  </details>

  <details>
      <summary>
        50 MB.txt Top 5 results AverageReadFileBenchmark
      </summary>
      
      | Method                     |Size| Mean (us)|Error (us)|StdDev (us)|Median (us)| Min (us) | Max (us) |
      | -------------------------- | -: | -------: | -------: | --------: | --------: | -------: | -------: |
      | FileStreamRead3            |50MB| 13606.72 |  1119.07 |    740.19 |  13500.87 | 12896.98 | 14955.66 |
      | ReadFileWinApi1            |50MB| 24294.66 |   309.08 |    204.44 |  24349.89 | 23917.68 | 24489.14 |
      | FileStreamRead1            |50MB| 24316.21 |   273.09 |    180.63 |  24305.34 | 23992.78 | 24620.23 |
      | FileStreamRead1NoOpenClose |50MB| 24599.56 |  1328.02 |    878.41 |  24292.51 | 23878.02 | 26963.95 |
      | ReadFileWinApiNoOpenClose  |50MB| 24739.33 |  1979.29 |   1309.18 |  24261.02 | 23805.71 | 28243.10 |

    - <details>
      <summary>FileStreamRead3</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the FileStream class.  The Read function is tested here.
        /// This test reads in the entire file using the RandomAccess option in the constructor.  No parsing is done here.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void FileStreamRead3(string pathToFile)
      ```
      </details>






    - <details>
      <summary>ReadFileWinApi1</summary>

      ```cs
        /// <summary>
        /// This function tests out the Windows API function ReadFile.  This function tests the ReadFile function
        /// by reading in the entire file with one call.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadFileWinApi1(string pathToFile)
      ```
      </details>

    - <details>
      <summary>FileStreamRead1</summary>

      ```cs
        /// <summary>
        // This function tests reading data from a file with the FileStream class.  The Read function is tested here.
        // This first test reads in the entire file using the Sequential Scan option in the constructor.  No parsing of
        // lines is done here.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void FileStreamRead1(string pathToFile)
      ```
      </details>

    - <details>
      <summary>FileStreamRead1NoOpenClose</summary>

      ```cs
        /// <summary>
        /// This function tests reading data from a file with the FileStream class.
        /// This function is exactly the same as FileStreamRead1, except it measures the time taken after
        /// opening the file and before closing it.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void FileStreamRead1NoOpenClose(string pathToFile)
      ```
      </details>

    - <details>
      <summary>ReadFileWinApiNoOpenClose</summary>

      ```cs
        /// <summary>
        /// This function is similar to ReadFileWinAPI, except that it times only the reading of the file.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        public void ReadFileWinApiNoOpenClose(string pathToFile)
      ```
      </details>
  </details>

## License and disclaimer

These tests is free to use by any individual or entity for any endeavor for profit or not. Even though this code has been tested and automated unit tests are provided, there is no gaurantee that it will run correctly with your system or environment.  I am not responsible for any failure and you agree that you accept any and all risk for using this software.

## Afterword

If you have ideas how to improve benchmarks, please, contact me at gitter.
