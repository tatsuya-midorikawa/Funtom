open BenchmarkDotNet
open BenchmarkDotNet.Extensions
open BenchmarkDotNet.Exporters
open BenchmarkDotNet.Diagnosers
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running
open System
open System.Linq
open System.Runtime.Intrinsics
open System.IO
open FSharp.NativeInterop

#nowarn "9"

[<PlainExporter; MemoryDiagnoser; ShortRunJob;>]
type Benchmark () =

  let csv = "./sample.csv" |> System.IO.Path.GetFullPath

  [<GlobalSetup>]
  member __.Setup() =
    printfn $"path: {csv}"
    printfn $"exists: {System.IO.File.Exists csv}"
    ()
    

  [<Benchmark>]
  member __.read() =
    use sr = File.OpenText csv
    let mutable c = ' '
    while 0 <= sr.Peek() do
      let t = char (sr.Read())
      c <- t
    c

  [<Benchmark>]
  member __.readline() =
    use sr = File.OpenText csv
    let mutable cache = 0
    while 0 <= sr.Peek() do
      let a = sr.ReadLine()
      cache <- a.Length
    cache

  [<Benchmark>]
  member __.readtoend() =
    use sr = File.OpenText csv
    let s = sr.ReadToEnd()
    s.Length

  [<Benchmark>]
  member __.fsread() =
    use fs = File.OpenRead csv
    let mutable c = ' 'B
    while fs.CanRead do
      let p = NativePtr.stackalloc<byte> 1 |> NativePtr.toVoidPtr
      let mutable buf = Span<byte>(p, 1)
      fs.Read(buf) |> ignore
      c <- buf[0]
    c

[<EntryPoint>]
let main args =
  BenchmarkRunner.Run<Benchmark>() |> ignore
  0