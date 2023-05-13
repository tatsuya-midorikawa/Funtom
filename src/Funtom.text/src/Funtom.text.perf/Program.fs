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
open System.IO.MemoryMappedFiles

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
    while 0 <= sr.Read() do ()
    c

  [<Benchmark>]
  member __.readline() =
    use sr = File.OpenText csv
    let mutable s = ""
    while 0 <= sr.Peek() do
      s <- sr.ReadLine()
    s

  [<Benchmark>]
  member __.readtoend() =
    use sr = File.OpenText csv
    let s = sr.ReadToEnd()
    s.Length

  [<Benchmark>]
  member __.fs_read() =
    use fs = File.OpenRead csv
    let p = NativePtr.stackalloc<byte> 1 |> NativePtr.toVoidPtr
    let mutable buf = Span<byte>(p, 1)
    for i in 0L..fs.Length - 1L do
      fs.Read(buf) |> ignore
    buf[0]

  [<Benchmark>]
  member __.fs_read128() =
    use fs = File.OpenText csv
    let p = NativePtr.stackalloc<char> 128 |> NativePtr.toVoidPtr
    let mutable buf = Span<char>(p, 128)
    while 0 < fs.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.fs_read256() =
    use fs = File.OpenText csv
    let p = NativePtr.stackalloc<char> 256 |> NativePtr.toVoidPtr
    let mutable buf = Span<char>(p, 256)
    while 0 < fs.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.fs_readbyte() =
    use fs = File.OpenRead csv
    let mutable c = ' 'B
    for i in 0L..fs.Length - 1L do
      c <- fs.ReadByte() |> byte
    c

  // [<Benchmark>]
  // member __.mmf_readbyte() =
  //   use mmf = MemoryMappedFile.CreateFromFile(csv, FileMode.Open)
  //   use accessor = mmf.CreateViewStream()
  //   let mutable c = ' 'B
  //   for i in 0L..accessor.Length - 1L do
  //     c <- accessor.ReadByte() |> byte
  //   c

  [<Benchmark>]
  member __.mmf_readbyte_128() =
    use mmf = MemoryMappedFile.CreateFromFile(csv, FileMode.Open)
    use accessor = mmf.CreateViewStream()
    let p = NativePtr.stackalloc<byte> 128 |> NativePtr.toVoidPtr
    let mutable buf = Span<byte>(p, 128)
    while 0 < accessor.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.mmf_readbyte_256() =
    use mmf = MemoryMappedFile.CreateFromFile(csv, FileMode.Open)
    use accessor = mmf.CreateViewStream()
    let p = NativePtr.stackalloc<byte> 256 |> NativePtr.toVoidPtr
    let mutable buf = Span<byte>(p, 256)
    while 0 < accessor.Read(buf) do
      ()
    buf[0]

[<EntryPoint>]
let main args =
  BenchmarkRunner.Run<Benchmark>() |> ignore
  
  // let csv = "./sample.csv" |> System.IO.Path.GetFullPath
  // use fs = File.OpenRead csv

  // let p = NativePtr.stackalloc<byte> 1 |> NativePtr.toVoidPtr
  // let mutable buf = Span<byte>(p, 1)
  // fs.Read(buf) |> ignore
  // let c = buf[0]

  // let csv = "./sample.csv" |> System.IO.Path.GetFullPath
  // use mmf = MemoryMappedFile.CreateFromFile(csv, FileMode.Open)
  // use accessor = mmf.CreateViewStream()
  // let b = accessor.ReadByte() |> byte

  // let csv = "./sample.csv" |> System.IO.Path.GetFullPath
  // use mmf = MemoryMappedFile.CreateFromFile(csv, FileMode.Open)
  // use accessor = mmf.CreateViewAccessor()
  // let b = accessor.ReadChar(0)

  // let csv = "./sample.csv" |> System.IO.Path.GetFullPath
  // let p = NativePtr.stackalloc<char> 10 |> NativePtr.toVoidPtr
  // let mutable buf = Span<char>(p, 10)
  // use sr = File.OpenText csv
  
  // let x = sr.Read(buf)
  // let x = sr.Read(buf)

  0