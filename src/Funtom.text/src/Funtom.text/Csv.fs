namespace Funtom.text

open System.IO
open System.Text

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

  let inline public read (path: string, enc: Encoding) = 
    use stream = File.OpenRead(path)
    use reader = new StreamReader(stream, enc)
    seq {
      while 0 <= reader.Peek() do
        let line = reader.ReadLine()
        line
    }
    

