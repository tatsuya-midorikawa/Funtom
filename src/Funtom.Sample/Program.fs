open System.Net.Http
open type Funtom.Threading.Tasks.Task
open Funtom.Text
open Funtom.Collections.Generic

[<EntryPoint>]
let main argv =
  let mutable vsb = new ValueStringBuilder(100)
  let a = vsb.[5]

  //async {
  //  use client = new HttpClient()
  //  let! response = await (client.GetAsync("https://midoliy.com"))
  //  return! await (response.Content.ReadAsStringAsync())
  //}
  //|> Async.RunSynchronously
  //|> printfn "%s"

  0 
