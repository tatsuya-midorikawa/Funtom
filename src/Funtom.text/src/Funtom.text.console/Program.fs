﻿open Funtom.text

let enc = System.Text.UTF8Encoding(true)
let csv = System.IO.Path.GetFullPath "./sample.csv"
for i in Csv.read (csv, enc) do
  printfn "%i" i