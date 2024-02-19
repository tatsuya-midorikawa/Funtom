namespace Funtom.winforms

module controls =
  module private Button =
    let apply (btn: System.Windows.Forms.Button) p =
      match p with
      #if NET8_0_OR_GREATER
      | Command cmd -> btn.Command <- cmd
      #endif
      #if NET48_OR_GREATER
      | Command cmd -> btn.Click.Add cmd
      #endif
      | _ -> Ctrl.apply btn p

  let button (properties: Property array) =
    let btn = new System.Windows.Forms.Button()
    properties |> Array.iter (Button.apply btn)
    Control btn
