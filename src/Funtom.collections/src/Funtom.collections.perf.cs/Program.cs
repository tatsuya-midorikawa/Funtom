namespace Funtom.collections.perf.cs;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Bogus;
using System.Linq;

public class Benchmark {
  private int[] xs = new int[100_000_000];

  [GlobalSetup]
  public void Setup() {
    var fake = new Faker();
    for (int i = 0; i < 100_000_000; i++) {
      xs[i] = fake.Random.Int(int.MinValue, int.MaxValue);
    }
  }

  [Benchmark]
  public int Linq_max() => xs.Max();
  [Benchmark]
  public int SimdLinq_max() => SimdLinq.SimdLinqExtensions.Max(xs);
  [Benchmark]
  public int Funtom_Array_max() => Funtom.collections.Array.max(xs);
}

internal class Program {
  static void Main(string[] args) {
    BenchmarkRunner.Run<Benchmark>();
  }
}
