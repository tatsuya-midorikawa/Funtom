namespace Funtom.text

open System.IO
open System.Text

module Csv =
  let inline public read (path: string, enc: Encoding) = 
    use stream = File.OpenRead(path)
    use reader = new StreamReader(stream, enc)
    seq {
      while 0 <= reader.Peek() do
        let line = reader.ReadLine()
        line
    }
    

