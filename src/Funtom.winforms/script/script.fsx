#I @"C:\Program Files\dotnet\shared\Microsoft.WindowsDesktop.App\8.0.1"
#r @"System.Windows.Forms"
#r @"System.Drawing.Common"
#load @".\Funtom.winforms.lit.fsx"

open Funtom.winforms.lit

#nowarn "3391"

[<System.STAThread>]
let main args =
  form [
    style [ 
      text "F# GUI Application"; size { width= 320<px>; height= 240<px> }
    ]
      
    label [ 
      style [ 
        text "Hello F#!!"; anchor Anchors.none; auto_size true
      ] 
    ]
  ]
  |> show_dialog

main () |> ignore