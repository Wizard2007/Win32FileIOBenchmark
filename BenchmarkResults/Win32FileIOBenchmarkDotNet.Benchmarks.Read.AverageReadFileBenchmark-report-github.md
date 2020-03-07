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
|                        Method|Size|          Mean|        Error|       StdDev|        Median|           Min|           Max|
|-------------------------------|--------------|---------------:|--------------:|--------------:|---------------:|---------------:|---------------:|
|               **ProduceName**|**01MB**|      **3.732 us**|     **2.821 us**|     **1.866 us**|      **2.799 us**|      **2.333 us**|      **8.397 us**|
|              ReadAllLines|01MB|  4,512.963 us|   107.323 us|    70.987 us|  4,480.307 us|  4,468.645 us|  4,670.174 us|
|               ReadAllText|01MB|  2,935.338 us|   113.747 us|    75.237 us|  2,900.490 us|  2,878.331 us|  3,092.923 us|
|              ReadAllBytes|01MB|    342.694 us|    50.318 us|    33.283 us|    328.186 us|    313.024 us|    414.256 us|
|             BinaryReader1|01MB|  2,425.356 us|    50.772 us|    33.583 us|  2,411.827 us|  2,390.835 us|  2,476.205 us|
|             StreamReader1|01MB|  1,990.667 us|    86.732 us|    57.368 us|  1,963.750 us|  1,947.655 us|  2,096.937 us|
|             StreamReader2|01MB|  1,541.330 us|    53.245 us|    35.218 us|  1,531.300 us|  1,505.876 us|  1,601.976 us|
|             StreamReader3|01MB|  1,585.555 us|    67.894 us|    44.908 us|  1,564.889 us|  1,542.730 us|  1,668.219 us|
|             StreamReader4|01MB|  2,655.389 us|   349.068 us|   230.887 us|  2,535.451 us|  2,482.269 us|  3,185.291 us|
|             StreamReader5|01MB|  1,649.466 us|    66.551 us|    44.019 us|  1,636.964 us|  1,608.973 us|  1,746.126 us|
|             StreamReader6|01MB|  3,234.088 us|   150.777 us|    99.729 us|  3,188.324 us|  3,166.165 us|  3,480.122 us|
|             StreamReader7|01MB| 85,030.484 us|   221.601 us|   146.576 us| 84,960.928 us| 84,855.265 us| 85,243.863 us|
|           FileStreamRead1|01MB|    675.918 us|   200.553 us|   132.653 us|    640.510 us|    544.877 us|    995.987 us|
|           FileStreamRead2|01MB| 31,613.503 us| 5,824.614 us| 3,852.621 us| 33,444.392 us| 26,543.628 us| 37,068.898 us|
|          FileStreamRead2A|01MB| 26,912.026 us| 2,656.102 us| 1,756.847 us| 26,316.907 us| 26,061.729 us| 31,864.576 us|
|           FileStreamRead3|01MB|    804.347 us|   480.621 us|   317.901 us|    609.255 us|    522.485 us|  1,300.614 us|
|           FileStreamRead4|01MB|    597.965 us|    75.677 us|    50.056 us|    582.197 us|    535.547 us|    691.826 us|
|           FileStreamRead5|01MB|  3,512.684 us|   236.756 us|   156.600 us|  3,499.715 us|  3,368.161 us|  3,873.385 us|
|           FileStreamRead6|01MB|  3,198.680 us|   164.522 us|   108.821 us|  3,159.400 us|  3,081.261 us|  3,380.757 us|
|           FileStreamRead7|01MB|  1,129.220 us|   214.049 us|   141.580 us|  1,088.588 us|  1,009.515 us|  1,443.364 us|
|           FileStreamRead8|01MB| 25,996.511 us| 1,858.483 us| 1,229.271 us| 25,584.262 us| 24,812.897 us| 28,985.311 us|
|           ReadFileWinApi1|01MB|    505.924 us|    86.573 us|    57.263 us|    477.934 us|    453.909 us|    612.520 us|
|           ReadFileWinApi2|01MB|    238.617 us|    46.373 us|    30.673 us|    227.188 us|    202.463 us|    295.764 us|
|           ReadFileWinApi3|01MB|    229.287 us|    43.508 us|    28.778 us|    217.391 us|    201.530 us|    289.233 us|
|  BinaryReader1NoOpenClose|01MB|  2,518.423 us|   249.667 us|   165.139 us|  2,452.180 us|  2,422.557 us|  2,956.238 us|
|  StreamReader2NoOpenClose|01MB|  1,559.617 us|    78.241 us|    51.751 us|  1,536.432 us|  1,510.541 us|  1,639.296 us|
|FileStreamRead1NoOpenClose|01MB|    540.259 us|   105.326 us|    69.667 us|    513.621 us|    483.298 us|    685.295 us|
| ReadFileWinApiNoOpenClose|01MB|    495.801 us|    65.059 us|    43.033 us|    486.331 us|    454.375 us|    577.999 us|
|               **ProduceName**|**10MB**|      **3.639 us**|     **3.237 us**|     **2.141 us**|      **2.799 us**|      **2.333 us**|      **8.864 us**|
|              ReadAllLines|10MB| 89,273.900 us|10,572.742 us| 6,993.214 us| 86,363.473 us| 85,033.469 us|106,502.463 us|
|               ReadAllText|10MB| 58,028.702 us| 3,888.596 us| 2,572.065 us| 57,471.929 us| 56,476.409 us| 65,079.681 us|
|              ReadAllBytes|10MB|  6,984.502 us|   293.136 us|   193.892 us|  6,877.440 us|  6,819.360 us|  7,280.266 us|
|             BinaryReader1|10MB| 40,457.305 us|   195.850 us|   129.543 us| 40,445.922 us| 40,317.633 us| 40,744.018 us|
|             StreamReader1|10MB| 33,946.864 us|   326.285 us|   215.817 us| 33,853.517 us| 33,770.246 us| 34,459.739 us|
|             StreamReader2|10MB| 25,368.737 us|   194.635 us|   128.739 us| 25,365.471 us| 25,185.167 us| 25,561.170 us|
|             StreamReader3|10MB| 25,364.958 us|   155.359 us|   102.760 us| 25,404.424 us| 25,176.770 us| 25,453.874 us|
|             StreamReader4|10MB| 42,952.310 us|   450.614 us|   298.053 us| 42,854.717 us| 42,682.810 us| 43,701.189 us|
|             StreamReader5|10MB| 27,869.573 us| 2,456.759 us| 1,624.994 us| 27,058.882 us| 26,894.905 us| 31,906.561 us|
|             StreamReader6|10MB| 52,076.667 us|   831.787 us|   550.175 us| 51,867.813 us| 51,575.782 us| 53,449.263 us|
|             StreamReader7|10MB|120,830.396 us| 9,059.619 us| 5,992.377 us|119,875.042 us|114,534.268 us|131,334.487 us|
|           FileStreamRead1|10MB|  5,575.799 us|   723.533 us|   478.573 us|  5,480.492 us|  5,091.894 us|  6,411.635 us|
|           FileStreamRead2|10MB| 67,100.018 us| 8,275.286 us| 5,473.589 us| 65,552.950 us| 64,291.756 us| 82,559.597 us|
|          FileStreamRead2A|10MB| 64,510.406 us| 1,372.036 us|   907.517 us| 64,221.547 us| 63,797.261 us| 66,959.227 us|
|           FileStreamRead3|10MB|  2,999.016 us|   284.886 us|   188.435 us|  2,936.878 us|  2,931.046 us|  3,534.237 us|
|           FileStreamRead4|10MB|  8,391.899 us|   863.864 us|   571.392 us|  8,212.575 us|  7,895.585 us|  9,416.856 us|
|           FileStreamRead5|10MB| 59,515.638 us| 1,154.237 us|   763.456 us| 59,398.592 us| 58,671.312 us| 61,074.275 us|
|           FileStreamRead6|10MB| 51,924.120 us|   927.070 us|   613.200 us| 52,095.001 us| 50,212.656 us| 52,291.866 us|
|           FileStreamRead7|10MB|  9,273.639 us|   903.951 us|   597.908 us|  9,171.008 us|  8,810.400 us| 10,756.189 us|
|           FileStreamRead8|10MB| 45,238.321 us| 2,514.791 us| 1,663.379 us| 44,870.482 us| 43,501.059 us| 49,653.317 us|
|           ReadFileWinApi1|10MB|  5,084.197 us|   255.342 us|   168.893 us|  5,020.286 us|  4,960.340 us|  5,433.375 us|
|           ReadFileWinApi2|10MB|  5,256.804 us|   459.897 us|   304.193 us|  5,153.240 us|  5,089.562 us|  6,098.144 us|
|           ReadFileWinApi3|10MB|  5,338.442 us|   440.538 us|   291.388 us|  5,139.011 us|  5,104.023 us|  5,834.569 us|
|  BinaryReader1NoOpenClose|10MB| 40,257.641 us| 2,315.583 us| 1,531.615 us| 39,785.352 us| 39,373.429 us| 44,418.206 us|
|  StreamReader2NoOpenClose|10MB| 25,009.155 us|   197.316 us|   130.513 us| 25,030.288 us| 24,796.569 us| 25,201.961 us|
|FileStreamRead1NoOpenClose|10MB|  5,183.236 us|   368.600 us|   243.806 us|  5,099.825 us|  4,981.799 us|  5,739.402 us|
| ReadFileWinApiNoOpenClose|10MB|  5,165.229 us|   389.859 us|   257.867 us|  5,026.584 us|  4,959.407 us|  5,631.640 us|
|               **ProduceName**|**50MB**|      **3.405 us**|     **2.956 us**|     **1.955 us**|      **2.566 us**|      **2.333 us**|      **8.397 us**|
|              ReadAllLines|50MB|471,690.094 us|11,723.122 us| 7,754.119 us|467,201.483 us|465,027.806 us|482,953.233 us|
|               ReadAllText|50MB|287,565.048 us| 5,037.947 us| 3,332.290 us|287,631.152 us|282,650.286 us|295,008.917 us|
|              ReadAllBytes|50MB| 35,620.542 us| 2,951.574 us| 1,952.283 us| 34,957.033 us| 34,746.639 us| 41,148.478 us|
|             BinaryReader1|50MB|200,503.078 us| 8,772.622 us| 5,802.546 us|197,726.911 us|196,874.141 us|215,058.012 us|
|             StreamReader1|50MB|164,043.062 us| 3,055.891 us| 2,021.282 us|163,359.307 us|162,474.115 us|168,814.375 us|
|             StreamReader2|50MB|126,078.570 us| 2,294.138 us| 1,517.430 us|125,636.137 us|125,165.901 us|130,216.276 us|
|             StreamReader3|50MB|129,294.743 us|17,714.665 us|11,717.154 us|125,637.536 us|125,094.059 us|162,622.930 us|
|             StreamReader4|50MB|216,233.136 us| 5,445.098 us| 3,601.595 us|216,677.249 us|211,201.888 us|223,112.675 us|
|             StreamReader5|50MB|134,123.949 us| 3,643.781 us| 2,410.136 us|133,110.002 us|132,870.219 us|140,707.491 us|
|             StreamReader6|50MB|257,796.989 us| 5,113.491 us| 3,382.258 us|256,293.959 us|255,559.448 us|265,172.236 us|
|             StreamReader7|50MB|240,852.947 us| 5,139.141 us| 3,399.224 us|239,112.373 us|238,625.809 us|249,381.532 us|
|           FileStreamRead1|50MB| 24,316.210 us|   273.094 us|   180.634 us| 24,305.340 us| 23,992.782 us| 24,620.231 us|
|           FileStreamRead2|50MB|224,202.569 us| 5,032.930 us| 3,328.972 us|222,638.940 us|222,203.458 us|230,973.273 us|
|          FileStreamRead2A|50MB|225,273.150 us| 3,885.588 us| 2,570.076 us|224,099.565 us|223,774.645 us|231,966.460 us|
|           FileStreamRead3|50MB| 13,606.717 us| 1,119.067 us|   740.193 us| 13,500.867 us| 12,896.978 us| 14,955.661 us|
|           FileStreamRead4|50MB| 41,383.176 us| 2,797.802 us| 1,850.573 us| 40,211.504 us| 39,890.316 us| 44,689.245 us|
|           FileStreamRead5|50MB|296,229.106 us| 9,641.810 us| 6,377.461 us|293,678.680 us|291,047.829 us|308,293.093 us|
|           FileStreamRead6|50MB|254,824.611 us| 5,296.065 us| 3,503.019 us|256,011.258 us|248,305.307 us|259,649.291 us|
|           FileStreamRead7|50MB| 43,640.637 us| 1,779.345 us| 1,176.927 us| 43,493.128 us| 42,352.059 us| 45,939.943 us|
|           FileStreamRead8|50MB|119,185.222 us| 7,484.107 us| 4,950.273 us|117,311.368 us|114,746.061 us|131,929.746 us|
|           ReadFileWinApi1|50MB| 24,294.657 us|   309.081 us|   204.438 us| 24,349.891 us| 23,917.675 us| 24,489.143 us|
|           ReadFileWinApi2|50MB| 26,159.508 us|   288.333 us|   190.714 us| 26,153.863 us| 25,791.156 us| 26,530.099 us|
|           ReadFileWinApi3|50MB| 26,338.039 us|   292.446 us|   193.435 us| 26,323.438 us| 25,963.763 us| 26,741.426 us|
|  BinaryReader1NoOpenClose|50MB|197,692.063 us| 6,960.156 us| 4,603.712 us|195,696.918 us|194,870.505 us|209,482.819 us|
|  StreamReader2NoOpenClose|50MB|124,874.522 us| 2,551.163 us| 1,687.436 us|124,334.123 us|123,928.265 us|129,610.753 us|
|FileStreamRead1NoOpenClose|50MB| 24,599.564 us| 1,328.024 us|   878.406 us| 24,292.511 us| 23,878.022 us| 26,963.948 us|
| ReadFileWinApiNoOpenClose|50MB| 24,739.329 us| 1,979.292 us| 1,309.179 us| 24,261.022 us| 23,805.714 us| 28,243.103 us|
