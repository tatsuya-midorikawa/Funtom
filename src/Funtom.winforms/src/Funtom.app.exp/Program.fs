open Funtom.winforms.exp
open Funtom.winforms.exp.forms

//#nowarn "0760"

let log msg = System.Diagnostics.Debug.WriteLine msg

new form {
  property= [ size { width = 480<px>; height = 320<px> }; ]
  controls= [
    new button ([
      size { width = 100<px>; height = 50<px> }
      location { x = 10<px>; y = 10<px> }
      text "Click me!"
      on_click (fun _ -> log "Hello, World!")
      ])
  ]
}
|> controls.show_dialog
|> ignore