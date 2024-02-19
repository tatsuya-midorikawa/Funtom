open Funtom.winforms
open Funtom.winforms.forms
open Funtom.winforms.controls

#nowarn "3391"

form [|
  size { width= 300<px>; height= 200<px> }
  text "Hello F#!!"
  key "Main form"
  button [|
    text "click"
    size { width= 100<px>; height= 30<px> }
    location { top= 10<px>; left= 10<px>; }
    cmd (fun _ -> msg.show("test1") |> ignore)  // norwarn "3391" がないと警告がでる
    //cmd.build (fun _ -> System.Windows.Forms.MessageBox.Show("test2") |> ignore)  // 警告回避版
  |]
|]
|> show_dialog
|> ignore