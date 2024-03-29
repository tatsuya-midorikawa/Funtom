#I @"C:\Program Files\dotnet\shared\Microsoft.WindowsDesktop.App\8.0.2"
#r @"System.Windows.Forms"
#r @"System.Drawing.Common"
#load @".\lib\Funtom.winforms.lit.fsx"

open Funtom.winforms.lit
open Funtom.winforms.lit.dialogs

#nowarn "3391"

[<System.STAThread>]
let main args =
  let dir_dlg = new dir_browser()
  let file_dlg = new file_dialog(index= 0, filter= "")

  let form =
    form [
      style [
        icon "./assets/phantom.ico"
        id "Main form"; text "Hello F#!!"; size { width= 640<px>; height= 480<px> } ]

      menu [ 
        style [ text "menuStrip"; position { top= 0<px>; left= 0<px> } ]

        menu_item [ 
          style [ text "AAA"; bitmap "./assets/phantom_16x16.png" ]
          menu_item [
            style [ id "menu"; text "BBB" ]
            cmd (fun _ -> dir_dlg.show() |> ignore)  ] ]

        menu_item [ 
          style [ text "CCC" ]
          menu_item [ style [ text "DDD" ] ] ]

      ]

      flow_layout [
        style [ dock Dock.fill; ]

        label [ style [ text "Click ->"; anchor Anchors.none; auto_size true] ]

        button [
          style [ id "btn1"; text "Click me!"; anchor Anchors.none ] ]

        input [ style [ id "input1"; text ""; anchor Anchors.none] ]

        flow_break

        group [
          style [ text "radio group";  ]
          flow_layout [
            style [ dock Dock.fill ]
            radio_button [ style [ text "radio1"; anchor Anchors.none; auto_size true] ]
            radio_button [ style [ text "radio2"; anchor Anchors.none; auto_size true] ] ]
        ]

        check_box [ 
          style [ selected true; text "sample content"; auto_size true ] ]

        combo_box [
          style [ auto_size true; index 1 ]
          items [ "aaa"; "bbb"; "ccc"; ] ]
      ]
    ]

  let textbox =
    form |> document.get_elem_by_id "input1"

  let on_btn1_click _ =
    msg.show $"test" |> ignore

  let btn1 =
    form
    |> document.get_elem_by_id "btn1"
    |> document.add_event_listener (evt.click on_btn1_click)

  form
  |> show_dialog

main () |> ignore