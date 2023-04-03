namespace Funtom.collections.perf.cs;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using Bogus;
using System.Linq;

//[DisassemblyDiagnoser(maxDepth: 9, syntax: DisassemblySyntax.Intel, printSource: true, printInstructionAddresses: true, exportGithubMarkdown: true, exportHtml: true, exportCombinedDisassemblyReport: true, exportDiff: true)]
public class Benchmark {
  private int[] xs = new int[100_000_000];
  private int[] ys = new int[100_000_000];

  [GlobalSetup]
  public void Setup() {
    var fake = new Faker();
    for (int i = 0; i < 100_000_000; i++) {
      xs[i] = fake.Random.Int(int.MinValue, int.MaxValue);
      ys[i] = fake.Random.Int(-20, 20);
    }
  }

  [Benchmark]
  public int Linq_max() => xs.Max();
  [Benchmark]
  public int SimdLinq_max() => SimdLinq.SimdLinqExtensions.Max(xs);
  [Benchmark]
  public int Funtom_Array_max() => Funtom.collections.Array.max(xs);


  [Benchmark]
  public int Linq_min() => xs.Min();
  [Benchmark]
  public int SimdLinq_min() => SimdLinq.SimdLinqExtensions.Min(xs);
  [Benchmark]
  public int Funtom_Array_min() => Funtom.collections.Array.min(xs);

  [Benchmark]
  public int Linq_sum() => ys.Sum();
  [Benchmark]
  public int SimdLinq_sum() => SimdLinq.SimdLinqExtensions.Sum(ys);
  [Benchmark]
  public int Funtom_Array_sum() => Funtom.collections.Array.sum(ys);
}

internal class Program {
  static void Main(string[] args) {
    BenchmarkRunner.Run<Benchmark>();
  }
}
