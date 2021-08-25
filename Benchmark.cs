using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace metric_collection_benchmark
{
  public class Benchmarks
  {
    private TelemetryClient _telemetryClient;
    private const string DEPENDENCY_TYPE = "Method";
    private const string DEPENDENCY_TARGET = "Self";
    private const string DEPENDENCY_NAME = "DoWork";
    private const string DEPENDENCY_DATA = "0";
    private const string DEPENDENCY_RESULT_CODE = "0";

    public Benchmarks()
    {
      var configuration = TelemetryConfiguration.CreateDefault();
      _telemetryClient = new TelemetryClient(configuration);
    }

    [Benchmark]
    public void NoTracking_DateTimeOffset()
    {
      var startTime = DateTimeOffset.UtcNow;
      var success = true;
      try
      {
        DoSomeWork(0);
      }
      catch
      {
        success = false;
        throw;
      }
      finally
      {
        var duration = DateTimeOffset.UtcNow.Subtract(startTime);
      }
    }




    [Benchmark]
    public void NoTracking_Stopwatch()
    {
      var startTime = DateTimeOffset.UtcNow;
      var success = true;
      var timer = Stopwatch.StartNew();
      try
      {
        DoSomeWork(0);
      }
      catch
      {
        success = false;
        throw;
      }
      finally
      {
        timer.Stop();
        var duration = timer.Elapsed;
      }
    }

    [Benchmark]
    public void TrackWithApplicationInsights()
    {
      var startTime = DateTimeOffset.UtcNow;
      var timer = Stopwatch.StartNew();
      var success = true;
      try
      {
        DoSomeWork(0);
      }
      catch
      {
        success = false;
        throw;
      }
      finally
      {
        timer.Stop();
        var duration = timer.Elapsed;
        _telemetryClient.TrackDependency(DEPENDENCY_TYPE, DEPENDENCY_TARGET, DEPENDENCY_NAME, DEPENDENCY_DATA, startTime, duration, DEPENDENCY_RESULT_CODE, success);
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void DoSomeWork(int x)
    {
      x++;
    }
  }
}
