open Funtom.text

let enc = System.Text.UTF8Encoding(true)
let csv = System.IO.Path.GetFullPath "./sample.csv"
for s in Csv.read (csv, enc) do
  printfn "%s" s