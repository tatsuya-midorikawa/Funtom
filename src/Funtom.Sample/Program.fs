open System.Net.Http
open type Funtom.Threading.Tasks.Task

[<EntryPoint>]
let main argv =
  
  async {
    use client = new HttpClient()
    let! response = await (client.GetAsync("https://midoliy.com"))
    return! await (response.Content.ReadAsStringAsync())
  }
  |> Async.RunSynchronously
  |> printfn "%s"

  0 
