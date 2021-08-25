# How fast (slow) is that metric collection?

Curious to know what overhead a `TrackDependency` call added. And then I also wondered if a stopwatch was faster/slower than comparing DateTimeOffsets. And now I know!

## Build and run all benchmarks

```shell
dotnet run -c Release --filter * --join
```

## Sample Results

```ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1200 (21H1/May2021Update)
Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK=5.0.206
  [Host]     : .NET 5.0.9 (5.0.921.35908), X64 RyuJIT
  DefaultJob : .NET 5.0.9 (5.0.921.35908), X64 RyuJIT


```

| Method                       |     Mean |    Error |   StdDev |
| ---------------------------- | -------: | -------: | -------: |
| NoTracking_DateTimeOffset    | 179.2 ns |  3.59 ns |  8.81 ns |
| NoTracking_Stopwatch         | 122.6 ns |  1.94 ns |  1.62 ns |
| TrackWithApplicationInsights | 713.1 ns | 14.23 ns | 22.97 ns |
