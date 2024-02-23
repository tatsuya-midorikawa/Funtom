open Funtom.winforms
open Funtom.winforms.forms
open Funtom.winforms.controls

open System.Threading.Tasks

#nowarn "3391"

let debug msg = System.Diagnostics.Debug.WriteLine msg

let mutable flag = true
//let btn = button [|
//  text "click"; location { top= 10<px>; left= 120<px>; right= 220<px>; bottom= 40<px> }; anchor Anchors.none
//  cmd (
//    exec= 
//      (fun (self, _) ->
//        flag <- false
//        msg.show("test") |> ignore
//        task {
//          debug "### start"
//          do! Task.Delay 5000
//          flag <- true
//          debug "### end"
//          self.notify()
//        }
//        |> ignore), 
//    can_exec=
//      (fun _ -> flag))
//  //cmd (fun _ -> msg.show("test1") |> ignore)  // norwarn "3391" がないと警告がでる
//  //cmd.build (fun _ -> System.Windows.Forms.MessageBox.Show("test2") |> ignore)  // 警告回避版
//|]

let form =
  form [|
    id "Main form"; text "Hello F#!!"; size { width= 300<px>; height= 200<px> }
    flow [| 
      dock Dock.fill; direction Direction.left_to_right
      label [| text "Click ->"; anchor Anchors.none; auto_size true |]
      button [| id "btn1"; text "Click me!"; anchor Anchors.none |] |]
  |]

form
|> document.get_elem_by_id "btn1"
|> document.add_event_listener (evt.click (fun _ -> msg.show "test" |> ignore))
|> ignore

form |> (show_dialog >> ignore)

