``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 7 SP1 (6.1.7601.0)
Intel Core i3-2330M CPU 2.20GHz (Sandy Bridge), 1 CPU, 4 logical and 2 physical cores
Frequency=2143603 Hz, Resolution=466.5043 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (4.7.3062.0), X86 LegacyJIT
  Job-WUNALH : .NET Framework 4.7.2 (4.7.3062.0), X64 RyuJIT

Jit=RyuJit  Platform=X64  Runtime=.NET 4.6.1  
InvocationCount=1  IterationCount=1  LaunchCount=10  
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=0  

```
|              Method|Size|          Mean|          Error|         StdDev|        Median|         Min|           Max|
|---------------------|--------------|---------------:|----------------:|----------------:|---------------:|-------------:|---------------:|
|     **ProduceName**|**01MB**|      **922.7 us**|      **230.51 us**|      **152.47 us**|      **870.3 us**|    **861.2 us**|    **1,354.3 us**|
|   WriteAllLines|01MB|   43,791.8 us|  188,390.95 us|  124,608.95 us|    4,278.5 us|  4,225.1 us|  398,433.9 us|
|    WriteAllText|01MB|   13,407.7 us|   46,010.25 us|   30,432.93 us|    3,633.8 us|  3,563.6 us|  100,011.5 us|
|   WriteAllBytes|01MB|  336,015.4 us|   48,221.45 us|   31,895.50 us|  327,857.1 us|292,548.1 us|  381,010.4 us|
|   BinaryWriter1|01MB|    2,561.4 us|      304.53 us|      201.43 us|    2,501.6 us|  2,461.7 us|    3,130.2 us|
|   StreamWriter1|01MB|   15,567.4 us|   54,562.04 us|   36,089.41 us|    4,019.2 us|  3,973.2 us|  118,273.3 us|
|FileStreamWrite1|01MB|    2,219.1 us|      190.82 us|      126.21 us|    2,179.5 us|  2,128.7 us|    2,566.2 us|
|FileStreamWrite2|01MB|   10,002.1 us|      639.94 us|      423.28 us|    9,996.5 us|  9,391.2 us|   10,571.9 us|
|FileStreamWrite3|01MB|   18,384.9 us|    1,706.92 us|    1,129.02 us|   18,206.0 us| 16,978.9 us|   20,104.9 us|
|WriteFileWinApi1|01MB|    3,516.1 us|      326.17 us|      215.74 us|    3,400.1 us|  3,345.3 us|    3,935.9 us|
|WriteFileWinApi2|01MB|    3,898.6 us|       61.57 us|       40.73 us|    3,911.2 us|  3,839.8 us|    3,955.5 us|
|     **ProduceName**|**10MB**|      **886.6 us**|       **32.90 us**|       **21.76 us**|      **883.3 us**|    **864.9 us**|      **928.8 us**|
|   WriteAllLines|10MB|  923,099.0 us|2,764,339.20 us|1,828,439.22 us|   56,298.9 us| 50,974.5 us|4,546,504.6 us|
|    WriteAllText|10MB|   40,674.5 us|    2,420.75 us|    1,601.18 us|   40,664.0 us| 37,812.0 us|   43,260.8 us|
|   WriteAllBytes|10MB|  509,992.1 us|  773,937.56 us|  511,911.77 us|  349,062.3 us|311,379.0 us|1,964,595.1 us|
|   BinaryWriter1|10MB|    8,988.7 us|      367.58 us|      243.13 us|    8,850.3 us|  8,773.1 us|    9,504.6 us|
|   StreamWriter1|10MB|  270,687.7 us|1,083,943.57 us|  716,961.56 us|   44,270.8 us| 41,908.9 us|2,311,191.5 us|
|FileStreamWrite1|10MB|    8,536.7 us|       75.49 us|       49.93 us|    8,518.8 us|  8,481.0 us|    8,641.5 us|
|FileStreamWrite2|10MB|   87,602.4 us|    5,074.90 us|    3,356.73 us|   85,817.7 us| 83,890.5 us|   92,527.9 us|
|FileStreamWrite3|10MB|  382,710.4 us|  736,263.45 us|  486,992.68 us|  206,744.7 us|198,034.8 us|1,753,493.5 us|
|WriteFileWinApi1|10MB|    9,894.9 us|      519.34 us|      343.51 us|    9,775.1 us|  9,695.4 us|   10,836.0 us|
|WriteFileWinApi2|10MB|   11,863.7 us|      704.96 us|      466.29 us|   11,624.4 us| 11,532.0 us|   13,021.5 us|
|     **ProduceName**|**50MB**|      **904.2 us**|      **116.68 us**|       **77.18 us**|      **874.5 us**|    **868.2 us**|    **1,120.5 us**|
|   WriteAllLines|50MB|1,680,155.3 us|4,273,387.14 us|2,826,581.00 us|  257,912.7 us|240,146.6 us|7,992,609.2 us|
|    WriteAllText|50MB|  918,435.1 us|3,318,239.87 us|2,194,810.22 us|  196,926.4 us|179,699.8 us|7,159,727.3 us|
|   WriteAllBytes|50MB|  350,087.1 us|   59,206.15 us|   39,161.20 us|  339,577.8 us|299,050.2 us|  416,302.8 us|
|   BinaryWriter1|50MB|  527,574.8 us|  974,968.99 us|  644,881.62 us|  306,752.5 us|276,182.7 us|2,353,039.7 us|
|   StreamWriter1|50MB|  208,328.7 us|   14,220.60 us|    9,406.05 us|  208,102.7 us|189,956.3 us|  222,119.5 us|
|FileStreamWrite1|50MB|  606,651.6 us|1,347,309.13 us|  891,161.57 us|  314,640.6 us|280,002.4 us|3,140,360.4 us|
|FileStreamWrite2|50MB|  656,534.5 us|  996,314.50 us|  659,000.35 us|  436,555.6 us|407,704.2 us|2,528,404.7 us|
|FileStreamWrite3|50MB|1,263,748.9 us|  536,079.13 us|  354,583.16 us|1,069,753.6 us|957,801.0 us|1,968,935.9 us|
|WriteFileWinApi1|50MB|  903,074.4 us|1,572,213.57 us|1,039,921.93 us|  317,877.7 us|302,650.7 us|3,100,890.4 us|
|WriteFileWinApi2|50MB|  129,739.5 us|  405,783.60 us|  268,400.72 us|   44,846.9 us| 44,286.2 us|  893,620.2 us|
