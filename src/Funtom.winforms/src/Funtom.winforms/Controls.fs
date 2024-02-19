namespace Funtom.winforms

module controls =
  module private Button =
    let apply (btn: System.Windows.Forms.Button) p =
      match p with
      | Anchor _ | Size _ | Location _ | Text _ | Name _ | Control _ | Controls _ -> Ctrl.apply btn p
      | Command cmd -> btn.Command <- cmd
      | _ -> exn $"This property is not supported: %A{p}" |> raise

  let button (properties: Property array) =
    let btn = new System.Windows.Forms.Button()
    properties |> Array.iter (Button.apply btn)
    Control btn
