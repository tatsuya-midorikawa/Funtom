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
  form [
    style [ id "Main form"; text "Hello F#!!"; size { width= 300<px>; height= 200<px> } ]

    menu [ 
      style [ text "menuStrip"; position { top= 0<px>; left= 0<px> } ] 
      menu_item [ 
        style [ text "AAA" ]
        menu_item [ style [ text "BBB" ] ]
      ]
      menu_item [ 
        style [ text "CCC" ]
        menu_item [ style [ text "DDDD" ] ]
      ] ]

    flow [
      style  [ dock Dock.fill; position { top= 48<px>; left= 0<px> } ]
      label  [ style [ text "Click ->"; anchor Anchors.none; auto_size true] ]
      button [
        style [ id "btn1"; text "Click me!"; anchor Anchors.none ]
        cmd (fun _ -> msg.show $"test" |> ignore)]
      input  [ style [ id "input1"; text ""; anchor Anchors.none] ]
      group [
        style [ text "radio group"; ]
        radio [ style [ text "radio1"; anchor Anchors.none; auto_size true] ]
        radio [ style [ text "radio2"; anchor Anchors.none; auto_size true] ]
      ]
    ]
  ]

let textbox =
  form |> document.get_elem_by_id "input1"

let on_btn1_click _ =
  msg.show $"{textbox.text}" |> ignore

let btn1 =
  form
  |> document.get_elem_by_id "btn1"
  |> document.add_event_listener (evt.click on_btn1_click)

form |> (show_dialog >> ignore)
