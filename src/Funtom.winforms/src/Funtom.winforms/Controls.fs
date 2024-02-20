namespace Funtom.winforms

module controls =
  (* ----------------------------------------
   * Button
   * ---------------------------------------- *)
  module private Button =
    let apply (btn: System.Windows.Forms.Button) p =
      match p with
      #if NET8_0
      | Command cmd -> btn.Command <- cmd
      #endif
      | _ -> Ctrl.apply btn p

  let button (properties: Property array) =
    let btn = new System.Windows.Forms.Button()
    properties |> Array.iter (Button.apply btn)
    Control btn

  (* ----------------------------------------
   * Label
   * ---------------------------------------- *)
  module private Label =
    let apply (lbl: System.Windows.Forms.Label) p =
      match p with
      | _ -> Ctrl.apply lbl p

  let label (properties: Property array) =
    let lbl = new System.Windows.Forms.Label()
    properties |> Array.iter (Label.apply lbl)
    Control lbl