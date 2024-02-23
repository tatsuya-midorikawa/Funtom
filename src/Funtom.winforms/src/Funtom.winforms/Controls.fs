namespace Funtom.winforms

module controls =

  module internals =
    let apply (ctrl: System.Windows.Forms.Control) p =
      match p with
      | Anchor anchor -> ctrl.Anchor <- Anchors.convert &anchor
      | Dock dock -> ctrl.Dock <- Dock.convert &dock
      | Size size -> ctrl.Size <- Size.convert &size
      | AutoSize auto_size -> ctrl.AutoSize <- auto_size
      | Position location -> ctrl.Location <- Position.convert &location
      | Location location -> ctrl.Location <- Location.to_point &location; ctrl.Size <- Location.to_size &location
      | Text text -> ctrl.Text <- text
      | Name name -> ctrl.Name <- name
      | Form form -> ctrl.Controls.Add form
      | Control c -> ctrl.Controls.Add c
      | Controls cs -> ctrl.Controls.AddRange (cs |> List.toArray)
      //#if NET8_0
      //| Command cmd -> ctrl.Click.Add cmd.Execute
      //#endif
      #if NET481
      | Command cmd -> ctrl.Click.Add cmd
      #endif
      | _ -> exn $"This property is not supported: %A{p}" |> raise



  (* ----------------------------------------
   * Button
   * ---------------------------------------- *)
  module private Button =
    let apply (btn: System.Windows.Forms.Button) p =
      match p with
      #if NET8_0
      | Command cmd -> btn.Command <- cmd
      #endif
      | _ -> internals.apply btn p

  let button (properties: Property array) =
    let btn = new System.Windows.Forms.Button()
    properties |> Array.iter (Button.apply btn)
    Control btn
    
    
    
  (* ----------------------------------------
    * FlowLayoutPanel
    * ---------------------------------------- *)
  module private FlowLayoutPanel =
    let apply (panel: System.Windows.Forms.FlowLayoutPanel) p =
      match p with
      | Direction direction -> panel.FlowDirection <- Direction.convert &direction
      | _ -> internals.apply panel p
    
  let flow (properties: Property array) =
    let panel = new System.Windows.Forms.FlowLayoutPanel()
    panel.Dock <- System.Windows.Forms.DockStyle.Fill
    properties |> Array.iter (FlowLayoutPanel.apply panel)
    Control panel



  (* ----------------------------------------
   * Label
   * ---------------------------------------- *)
  module private Label =
    let apply (lbl: System.Windows.Forms.Label) p =
      match p with
      | _ -> internals.apply lbl p

  let label (properties: Property array) =
    let lbl = new System.Windows.Forms.Label()
    properties |> Array.iter (Label.apply lbl)
    Control lbl