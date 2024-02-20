open Funtom.winforms
open Funtom.winforms.forms
open Funtom.winforms.controls

open System.Threading.Tasks

#nowarn "3391"

let debug msg = System.Diagnostics.Debug.WriteLine msg

let mutable flag = true

form [|
  size { width= 300<px>; height= 200<px> }
  text "Hello F#!!"
  id "Main form"
  button [|
    text "click"
    size { width= 100<px>; height= 30<px> }
    location { top= 10<px>; left= 10<px>; }
    cmd (
      exec= 
        (fun (self, _) ->
          flag <- false
          msg.show("test") |> ignore
          task {
            debug "### start"
            do! Task.Delay 5000
            flag <- true
            debug "### end"
            self.notify()
          }
          |> ignore), 
      can_exec=
        (fun _ -> flag))
    //cmd (fun _ -> msg.show("test1") |> ignore)  // norwarn "3391" がないと警告がでる
    //cmd.build (fun _ -> System.Windows.Forms.MessageBox.Show("test2") |> ignore)  // 警告回避版
  |]
|]
|> show_dialog
|> ignore

let btn = button [|
  text "click"
  size { width= 100<px>; height= 30<px> }
  location { top= 10<px>; left= 10<px>; }
  cmd (
    exec= 
      (fun (self, _) -> 
        flag <- false
        task {
          debug "### start"
          do! Task.Delay 5000
          flag <- true
          debug "### end"
          self.notify()
        }
        |> ignore), 
    can_exec=
      (fun _ -> flag))
|]

btn
|> document.add_event_listner (evt.click (fun _ -> msg.show "test" |> ignore))
|> ignore