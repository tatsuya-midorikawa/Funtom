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
  member __.streamreader_readline() =
    use sr = File.OpenText csv
    let mutable s = ""
    while 0 <= sr.Peek() do
      s <- sr.ReadLine()
    s

  [<Benchmark>]
  member __.streamreader_readtoend() =
    use sr = File.OpenText csv
    let s = sr.ReadToEnd()
    s.Length

  // [<Benchmark>]
  // member __.streamreader_read() =
  //   use sr = File.OpenText csv
  //   let mutable c = ' '
  //   while 0 <= sr.Read() do ()
  //   c

  [<Benchmark>]
  member __.streamreader_read__128() =
    use fs = File.OpenText csv
    let p = NativePtr.stackalloc<char> 128 |> NativePtr.toVoidPtr
    let mutable buf = Span<char>(p, 128)
    while 0 < fs.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.streamreader_read__256() =
    use fs = File.OpenText csv
    let p = NativePtr.stackalloc<char> 256 |> NativePtr.toVoidPtr
    let mutable buf = Span<char>(p, 256)
    while 0 < fs.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.streamreader_read__512() =
    use fs = File.OpenText csv
    let p = NativePtr.stackalloc<char> 512 |> NativePtr.toVoidPtr
    let mutable buf = Span<char>(p, 512)
    while 0 < fs.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.streamreader_read_1024() =
    use fs = File.OpenText csv
    let p = NativePtr.stackalloc<char> 1024 |> NativePtr.toVoidPtr
    let mutable buf = Span<char>(p, 1024)
    while 0 < fs.Read(buf) do
      ()
    buf[0]

  // [<Benchmark>]
  // member __.filestream_read() =
  //   use fs = File.OpenRead csv
  //   let p = NativePtr.stackalloc<byte> 1 |> NativePtr.toVoidPtr
  //   let mutable buf = Span<byte>(p, 1)
  //   for i in 0L..fs.Length - 1L do
  //     fs.Read(buf) |> ignore
  //   buf[0]

  [<Benchmark>]
  member __.filestream_read__128_stack() =
    use fs = File.OpenRead csv
    let p = NativePtr.stackalloc<byte> 128 |> NativePtr.toVoidPtr
    let mutable buf = Span<byte>(p, 128)
    while 0 < fs.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.filestream_read__256_stack() =
    use fs = File.OpenRead csv
    let p = NativePtr.stackalloc<byte> 256 |> NativePtr.toVoidPtr
    let mutable buf = Span<byte>(p, 256)
    while 0 < fs.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.filestream_read__512_stack() =
    use fs = File.OpenRead csv
    let p = NativePtr.stackalloc<byte> 512 |> NativePtr.toVoidPtr
    let mutable buf = Span<byte>(p, 512)
    while 0 < fs.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.filestream_read_1024_stack() =
    use fs = File.OpenRead csv
    let p = NativePtr.stackalloc<byte> 1024 |> NativePtr.toVoidPtr
    let mutable buf = Span<byte>(p, 1024)
    while 0 < fs.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.filestream_read__128_array() =
    use fs = File.OpenRead csv
    let buf = Array.zeroCreate<byte> 128
    while 0 < fs.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.filestream_read__256_array() =
    use fs = File.OpenRead csv
    let buf = Array.zeroCreate<byte> 256
    while 0 < fs.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.filestream_read__512_array() =
    use fs = File.OpenRead csv
    let buf = Array.zeroCreate<byte> 512
    while 0 < fs.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.filestream_read_1024_array() =
    use fs = File.OpenRead csv
    let buf = Array.zeroCreate<byte> 1024
    while 0 < fs.Read(buf) do
      ()
    buf[0]

  // [<Benchmark>]
  // member __.fs_readbyte() =
  //   use fs = File.OpenRead csv
  //   let mutable c = ' 'B
  //   for i in 0L..fs.Length - 1L do
  //     c <- fs.ReadByte() |> byte
  //   c

  // [<Benchmark>]
  // member __.mmf_readbyte() =
  //   use mmf = MemoryMappedFile.CreateFromFile(csv, FileMode.Open)
  //   use accessor = mmf.CreateViewStream()
  //   let mutable c = ' 'B
  //   for i in 0L..accessor.Length - 1L do
  //     c <- accessor.ReadByte() |> byte
  //   c 

  // [<Benchmark>]
  // member __.mmf_accsessor_read__128_stack() =
  //   let fi = FileInfo csv
  //   use mmf = MemoryMappedFile.CreateFromFile(csv, FileMode.Open)
  //   use accessor = mmf.CreateViewAccessor()
  //   let mutable buf = 0uy
  //   for i in 0L .. (fi.Length - 1L) do
  //     accessor.Read(i, &buf)
  //   buf

  [<Benchmark>]
  member __.mmf_streamread__128_stack() =
    use mmf = MemoryMappedFile.CreateFromFile(csv, FileMode.Open)
    use accessor = mmf.CreateViewStream()
    let p = NativePtr.stackalloc<byte> 128 |> NativePtr.toVoidPtr
    let mutable buf = Span<byte>(p, 128)
    while 0 < accessor.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.mmf_streamread__256_stack() =
    use mmf = MemoryMappedFile.CreateFromFile(csv, FileMode.Open)
    use accessor = mmf.CreateViewStream()
    let p = NativePtr.stackalloc<byte> 256 |> NativePtr.toVoidPtr
    let mutable buf = Span<byte>(p, 256)
    while 0 < accessor.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.mmf_streamread__512_stack() =
    use mmf = MemoryMappedFile.CreateFromFile(csv, FileMode.Open)
    use accessor = mmf.CreateViewStream()
    let p = NativePtr.stackalloc<byte> 512 |> NativePtr.toVoidPtr
    let mutable buf = Span<byte>(p, 512)
    while 0 < accessor.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.mmf_streamread_1024_stack() =
    use mmf = MemoryMappedFile.CreateFromFile(csv, FileMode.Open)
    use accessor = mmf.CreateViewStream()
    let p = NativePtr.stackalloc<byte> 1024 |> NativePtr.toVoidPtr
    let mutable buf = Span<byte>(p, 1024)
    while 0 < accessor.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.mmf_streamread__128_array() =
    use mmf = MemoryMappedFile.CreateFromFile(csv, FileMode.Open)
    use accessor = mmf.CreateViewStream()
    let mutable buf = Array.zeroCreate<byte>(128)
    while 0 < accessor.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.mmf_streamread__256_array() =
    use mmf = MemoryMappedFile.CreateFromFile(csv, FileMode.Open)
    use accessor = mmf.CreateViewStream()
    let mutable buf = Array.zeroCreate<byte>(256)
    while 0 < accessor.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.mmf_streamread__512_array() =
    use mmf = MemoryMappedFile.CreateFromFile(csv, FileMode.Open)
    use accessor = mmf.CreateViewStream()
    let mutable buf = Array.zeroCreate<byte>(512)
    while 0 < accessor.Read(buf) do
      ()
    buf[0]

  [<Benchmark>]
  member __.mmf_streamread_1024_array() =
    use mmf = MemoryMappedFile.CreateFromFile(csv, FileMode.Open)
    use accessor = mmf.CreateViewStream()
    let mutable buf = Array.zeroCreate<byte>(1024)
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


  // let csv = "./sample.csv" |> System.IO.Path.GetFullPath
  // use mmf = MemoryMappedFile.CreateFromFile(csv, FileMode.Open)
  // use accessor = mmf.CreateViewStream()
  // let p = NativePtr.stackalloc<byte> 128 |> NativePtr.toVoidPtr
  // let mutable buf = Span<byte>(p, 128)
  // while 0 < accessor.Read(buf) do
  //   ()

  // '\n' |> Convert.ToByte |> printfn "%A"
  // '\r' |> Convert.ToByte |> printfn "%A"
  // 'a' |> Convert.ToByte |> printfn "%A"

  0