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
|                        Method|Size|        Mean|        Error|       StdDev|      Median|         Min|         Max|
|-------------------------------|--------------|-------------:|--------------:|--------------:|-------------:|-------------:|-------------:|
|               **ProduceName**|**01MB**|  **1,036.1 us**|    **315.98 us**|    **209.00 us**|    **956.8 us**|    **826.6 us**|  **1,368.3 us**|
|              ReadAllLines|01MB|  6,902.4 us|  2,878.94 us|  1,904.24 us|  6,251.4 us|  5,915.7 us| 12,191.6 us|
|               ReadAllText|01MB|  5,250.3 us|  3,668.51 us|  2,426.49 us|  4,410.3 us|  4,119.2 us| 12,006.9 us|
|              ReadAllBytes|01MB|  2,310.0 us|  3,428.91 us|  2,268.01 us|  1,588.4 us|  1,536.7 us|  8,763.3 us|
|             BinaryReader1|01MB|  5,170.4 us|  3,272.29 us|  2,164.41 us|  4,455.6 us|  4,383.7 us| 11,323.0 us|
|             StreamReader1|01MB|  4,534.7 us|  3,452.41 us|  2,283.56 us|  3,804.8 us|  3,685.4 us| 11,026.8 us|
|             StreamReader2|01MB|  4,208.9 us|  3,532.28 us|  2,336.39 us|  3,382.6 us|  3,249.2 us| 10,784.6 us|
|             StreamReader3|01MB|  4,030.6 us|  3,417.19 us|  2,260.26 us|  3,301.2 us|  3,216.1 us| 10,459.5 us|
|             StreamReader4|01MB|  4,906.8 us|  4,079.08 us|  2,698.06 us|  4,041.8 us|  3,976.0 us| 12,582.1 us|
|             StreamReader5|01MB|  4,116.6 us|  3,936.54 us|  2,603.78 us|  3,280.7 us|  3,210.5 us| 11,523.1 us|
|             StreamReader6|01MB|  5,882.8 us|  3,206.54 us|  2,120.93 us|  5,226.2 us|  5,132.0 us| 11,917.3 us|
|             StreamReader7|01MB|113,076.4 us|  4,025.23 us|  2,662.44 us|112,274.1 us|111,771.6 us|120,622.1 us|
|           FileStreamRead1|01MB| 13,908.2 us|  2,428.39 us|  1,606.23 us| 13,334.1 us| 13,163.4 us| 18,393.3 us|
|           FileStreamRead2|01MB| 40,784.0 us|  2,726.31 us|  1,803.29 us| 40,158.8 us| 39,917.8 us| 45,875.1 us|
|          FileStreamRead2A|01MB| 42,351.9 us|  3,111.48 us|  2,058.05 us| 41,681.7 us| 41,516.1 us| 48,194.1 us|
|           FileStreamRead3|01MB| 13,781.0 us|  2,543.76 us|  1,682.54 us| 13,272.5 us| 13,164.3 us| 18,567.3 us|
|           FileStreamRead4|01MB|  4,716.1 us|  4,098.70 us|  2,711.04 us|  3,817.9 us|  3,662.5 us| 12,408.5 us|
|           FileStreamRead5|01MB|  8,533.1 us|  2,094.11 us|  1,385.12 us|  8,072.6 us|  7,806.0 us| 12,431.4 us|
|           FileStreamRead6|01MB|  7,782.5 us|  1,717.81 us|  1,136.23 us|  7,464.3 us|  7,026.0 us| 10,835.0 us|
|           FileStreamRead7|01MB| 16,735.8 us|  2,957.15 us|  1,955.97 us| 16,112.8 us| 15,790.2 us| 22,276.0 us|
|           FileStreamRead8|01MB| 43,296.5 us|  4,376.55 us|  2,894.82 us| 42,015.5 us| 41,572.5 us| 49,483.0 us|
|           ReadFileWinApi1|01MB| 16,083.4 us|  2,653.74 us|  1,755.28 us| 15,436.9 us| 15,335.9 us| 21,028.1 us|
|           ReadFileWinApi2|01MB|  5,130.8 us|  3,897.60 us|  2,578.02 us|  4,247.5 us|  4,152.4 us| 12,447.7 us|
|           ReadFileWinApi3|01MB|  5,142.7 us|  3,635.88 us|  2,404.91 us|  4,352.3 us|  4,289.5 us| 11,978.9 us|
|  BinaryReader1NoOpenClose|01MB|  5,319.0 us|  3,617.89 us|  2,393.01 us|  4,547.0 us|  4,440.7 us| 12,117.0 us|
|  StreamReader2NoOpenClose|01MB|  4,127.8 us|  4,026.72 us|  2,663.42 us|  3,269.0 us|  3,222.1 us| 11,706.5 us|
|FileStreamRead1NoOpenClose|01MB| 15,223.3 us|  7,731.98 us|  5,114.23 us| 13,349.0 us| 13,149.4 us| 29,517.1 us|
| ReadFileWinApiNoOpenClose|01MB| 16,371.8 us|  2,879.28 us|  1,904.47 us| 15,666.4 us| 15,382.0 us| 21,393.4 us|
|               **ProduceName**|**10MB**|    **847.1 us**|     **33.76 us**|     **22.33 us**|    **853.5 us**|    **816.4 us**|    **885.0 us**|
|              ReadAllLines|10MB|102,765.0 us| 27,124.92 us| 17,941.46 us| 98,226.0 us| 90,630.6 us|152,313.7 us|
|               ReadAllText|10MB| 63,214.7 us| 39,291.15 us| 25,988.66 us| 53,978.7 us| 53,293.9 us|136,961.0 us|
|              ReadAllBytes|10MB| 13,849.4 us| 27,035.97 us| 17,882.62 us|  8,162.4 us|  8,069.1 us| 64,742.9 us|
|             BinaryReader1|10MB| 57,654.0 us| 46,693.96 us| 30,885.17 us| 48,147.2 us| 47,131.4 us|145,542.8 us|
|             StreamReader1|10MB| 49,656.0 us| 42,680.84 us| 28,230.74 us| 40,954.4 us| 38,993.2 us|129,978.4 us|
|             StreamReader2|10MB| 41,538.4 us| 43,724.47 us| 28,921.03 us| 32,327.6 us| 31,962.1 us|123,837.3 us|
|             StreamReader3|10MB| 42,200.8 us| 47,827.29 us| 31,634.79 us| 32,301.7 us| 31,737.2 us|132,230.2 us|
|             StreamReader4|10MB| 55,294.3 us| 47,352.37 us| 31,320.66 us| 44,684.6 us| 44,461.1 us|144,239.4 us|
|             StreamReader5|10MB| 40,314.3 us| 46,890.70 us| 31,015.29 us| 29,345.7 us| 29,240.0 us|128,155.7 us|
|             StreamReader6|10MB| 63,479.9 us| 32,089.89 us| 21,225.47 us| 54,366.9 us| 54,001.6 us|122,071.1 us|
|             StreamReader7|10MB|155,808.8 us| 47,734.69 us| 31,573.54 us|142,596.8 us|141,944.2 us|241,844.7 us|
|           FileStreamRead1|10MB| 23,431.6 us| 25,154.88 us| 16,638.40 us| 18,187.4 us| 17,778.5 us| 70,781.3 us|
|           FileStreamRead2|10MB| 94,334.8 us| 27,426.64 us| 18,141.03 us| 93,601.3 us| 78,528.1 us|137,442.9 us|
|          FileStreamRead2A|10MB|100,484.1 us| 77,469.80 us| 51,241.48 us| 80,341.1 us| 79,838.0 us|244,948.8 us|
|           FileStreamRead3|10MB| 21,100.5 us| 25,342.47 us| 16,762.48 us| 15,667.1 us| 15,568.2 us| 68,798.2 us|
|           FileStreamRead4|10MB| 22,120.9 us| 36,099.16 us| 23,877.36 us| 14,502.9 us| 13,522.1 us| 90,052.1 us|
|           FileStreamRead5|10MB| 74,901.2 us| 24,671.06 us| 16,318.38 us| 68,269.2 us| 65,942.7 us|119,872.5 us|
|           FileStreamRead6|10MB| 64,144.5 us| 21,605.77 us| 14,290.88 us| 59,796.8 us| 58,481.4 us|104,775.0 us|
|           FileStreamRead7|10MB| 32,971.5 us| 24,194.10 us| 16,002.90 us| 27,730.4 us| 26,365.4 us| 78,396.5 us|
|           FileStreamRead8|10MB| 67,575.7 us| 30,944.14 us| 20,467.63 us| 61,053.5 us| 58,161.0 us|125,403.8 us|
|           ReadFileWinApi1|10MB| 26,110.8 us| 26,041.15 us| 17,224.61 us| 20,225.8 us| 19,828.8 us| 74,975.6 us|
|           ReadFileWinApi2|10MB| 21,942.4 us| 48,630.84 us| 32,166.29 us| 11,797.9 us| 11,454.5 us|113,486.5 us|
|           ReadFileWinApi3|10MB| 21,803.2 us| 47,024.97 us| 31,104.11 us| 11,900.3 us| 11,579.6 us|110,323.1 us|
|  BinaryReader1NoOpenClose|10MB| 57,998.1 us| 41,936.68 us| 27,738.52 us| 49,216.4 us| 47,549.8 us|136,825.7 us|
|  StreamReader2NoOpenClose|10MB| 41,613.0 us| 44,055.26 us| 29,139.83 us| 32,228.9 us| 31,505.8 us|124,495.5 us|
|FileStreamRead1NoOpenClose|10MB| 23,393.3 us| 25,803.82 us| 17,067.63 us| 17,985.1 us| 17,840.1 us| 71,967.2 us|
| ReadFileWinApiNoOpenClose|10MB| 26,500.2 us| 27,375.53 us| 18,107.22 us| 20,551.1 us| 19,939.3 us| 77,956.6 us|
|               **ProduceName**|**50MB**|    **837.2 us**|     **25.53 us**|     **16.89 us**|    **846.5 us**|    **812.2 us**|    **855.6 us**|
|              ReadAllLines|50MB|496,784.5 us| 93,129.47 us| 61,599.38 us|478,582.8 us|463,108.1 us|668,476.9 us|
|               ReadAllText|50MB|298,296.9 us|136,103.19 us| 90,023.83 us|262,894.3 us|260,970.0 us|551,639.0 us|
|              ReadAllBytes|50MB| 61,713.5 us|120,498.37 us| 79,702.21 us| 36,568.3 us| 35,995.5 us|288,548.3 us|
|             BinaryReader1|50MB|267,789.7 us|186,931.87 us|123,643.86 us|228,238.6 us|222,006.1 us|619,556.0 us|
|             StreamReader1|50MB|251,198.5 us|267,956.36 us|177,236.54 us|195,246.7 us|191,709.9 us|755,603.1 us|
|             StreamReader2|50MB|206,366.2 us|267,071.78 us|176,651.45 us|150,335.7 us|150,099.2 us|709,123.8 us|
|             StreamReader3|50MB|212,889.2 us|291,208.99 us|192,616.71 us|152,001.3 us|149,133.0 us|761,068.6 us|
|             StreamReader4|50MB|276,773.6 us|289,128.49 us|191,240.59 us|213,180.3 us|210,138.3 us|820,659.0 us|
|             StreamReader5|50MB|182,860.7 us|223,929.54 us|148,115.52 us|135,876.8 us|135,196.2 us|604,401.6 us|
|             StreamReader6|50MB|294,795.9 us|163,958.98 us|108,448.71 us|258,934.8 us|257,566.3 us|603,197.0 us|
|             StreamReader7|50MB|315,049.2 us|218,292.02 us|144,386.66 us|268,033.8 us|267,279.0 us|725,897.5 us|
|           FileStreamRead1|50MB| 62,348.4 us|111,738.12 us| 73,907.85 us| 37,954.8 us| 37,335.7 us|272,578.0 us|
|           FileStreamRead2|50MB|265,174.6 us|113,741.09 us| 75,232.69 us|239,875.6 us|236,511.1 us|478,631.1 us|
|          FileStreamRead2A|50MB|277,313.5 us|169,344.72 us|112,011.05 us|240,213.8 us|239,079.3 us|595,710.6 us|
|           FileStreamRead3|50MB| 51,835.1 us|116,935.03 us| 77,345.28 us| 26,936.7 us| 26,565.6 us|271,922.6 us|
|           FileStreamRead4|50MB| 99,005.6 us|175,168.00 us|115,862.78 us| 62,941.0 us| 58,958.7 us|428,734.2 us|
|           FileStreamRead5|50MB|351,910.1 us|163,166.68 us|107,924.66 us|315,581.8 us|312,283.1 us|658,594.9 us|
|           FileStreamRead6|50MB|294,555.7 us| 92,888.27 us| 61,439.84 us|275,362.3 us|272,560.7 us|469,352.8 us|
|           FileStreamRead7|50MB| 96,812.4 us|120,638.01 us| 79,794.57 us| 70,713.0 us| 69,857.6 us|323,783.4 us|
|           FileStreamRead8|50MB|169,304.0 us|139,643.04 us| 92,365.22 us|142,318.8 us|125,158.0 us|429,533.8 us|
|           ReadFileWinApi1|50MB| 65,342.3 us|120,066.68 us| 79,416.68 us| 40,191.2 us| 39,786.3 us|291,363.7 us|
|           ReadFileWinApi2|50MB|101,638.5 us|282,714.05 us|186,997.84 us| 42,582.0 us| 41,912.1 us|633,842.6 us|
|           ReadFileWinApi3|50MB| 84,994.1 us|203,576.61 us|134,653.32 us| 42,569.7 us| 41,906.5 us|468,222.4 us|
|  BinaryReader1NoOpenClose|50MB|274,905.3 us|206,204.05 us|136,391.21 us|230,614.1 us|226,913.3 us|662,930.1 us|
|  StreamReader2NoOpenClose|50MB|196,063.1 us|212,461.49 us|140,530.12 us|151,917.8 us|148,856.4 us|595,985.8 us|
|FileStreamRead1NoOpenClose|50MB| 63,138.1 us|121,750.67 us| 80,530.53 us| 37,636.6 us| 36,812.8 us|292,325.6 us|
| ReadFileWinApiNoOpenClose|50MB| 63,172.2 us|114,893.13 us| 75,994.69 us| 38,964.1 us| 38,558.4 us|279,451.5 us|
