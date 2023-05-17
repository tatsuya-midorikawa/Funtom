namespace Funtom.text

#nowarn "9"

open System
open System.IO
open System.Text
open FSharp.NativeInterop

module Csv =

  [<Literal>]
  let LF = 0x0Auy
  [<Literal>]
  let CR = 0x0Duy
  [<Literal>]
  let HT = 0x09uy
  [<Literal>]
  let VT = 0x0buy
  [<Literal>]
  let DoubleQuotation = 0x22uy
  [<Literal>]
  let SingleQuotation = 0x27uy
  [<Literal>]
  let Comma = 0x2Cuy
  [<Literal>]
  let Backslash = 0x5Cuy

  let public read (csv: string, enc: Encoding) =
    use fs = File.OpenRead csv
    let p = NativePtr.stackalloc<byte> 512 |> NativePtr.toVoidPtr
    let buf = Array.zeroCreate<byte> 512
    seq {
      let mutable length = fs.Read(buf)
      while 0 < length do
        let view = buf.AsSpan().Slice(0, length)
        ""
    } |> ignore
    
    buf[0]

    //use stream = File.OpenRead(path)
    //use reader = new StreamReader(stream, enc)
    //seq {
    //  while 0 <= reader.Peek() do
    //    let line = reader.ReadLine()
    //    line
    //}
    

