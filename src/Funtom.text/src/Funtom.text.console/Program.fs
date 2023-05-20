open Funtom.text

//let enc = System.Text.UTF8Encoding(false)
let enc = System.Text.UTF8Encoding(true)
let csv = System.IO.Path.GetFullPath "./sample.csv"
for s in Csv.enumerate (csv, enc) do
  printfn "%s" s
  printfn "----------"