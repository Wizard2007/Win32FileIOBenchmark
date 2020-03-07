``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 7 SP1 (6.1.7601.0)
Intel Core i3-2330M CPU 2.20GHz (Sandy Bridge), 1 CPU, 4 logical and 2 physical cores
Frequency=2143603 Hz, Resolution=466.5043 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (4.7.3062.0), X86 LegacyJIT
  Job-EALFOD : .NET Framework 4.7.2 (4.7.3062.0), X64 RyuJIT

Jit=RyuJit  Platform=X64  Runtime=.NET 4.6.1  
IterationCount=10  LaunchCount=1  RunStrategy=Monitoring  
WarmupCount=1  

```
|              Method|Size|            Mean|           Error|          StdDev|        Median|           Min|            Max|
|---------------------|--------------|-----------------:|-----------------:|-----------------:|---------------:|---------------:|----------------:|
|     **ProduceName**|**01MB**|        **8.210 us**|        **3.599 us**|        **2.381 us**|      **7.464 us**|      **6.998 us**|       **14.93 us**|
|   WriteAllLines|01MB|    3,293.987 us|      492.104 us|      325.496 us|  3,188.557 us|  3,143.772 us|    4,206.94 us|
|    WriteAllText|01MB|    2,468.414 us|       78.507 us|       51.927 us|  2,454.046 us|  2,415.559 us|    2,583.03 us|
|   WriteAllBytes|01MB|  892,708.398 us|1,745,557.818 us|1,154,578.418 us|361,404.607 us|319,649.207 us|3,527,138.65 us|
|   BinaryWriter1|01MB|      653.246 us|       43.186 us|       28.565 us|    648.907 us|    615.786 us|      712.35 us|
|   StreamWriter1|01MB|    9,732.772 us|   34,969.407 us|   23,130.097 us|  2,404.596 us|  2,375.906 us|   75,562.03 us|
|FileStreamWrite1|01MB|      653.712 us|       31.992 us|       21.161 us|    658.238 us|    615.319 us|      678.76 us|
|FileStreamWrite2|01MB|  122,945.200 us|  546,115.900 us|  361,221.855 us|  8,449.326 us|  7,922.642 us|1,150,996.71 us|
|FileStreamWrite3|01MB|   17,059.222 us|    4,912.467 us|    3,249.293 us| 15,731.224 us| 15,381.580 us|   26,046.33 us|
|WriteFileWinApi1|01MB|   25,655.637 us|  119,612.485 us|   79,116.253 us|    640.510 us|    611.121 us|  250,824.43 us|
|WriteFileWinApi2|01MB|      779.855 us|       38.566 us|       25.509 us|    775.797 us|    749.672 us|      820.11 us|
|     **ProduceName**|**10MB**|        **8.397 us**|        **3.762 us**|        **2.488 us**|      **7.464 us**|      **6.998 us**|       **15.39 us**|
|   WriteAllLines|10MB|   44,408.036 us|    1,045.088 us|      691.261 us| 44,431.502 us| 43,607.422 us|   45,495.36 us|
|    WriteAllText|10MB|   33,622.644 us|    1,244.214 us|      822.970 us| 33,395.176 us| 32,919.342 us|   35,500.04 us|
|   WriteAllBytes|10MB|  751,845.421 us|1,616,794.005 us|1,069,409.129 us|349,186.860 us|315,597.151 us|3,728,651.25 us|
|   BinaryWriter1|10MB|    6,169.286 us|      253.313 us|      167.551 us|  6,100.243 us|  6,040.298 us|    6,572.11 us|
|   StreamWriter1|10MB|   33,195.186 us|    5,249.927 us|    3,472.502 us| 32,011.758 us| 31,317.833 us|   42,849.35 us|
|FileStreamWrite1|10MB|    6,022.757 us|      403.778 us|      267.074 us|  5,950.029 us|  5,829.438 us|    6,772.71 us|
|FileStreamWrite2|10MB|   82,411.529 us|    8,098.280 us|    5,356.511 us| 82,013.321 us| 76,219.804 us|   95,734.61 us|
|FileStreamWrite3|10MB|  338,604.863 us|  637,376.363 us|  421,585.000 us|209,335.637 us|188,838.605 us|1,538,145.36 us|
|WriteFileWinApi1|10MB|   37,533.816 us|  151,106.776 us|   99,947.777 us|  5,920.406 us|  5,843.899 us|  321,990.13 us|
|WriteFileWinApi2|10MB|    7,417.325 us|      791.894 us|      523.789 us|  7,197.928 us|  7,067.540 us|    8,762.82 us|
|     **ProduceName**|**50MB**|        **8.630 us**|        **4.646 us**|        **3.073 us**|      **7.697 us**|      **6.998 us**|       **17.26 us**|
|   WriteAllLines|50MB|  224,794.423 us|   15,818.021 us|   10,462.641 us|219,490.269 us|216,702.440 us|  247,293.93 us|
|    WriteAllText|50MB|  169,526.027 us|   12,540.601 us|    8,294.831 us|166,084.158 us|164,446.961 us|  190,636.05 us|
|   WriteAllBytes|50MB|1,131,659.827 us|2,352,695.999 us|1,556,162.734 us|379,703.005 us|325,478.645 us|5,144,793.14 us|
|   BinaryWriter1|50MB|  367,866.018 us|   88,355.417 us|   58,441.638 us|368,482.643 us|302,041.936 us|  463,004.58 us|
|   StreamWriter1|50MB|  846,513.277 us|2,272,739.967 us|1,503,276.769 us|170,460.435 us|152,909.377 us|4,576,251.29 us|
|FileStreamWrite1|50MB|  359,675.509 us|  238,623.815 us|  157,834.879 us|311,235.336 us|285,048.118 us|  804,315.91 us|
|FileStreamWrite2|50MB|  620,357.734 us|  911,974.281 us|  603,214.521 us|428,533.875 us|379,658.920 us|2,334,094.51 us|
|FileStreamWrite3|50MB|1,318,387.267 us|1,071,903.778 us|  708,997.981 us|984,968.299 us|956,127.137 us|2,772,914.11 us|
|WriteFileWinApi1|50MB|  663,853.148 us|1,162,097.559 us|  768,655.583 us|332,624.558 us|291,162.589 us|2,618,503.52 us|
|WriteFileWinApi2|50MB|   37,797.624 us|    4,953.419 us|    3,276.380 us| 36,167.378 us| 35,344.698 us|   45,133.36 us|
