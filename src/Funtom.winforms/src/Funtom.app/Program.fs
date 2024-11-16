open Funtom.winforms
//open Funtom.winforms.forms
//open Funtom.winforms.controls
//open Funtom.winforms.dialogs
open System.Threading

open Funtom.winforms.values
open Funtom.winforms.exp
#nowarn "3391"

let debug msg = System.Diagnostics.Debug.WriteLine msg

Application.initialize()

//let button = button {
//  text "Click me!"
//}

let form = form () {
  suspend
  size { width= 800; height= 600 }
  children [|
    flowlayout (Dock.fill, Direction.topdown) {
      children [|
        button () { text "Click me 1"; size { width= 200; height= 100 } }
        button () { text "Click me 2"; size { width= 300; height= 150 } }
        button () { text "Click me 3"; size { width= 150; height= 200 } } 
      |]
    }
    //button () { 
    //  text "Click me 1"
    //  location { top = 0; left = 0 } } 
    //button () { 
    //  text "Click me 2" 
    //  location { top = 20; left = 0 } }
    //button () {
    //  text "Click me 3"
    //  location { top = 40; left = 0 } } 
  |]
  resume
}

[<System.STAThread>]
do
  Application.run form


//let syncContext = System.Threading.SynchronizationContext.Current
//let get_thread_id() =
//  let mutable id = -1
//  if System.Threading.SynchronizationContext.Current = null
//    then ()
//    else
//      System.Threading.SynchronizationContext.Current.Send ((fun _ -> id <- System.Threading.Thread.CurrentThread.ManagedThreadId), null)
//  debug $"ManagedThreadId= {System.Threading.Thread.CurrentThread.ManagedThreadId} / id= {id}"
//  id = System.Threading.Thread.CurrentThread.ManagedThreadId
//let dir_dlg = new dir_browser()
//let file_dlg = new file_dialog(index= 0, filter= "")

//let form =
//  form [
//    style [
//      icon "./phantom.ico"
//      id "Main form"; text "Hello F#!!"; size { width= 640<px>; height= 480<px> } ]

//    menu [ 
//      style [ text "menuStrip"; position { top= 0<px>; left= 0<px> } ]
//      //cmd (fun _ -> msg.show "menuStrip" |> ignore)  // norwarn "3391" がないと警告がでる

//      menu_item [ 
//        style [ text "AAA"; bitmap "./phantom_16x16.png" ]
//        menu_item [
//          style [ id "menu"; text "BBB" ]
//          cmd.build (fun _ -> dir_dlg.show() |> ignore)  (* 警告回避版 *) ] ]

//      menu_item [ 
//        style [ text "CCC" ]
//        menu_item [ style [ text "DDD" ] ] ]

//    ]

//    flow_layout [
//      style [ dock Dock.fill; ]

//      label [ style [ text "Click ->"; anchor Anchors.none; auto_size true] ]

//      button [
//        style [ id "btn1"; text "Click me!"; anchor Anchors.none ] ]

//      input [ style [ id "input1"; text ""; anchor Anchors.none] ]

//      flow_break

//      group [
//        style [ text "radio group";  ]
//        flow_layout [
//          style [ dock Dock.fill ]
//          radio_button [ style [ text "radio1"; anchor Anchors.none; auto_size true] ]
//          radio_button [ style [ text "radio2"; anchor Anchors.none; auto_size true] ] ]
//      ]

//      check_box [ 
//        style [ selected true; text "sample content"; auto_size true ] ]

//      combo_box [
//        style [ auto_size true; index 1 ]
//        items [ "aaa"; "bbb"; "ccc"; ] ]
//    ]
//  ]

//let textbox =
//  form |> document.get_elem_by_id "input1"

//let on_btn1_click _ =
//  System.Threading.Tasks.Task.Run(fun _ ->
//    debug $"btn task: {get_thread_id()}"
//    msg.show $"{textbox.text}" |> ignore
//  )
//  |> ignore

//let btn1 =
//  form
//  |> document.get_elem_by_id "btn1"
//  |> document.add_event_listener (evt.click on_btn1_click)

//// Create main() since STAThread is required for dir_dlg.show(), etc. to work.
//[<EntryPoint; System.STAThread>]
//let main _ =
//  debug $"main: {get_thread_id()}"
//  form |> (show_dialog >> ignore)
//  0
