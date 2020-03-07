``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 7 SP1 (6.1.7601.0)
Intel Core i3-2330M CPU 2.20GHz (Sandy Bridge), 1 CPU, 4 logical and 2 physical cores
Frequency=2143603 Hz, Resolution=466.5043 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (4.7.3062.0), X86 LegacyJIT
  Job-WFPNTH : .NET Framework 4.7.2 (4.7.3062.0), X64 RyuJIT

Jit=RyuJit  Platform=X64  Runtime=.NET 4.6.1  
IterationCount=1  LaunchCount=10  RunStrategy=ColdStart  
WarmupCount=0  

```
|              Method|Size|          Mean|          Error|         StdDev|        Median|         Min|           Max|
|---------------------|--------------|---------------:|----------------:|----------------:|---------------:|-------------:|---------------:|
|     **ProduceName**|**01MB**|      **883.0 us**|       **29.76 us**|       **19.68 us**|      **878.0 us**|    **864.9 us**|      **928.3 us**|
|   WriteAllLines|01MB|    4,720.0 us|      838.16 us|      554.39 us|    4,560.3 us|  4,424.8 us|    6,291.7 us|
|    WriteAllText|01MB|    3,923.6 us|      146.85 us|       97.13 us|    3,923.8 us|  3,717.1 us|    4,050.2 us|
|   WriteAllBytes|01MB|  356,909.8 us|   48,481.67 us|   32,067.62 us|  351,315.3 us|309,331.5 us|  406,141.9 us|
|   BinaryWriter1|01MB|    3,912.2 us|    5,807.67 us|    3,841.41 us|    2,612.0 us|  2,568.1 us|   14,822.2 us|
|   StreamWriter1|01MB|    6,160.5 us|    9,043.90 us|    5,981.98 us|    4,276.0 us|  4,159.4 us|   23,184.8 us|
|FileStreamWrite1|01MB|    2,314.4 us|       85.39 us|       56.48 us|    2,293.1 us|  2,258.8 us|    2,445.9 us|
|FileStreamWrite2|01MB|   10,336.0 us|      587.00 us|      388.26 us|   10,300.9 us|  9,770.0 us|   11,107.5 us|
|FileStreamWrite3|01MB|   17,617.4 us|    1,707.17 us|    1,129.19 us|   17,140.5 us| 16,437.7 us|   19,741.5 us|
|WriteFileWinApi1|01MB|    4,544.7 us|      126.42 us|       83.62 us|    4,524.2 us|  4,434.6 us|    4,693.5 us|
|WriteFileWinApi2|01MB|    5,078.4 us|      204.35 us|      135.17 us|    5,023.6 us|  4,986.5 us|    5,427.3 us|
|     **ProduceName**|**10MB**|      **876.8 us**|       **20.47 us**|       **13.54 us**|      **873.3 us**|    **864.4 us**|      **906.0 us**|
|   WriteAllLines|10MB|  443,325.5 us|1,203,010.85 us|  795,717.19 us|   51,044.2 us| 48,521.6 us|2,378,063.5 us|
|    WriteAllText|10MB|  451,240.8 us|1,521,636.13 us|1,006,468.09 us|   37,471.3 us| 35,774.8 us|3,170,122.5 us|
|   WriteAllBytes|10MB|  559,033.8 us|  836,467.58 us|  553,271.51 us|  356,330.9 us|295,702.6 us|2,095,349.3 us|
|   BinaryWriter1|10MB|   10,175.5 us|    1,217.32 us|      805.18 us|    9,996.0 us|  9,113.2 us|   12,282.1 us|
|   StreamWriter1|10MB|  394,819.2 us|1,158,855.50 us|  766,511.16 us|   42,255.3 us| 39,669.2 us|2,213,868.0 us|
|FileStreamWrite1|10MB|  211,413.5 us|  964,096.71 us|  637,690.28 us|    9,685.3 us|  9,596.0 us|2,026,311.8 us|
|FileStreamWrite2|10MB|   87,567.8 us|    4,478.38 us|    2,962.17 us|   86,482.9 us| 84,144.8 us|   93,218.3 us|
|FileStreamWrite3|10MB|  223,694.7 us|   65,384.79 us|   43,247.99 us|  213,904.6 us|194,328.0 us|  345,237.4 us|
|WriteFileWinApi1|10MB|   11,926.4 us|      487.73 us|      322.61 us|   11,975.9 us| 11,096.3 us|   12,406.7 us|
|WriteFileWinApi2|10MB|   14,067.6 us|    1,943.67 us|    1,285.62 us|   13,768.2 us| 12,643.2 us|   17,568.6 us|
|     **ProduceName**|**50MB**|      **902.4 us**|       **40.10 us**|       **26.52 us**|      **904.3 us**|    **864.4 us**|      **960.1 us**|
|   WriteAllLines|50MB|1,451,571.5 us|2,930,548.07 us|1,938,376.10 us|  250,820.7 us|227,604.6 us|5,170,181.2 us|
|    WriteAllText|50MB|1,450,182.9 us|2,489,232.45 us|1,646,473.14 us|  603,642.1 us|172,933.1 us|4,505,169.6 us|
|   WriteAllBytes|50MB|  628,875.5 us|1,176,325.45 us|  778,066.45 us|  354,542.8 us|320,899.0 us|2,830,180.3 us|
|   BinaryWriter1|50MB|  319,342.8 us|   17,115.85 us|   11,321.07 us|  318,113.2 us|303,101.8 us|  334,163.6 us|
|   StreamWriter1|50MB|  929,100.2 us|1,552,769.94 us|1,027,061.17 us|  207,873.4 us|187,681.7 us|2,880,914.0 us|
|FileStreamWrite1|50MB|  831,488.6 us|1,357,650.76 us|  898,001.92 us|  340,205.7 us|314,534.5 us|2,700,009.7 us|
|FileStreamWrite2|50MB|  687,819.3 us|1,166,354.96 us|  771,471.59 us|  428,375.7 us|416,123.2 us|2,881,940.4 us|
|FileStreamWrite3|50MB|1,187,648.4 us|  483,256.49 us|  319,644.25 us|1,044,556.8 us|976,873.5 us|2,034,855.3 us|
|WriteFileWinApi1|50MB|  759,861.7 us|1,048,295.86 us|  693,382.80 us|  348,417.1 us|301,008.6 us|1,961,758.3 us|
|WriteFileWinApi2|50MB|  170,213.2 us|  572,169.31 us|  378,454.57 us|   51,224.7 us| 45,536.4 us|1,247,301.9 us|
