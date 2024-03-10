open System
open System.IO
open System.Diagnostics

let src = 
  if File.Exists "./script.fsx"
    then Path.GetFullPath "./script.fsx"
    else Path.GetFullPath "../script.fsx"
let script = File.ReadAllText src
let psi = new ProcessStartInfo("dotnet", "--list-runtimes", CreateNoWindow= true, RedirectStandardOutput= true)
let dotnet = Process.Start(psi)
let runtime_info = 
  dotnet.StandardOutput.ReadToEnd().Split([|'\n'; '\r'|], StringSplitOptions.RemoveEmptyEntries)
  |> Array.filter (fun r -> r.StartsWith "Microsoft.WindowsDesktop.App")
  |> Array.last
  |> _.Split()
  |> Array.map (fun v -> v.Replace("[", "").Replace("]", ""))

let runtime =
  runtime_info[2..]
  |> Array.fold (fun acc v -> if acc = "" then v else $"{acc} {v}") ""
  |> (fun p -> Path.Combine(p, runtime_info[1]))

using (File.CreateText src) (fun file -> 
  file.WriteLine $"#I @\"{runtime}\""
  file.Write script )