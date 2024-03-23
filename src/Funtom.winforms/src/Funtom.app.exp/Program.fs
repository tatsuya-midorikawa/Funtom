open Funtom.winforms.exp
open Funtom.winforms.exp.forms

#nowarn "0760"

let log msg = System.Diagnostics.Debug.WriteLine msg

// Control can be created using the Tuple value (?style: Style list, ?ctrls: Control list).
// `style` and `ctrls` are optional, respectively, as are the parameter names.
form (
  style= [ 
    size { width = 480<px>; height = 320<px> }
    icon "./phantom.ico" 
    text "Hello F#!!"
  ],
  ctrls= [
    menustrip (
      style= [ location { x = 0<px>; y = 0<px> }; text "menuStrip" ],
      ctrls= [
        menuitem (
          style= [ text "AAA"; img "./phantom_16x16.png" ],
          ctrls= [ 
            // The following is an example of omitting ctrls.
            menuitem (style= [ text "BBB" ]) ]
        )
        menuitem (
          style= [ text "CCC" ],
          ctrls= [ 
            // The following is an example of omitting ctrls and parameter name.
            menuitem ([ text "DDD" ]) ]
        )
      ]
    )
    // Control can also be created using the Property record.
    // Note that the `style` and `ctrls` specifications are mandatory when using the Property record.
    flowlayout {
      style= [ dock Dock.fill ]
      ctrls= [
        label ([ text "Click ->"; anchor Anchor.none; auto_size true ])
        button ([
            size { width = 100<px>; height = 50<px> }
            location { x = 10<px>; y = 10<px> }
            text "Click me!"
            on_click (fun _ -> log "Hello, World!")
          ])
        textbox ([
            size { width = 200<px>; height = 50<px> }
            location { x = 10<px>; y = 70<px> }
            text "foo"
          ])
        //flowbreak
        label ([ text "Click2 ->"; anchor Anchor.none; auto_size true ])
        button ([
            size { width = 100<px>; height = 50<px> }
            location { x = 10<px>; y = 10<px> }
            text "Click2 me!"
            on_click (fun _ -> log "Hello, World2!")
          ])
        textbox ([
            size { width = 200<px>; height = 50<px> }
            location { x = 10<px>; y = 70<px> }
            text "bar"
          ])

      ]
    }
  ]
)
|> controls.show_dialog
|> ignore