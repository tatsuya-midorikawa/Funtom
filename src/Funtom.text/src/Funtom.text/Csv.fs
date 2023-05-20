namespace Funtom.text

#nowarn "9"

open System
open System.IO
open System.Text
open FSharp.NativeInterop

module Csv =
  [<Literal>]
  let private BufferSize = 512
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
    //let buf = Array.zeroCreate<byte> 512
    let cache = ResizeArray<byte>(BufferSize * 2)
    let buf = Array.zeroCreate<byte> 16
    seq {    
      use fs = File.OpenRead csv
      let mutable length = fs.Read(buf)
      let mutable i = 0

      while 0 < length do
        //let view = buf.AsSpan().Slice(0, length)
        let mutable last_idx = 0;
        for i = 0 to (length - 1) do
          match buf[i] with
          | CR ->
            let view = buf.AsSpan().Slice(last_idx, i - last_idx)
            yield enc.GetString(view)
          | LF ->            
            if 0 < i && buf[i-1] = CR
              // CRLF の場合, 改行文字として扱わない.
              then ()
              // CRLF 以外の場合, 改行文字として処理する.
              else
                let view = buf.AsSpan().Slice(last_idx, i - last_idx)
                yield enc.GetString(view)
            ()
          | _ ->
            if i = length - 1
              then 
                let view = buf.AsSpan().Slice(last_idx, length - last_idx)
                yield enc.GetString(view)
              else ()
        length <- fs.Read(buf)
    }
    

    //use stream = File.OpenRead(path)
    //use reader = new StreamReader(stream, enc)
    //seq {
    //  while 0 <= reader.Peek() do
    //    let line = reader.ReadLine()
    //    line
    //}
    

