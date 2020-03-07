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
|                        Method|Size|        Mean|        Error|       StdDev|      Median|         Min|         Max|
|-------------------------------|--------------|-------------:|--------------:|--------------:|-------------:|-------------:|-------------:|
|               **ProduceName**|**01MB**|    **846.1 us**|     **33.26 us**|     **22.00 us**|    **833.9 us**|    **822.0 us**|    **882.2 us**|
|              ReadAllLines|01MB| 10,998.2 us|    922.46 us|    610.15 us| 10,984.5 us| 10,119.9 us| 11,905.2 us|
|               ReadAllText|01MB| 13,450.7 us|  6,530.12 us|  4,319.27 us| 12,126.5 us| 11,085.1 us| 25,562.6 us|
|              ReadAllBytes|01MB|  7,804.6 us|  1,712.92 us|  1,132.99 us|  7,569.3 us|  7,149.6 us| 10,960.1 us|
|             BinaryReader1|01MB| 14,220.9 us| 10,308.18 us|  6,818.23 us| 12,040.0 us| 10,503.8 us| 33,321.9 us|
|             StreamReader1|01MB| 11,260.7 us|  1,099.13 us|    727.01 us| 11,292.4 us| 10,387.2 us| 12,169.2 us|
|             StreamReader2|01MB| 18,277.2 us| 30,451.39 us| 20,141.71 us| 12,243.6 us| 10,897.1 us| 75,581.2 us|
|             StreamReader3|01MB| 11,520.6 us|  1,157.08 us|    765.34 us| 11,396.9 us| 10,348.0 us| 12,806.5 us|
|             StreamReader4|01MB| 12,226.9 us|    888.26 us|    587.53 us| 12,231.3 us| 11,291.7 us| 13,453.1 us|
|             StreamReader5|01MB| 13,504.1 us|  8,943.72 us|  5,915.72 us| 11,749.6 us| 10,251.9 us| 30,265.9 us|
|             StreamReader6|01MB| 11,318.4 us|    839.33 us|    555.17 us| 11,293.1 us| 10,467.4 us| 12,272.8 us|
|             StreamReader7|01MB|121,833.4 us|  4,204.60 us|  2,781.08 us|121,640.1 us|118,919.9 us|128,429.1 us|
|           FileStreamRead1|01MB| 25,319.8 us| 26,343.01 us| 17,424.27 us| 19,305.8 us| 18,298.2 us| 74,622.5 us|
|           FileStreamRead2|01MB| 49,423.3 us| 10,704.07 us|  7,080.08 us| 46,992.4 us| 45,044.3 us| 69,055.2 us|
|          FileStreamRead2A|01MB| 48,952.9 us|  2,215.87 us|  1,465.66 us| 48,544.0 us| 47,204.6 us| 51,943.9 us|
|           FileStreamRead3|01MB| 20,056.7 us|  4,574.35 us|  3,025.65 us| 19,096.4 us| 18,845.8 us| 28,654.1 us|
|           FileStreamRead4|01MB| 18,898.9 us| 40,083.79 us| 26,512.95 us| 10,505.2 us|  9,847.9 us| 94,342.1 us|
|           FileStreamRead5|01MB| 20,747.0 us| 38,025.72 us| 25,151.66 us| 12,866.0 us| 12,075.5 us| 92,318.4 us|
|           FileStreamRead6|01MB| 11,868.2 us|    758.27 us|    501.55 us| 11,888.6 us| 11,265.6 us| 12,904.9 us|
|           FileStreamRead7|01MB| 23,700.1 us|  7,688.17 us|  5,085.25 us| 22,034.6 us| 21,173.7 us| 37,925.4 us|
|           FileStreamRead8|01MB| 69,545.8 us| 97,071.16 us| 64,206.56 us| 47,850.7 us| 47,126.3 us|251,988.4 us|
|           ReadFileWinApi1|01MB| 20,744.8 us|  1,359.40 us|    899.16 us| 20,544.8 us| 20,034.0 us| 23,218.9 us|
|           ReadFileWinApi2|01MB| 20,060.8 us| 45,073.91 us| 29,813.60 us| 10,652.2 us|  9,289.5 us|104,869.7 us|
|           ReadFileWinApi3|01MB| 21,682.7 us| 51,858.35 us| 34,301.09 us| 11,063.4 us|  9,862.8 us|119,284.2 us|
|  BinaryReader1NoOpenClose|01MB| 11,369.4 us|    756.60 us|    500.44 us| 11,509.6 us| 10,313.0 us| 12,046.5 us|
|  StreamReader2NoOpenClose|01MB| 11,859.5 us|  3,816.32 us|  2,524.26 us| 11,647.7 us|  9,576.4 us| 18,695.2 us|
|FileStreamRead1NoOpenClose|01MB| 18,990.5 us|    421.80 us|    278.99 us| 19,072.6 us| 18,558.5 us| 19,305.3 us|
| ReadFileWinApiNoOpenClose|01MB| 20,403.7 us|    696.15 us|    460.46 us| 20,313.2 us| 19,918.8 us| 21,484.9 us|
|               **ProduceName**|**10MB**|    **846.9 us**|     **30.95 us**|     **20.47 us**|    **840.2 us**|    **827.6 us**|    **893.4 us**|
|              ReadAllLines|10MB|161,575.4 us| 61,094.04 us| 40,409.93 us|149,138.6 us|133,800.0 us|274,625.5 us|
|               ReadAllText|10MB|143,293.7 us|  8,209.73 us|  5,430.23 us|145,436.2 us|133,925.9 us|150,236.8 us|
|              ReadAllBytes|10MB| 66,965.8 us|  6,688.36 us|  4,423.94 us| 66,947.3 us| 60,573.7 us| 73,943.3 us|
|             BinaryReader1|10MB|134,979.7 us|  6,990.83 us|  4,624.00 us|134,282.6 us|127,810.0 us|141,488.4 us|
|             StreamReader1|10MB|139,416.1 us| 12,655.92 us|  8,371.11 us|138,951.1 us|128,391.3 us|151,193.6 us|
|             StreamReader2|10MB|138,489.8 us| 10,431.58 us|  6,899.85 us|136,829.7 us|129,881.3 us|149,231.0 us|
|             StreamReader3|10MB|131,275.8 us| 11,700.84 us|  7,739.38 us|132,305.7 us|120,017.1 us|147,676.1 us|
|             StreamReader4|10MB|145,757.2 us|  8,526.80 us|  5,639.95 us|144,833.5 us|134,972.3 us|155,948.2 us|
|             StreamReader5|10MB|132,392.0 us| 12,243.54 us|  8,098.34 us|129,388.7 us|122,542.7 us|146,408.6 us|
|             StreamReader6|10MB|144,072.4 us| 70,080.50 us| 46,353.91 us|130,088.5 us|124,782.0 us|275,692.8 us|
|             StreamReader7|10MB|242,217.9 us|  9,202.93 us|  6,087.17 us|244,748.4 us|230,976.5 us|250,606.6 us|
|           FileStreamRead1|10MB| 73,348.8 us|  4,526.79 us|  2,994.19 us| 74,033.8 us| 68,742.7 us| 77,763.5 us|
|           FileStreamRead2|10MB|140,418.2 us| 39,703.40 us| 26,261.34 us|132,049.9 us|128,589.1 us|214,864.9 us|
|          FileStreamRead2A|10MB|153,136.1 us|  7,231.68 us|  4,783.31 us|153,051.4 us|146,874.2 us|160,860.9 us|
|           FileStreamRead3|10MB| 72,316.9 us|  3,598.15 us|  2,379.95 us| 73,046.9 us| 67,850.3 us| 74,801.2 us|
|           FileStreamRead4|10MB| 92,576.1 us|  4,864.85 us|  3,217.79 us| 92,735.7 us| 86,222.1 us| 99,015.1 us|
|           FileStreamRead5|10MB|113,976.0 us|  9,911.25 us|  6,555.67 us|113,808.9 us|105,193.5 us|126,325.2 us|
|           FileStreamRead6|10MB|104,284.3 us| 10,472.28 us|  6,926.76 us|102,471.2 us| 98,451.1 us|122,573.1 us|
|           FileStreamRead7|10MB| 82,108.3 us|  3,546.45 us|  2,345.76 us| 82,766.7 us| 78,078.8 us| 84,420.5 us|
|           FileStreamRead8|10MB|131,122.1 us| 15,247.42 us| 10,085.22 us|127,208.5 us|123,287.3 us|151,847.1 us|
|           ReadFileWinApi1|10MB| 74,936.3 us|  3,883.23 us|  2,568.52 us| 75,717.6 us| 70,396.4 us| 78,342.4 us|
|           ReadFileWinApi2|10MB|117,854.8 us| 18,115.12 us| 11,982.03 us|115,798.0 us| 95,333.9 us|139,134.0 us|
|           ReadFileWinApi3|10MB|126,173.2 us|106,660.51 us| 70,549.33 us|105,359.1 us| 94,038.4 us|325,363.0 us|
|  BinaryReader1NoOpenClose|10MB|139,656.0 us| 36,558.95 us| 24,181.48 us|132,646.1 us|124,217.0 us|206,292.9 us|
|  StreamReader2NoOpenClose|10MB|134,671.4 us|  8,991.23 us|  5,947.14 us|133,831.9 us|126,893.8 us|146,519.2 us|
|FileStreamRead1NoOpenClose|10MB| 78,339.6 us| 10,711.57 us|  7,085.04 us| 75,499.8 us| 69,197.0 us| 88,345.6 us|
| ReadFileWinApiNoOpenClose|10MB| 77,988.1 us| 16,743.18 us| 11,074.58 us| 75,644.8 us| 70,208.4 us|108,690.8 us|
|               **ProduceName**|**50MB**|    **855.3 us**|     **31.26 us**|     **20.68 us**|    **860.2 us**|    **822.9 us**|    **885.0 us**|
|              ReadAllLines|50MB|732,806.5 us| 92,972.47 us| 61,495.54 us|704,504.8 us|698,859.8 us|881,466.9 us|
|               ReadAllText|50MB|673,949.0 us| 45,570.59 us| 30,142.12 us|656,117.1 us|645,151.2 us|714,406.5 us|
|              ReadAllBytes|50MB|278,368.3 us|  3,912.62 us|  2,587.95 us|279,020.4 us|274,651.6 us|282,323.7 us|
|             BinaryReader1|50MB|671,039.0 us|152,729.82 us|101,021.32 us|658,923.6 us|484,495.0 us|833,525.6 us|
|             StreamReader1|50MB|645,303.5 us| 86,194.98 us| 57,012.64 us|622,160.0 us|611,943.5 us|778,362.9 us|
|             StreamReader2|50MB|649,703.7 us| 73,630.94 us| 48,702.31 us|647,607.1 us|584,803.2 us|713,041.5 us|
|             StreamReader3|50MB|628,769.7 us| 75,664.52 us| 50,047.39 us|615,514.2 us|593,197.5 us|759,254.4 us|
|             StreamReader4|50MB|677,311.8 us| 68,207.97 us| 45,115.35 us|660,610.7 us|642,047.5 us|788,607.3 us|
|             StreamReader5|50MB|656,439.0 us| 90,824.82 us| 60,074.99 us|633,077.3 us|603,305.7 us|766,641.0 us|
|             StreamReader6|50MB|598,262.2 us| 32,203.75 us| 21,300.79 us|591,146.3 us|577,104.5 us|650,535.1 us|
|             StreamReader7|50MB|754,314.6 us| 97,574.66 us| 64,539.59 us|722,559.4 us|714,825.9 us|886,259.7 us|
|           FileStreamRead1|50MB|278,235.6 us| 12,258.46 us|  8,108.21 us|275,216.1 us|271,777.0 us|296,443.0 us|
|           FileStreamRead2|50MB|478,159.0 us| 10,536.42 us|  6,969.19 us|474,753.0 us|471,025.2 us|488,311.0 us|
|          FileStreamRead2A|50MB|587,109.9 us| 39,540.73 us| 26,153.74 us|579,375.7 us|565,296.4 us|652,782.7 us|
|           FileStreamRead3|50MB|273,887.3 us|  7,532.64 us|  4,982.38 us|274,256.0 us|267,308.4 us|280,156.4 us|
|           FileStreamRead4|50MB|446,776.8 us| 69,233.63 us| 45,793.76 us|432,592.0 us|408,447.4 us|567,314.5 us|
|           FileStreamRead5|50MB|526,041.0 us| 41,873.55 us| 27,696.76 us|520,839.7 us|492,693.4 us|574,857.8 us|
|           FileStreamRead6|50MB|486,126.3 us| 72,062.31 us| 47,664.75 us|472,822.6 us|452,679.0 us|616,441.6 us|
|           FileStreamRead7|50MB|314,828.3 us|  7,539.58 us|  4,986.96 us|312,711.1 us|310,972.2 us|326,663.1 us|
|           FileStreamRead8|50MB|438,113.8 us| 14,092.71 us|  9,321.45 us|437,454.1 us|425,461.7 us|454,031.8 us|
|           ReadFileWinApi1|50MB|281,941.9 us|  6,889.08 us|  4,556.70 us|282,059.2 us|275,523.0 us|290,369.5 us|
|           ReadFileWinApi2|50MB|530,982.0 us|166,668.25 us|110,240.73 us|461,554.9 us|438,739.8 us|706,913.5 us|
|           ReadFileWinApi3|50MB|494,282.8 us|115,312.62 us| 76,272.16 us|456,191.3 us|445,117.4 us|652,140.3 us|
|  BinaryReader1NoOpenClose|50MB|653,360.0 us| 77,061.95 us| 50,971.71 us|641,676.9 us|598,268.0 us|780,846.5 us|
|  StreamReader2NoOpenClose|50MB|654,230.1 us| 92,624.17 us| 61,265.15 us|635,866.6 us|597,602.3 us|797,942.5 us|
|FileStreamRead1NoOpenClose|50MB|280,308.5 us|  9,152.91 us|  6,054.08 us|279,104.4 us|272,117.6 us|290,348.1 us|
| ReadFileWinApiNoOpenClose|50MB|281,276.8 us|  7,345.35 us|  4,858.49 us|279,934.1 us|274,836.3 us|291,136.5 us|
