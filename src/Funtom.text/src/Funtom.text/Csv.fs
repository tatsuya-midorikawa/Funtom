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

  let public enumerate (csv: string, enc: Encoding) =
    let buf = Array.zeroCreate<byte> BufferSize
    let cache = StringBuilder(BufferSize * 2)
    let inline get_string(src: array<byte>, start, length) =
      let view = src.AsSpan().Slice(start, length)
      let str = cache.Append(enc.GetString(view)).ToString()
      cache.Clear() |> ignore
      str

    seq {    
      use fs = File.OpenRead csv
      // BOM がある場合, その Byte 数分読み飛ばす.
      fs.Seek(enc.Preamble.Length, SeekOrigin.Current) |> ignore
      let mutable length = fs.Read(buf)
      let mutable last = 0uy
      let mutable escaped = false
      let mutable quoted = false
      while 0 < length do
        let mutable last_idx = 0;
        for i = 0 to (length - 1) do
          match buf[i] with
          | CR ->
            if quoted
              then ()
              else
                let s = get_string(buf, last_idx, i - last_idx)
                last_idx <- i + 1
                yield s
          | LF ->
            if quoted
              then ()
              else
                if (0 < i && buf[i-1] = CR) || (i = 0 && last = CR)
                  then
                    // CRLF の場合, 改行文字として扱わなず, LF 分 last_idx を進める.
                    last_idx <- i + 1
                  else
                    // CRLF 以外の場合, 改行文字として処理する.
                    let s = get_string(buf, last_idx, i - last_idx)
                    last_idx <- i + 1
                    yield s
          | Backslash ->
            // flag を反転する.
            //    \  : true
            //    \\ : false
            escaped <- not escaped
          | DoubleQuotation ->
            // TODO: "" でエスケープできるパターンを追加する
            quoted <- not quoted
            //if escaped
            //  then quoted <- not quoted
            //  else ()
            escaped <- false
          | _ ->
            escaped <- false
            if i = length - 1
              then
                // 改行文字で終わっていない場合, StringBuilder でキャッシュを持つ
                // その際, ここでの yield は避け, 必ず CR or LF のところで yield する
                let view = buf.AsSpan().Slice(last_idx, length - last_idx)
                cache.Append(enc.GetString(view)) |> ignore
              else
                // 中間文字の場合, 特に何もせず読み飛ばす.
                ()
          if last = CR then last <- 0uy
        last <- buf[length - 1]
        length <- fs.Read(buf)
    }
    

    //use stream = File.OpenRead(path)
    //use reader = new StreamReader(stream, enc)
    //seq {
    //  while 0 <= reader.Peek() do
    //    let line = reader.ReadLine()
    //    line
    //}
    

