open Funtom.winforms.exp
open Funtom.winforms.exp.forms

//#nowarn "0760"

let log msg = System.Diagnostics.Debug.WriteLine msg

new form {
  style= [ 
    size { width = 480<px>; height = 320<px> }
    icon "./phantom.ico" 
    text "Hello F#!!"
  ]
  ctrls= [
    new menustrip {
      style= [ location { x = 0<px>; y = 0<px> }; text "menuStrip" ]
      ctrls= [
        new menuitem {
          style= [ text "AAA"; img "./phantom_16x16.png" ]
          ctrls= [ 
            new menuitem { style= [ text "BBB" ]; ctrls= [] }
          ]
        }
        new menuitem {
          style= [ text "CCC" ]
          ctrls= [
            new menuitem { style= [ text "DDD" ]; ctrls= [] }
          ]
        }
      ]
    }
    new flowlayout {
      style= [ dock Dock.fill ]
      ctrls= [
        new label ([ text "Click ->"; anchor Anchor.none; auto_size true ])
        new button ([
            size { width = 100<px>; height = 50<px> }
            location { x = 10<px>; y = 10<px> }
            text "Click me!"
            on_click (fun _ -> log "Hello, World!")
          ])
        new textbox {
          style= [
            size { width = 200<px>; height = 50<px> }
            location { x = 10<px>; y = 70<px> }
            text ""
          ]
          ctrls= []
        }
      ]
    }
  ]
}
|> controls.show_dialog
|> ignore