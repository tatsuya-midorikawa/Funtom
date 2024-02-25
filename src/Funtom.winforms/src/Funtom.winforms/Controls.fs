namespace Funtom.winforms

module controls =

  module internals =
    let apply (ctrl: System.Windows.Forms.Control) p =
      match p with
      | Styles styles ->
        let apply' =function
          | Anchor anchor -> ctrl.Anchor <- Anchors.convert &anchor
          | Dock dock -> ctrl.Dock <- Dock.convert &dock
          | Size size -> ctrl.Size <- Size.convert &size
          | AutoSize auto_size -> ctrl.AutoSize <- auto_size
          | Position location -> ctrl.Location <- Position.convert &location
          | Location location -> ctrl.Location <- Location.to_point &location; ctrl.Size <- Location.to_size &location
          | Text text -> ctrl.Text <- text
          | Name name -> ctrl.Name <- name
          | _ -> ()
        ctrl.SuspendLayout()
        styles |> List.iter apply'
        ctrl.ResumeLayout(false)
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

  let button (properties: Property list) =
    let btn = new System.Windows.Forms.Button()
    properties |> List.iter (Button.apply btn)
    Control btn
    
    
    
  (* ----------------------------------------
    * FlowLayoutPanel
    * ---------------------------------------- *)
  module private FlowLayoutPanel =
    let apply (panel: System.Windows.Forms.FlowLayoutPanel) p =
      let apply' = function Direction d -> panel.FlowDirection <- Direction.convert &d | _ -> ()
      match p with Styles s -> s |> List.iter apply' | _ -> ()
      internals.apply panel p
    
  let flow (properties: Property list) =
    let panel = new System.Windows.Forms.FlowLayoutPanel()
    properties |> List.iter (FlowLayoutPanel.apply panel)
    Control panel



  (* ----------------------------------------
   * Label
   * ---------------------------------------- *)
  module private Label =
    let apply (lbl: System.Windows.Forms.Label) p =
      match p with
      | _ -> internals.apply lbl p

  let label (properties: Property list) =
    let lbl = new System.Windows.Forms.Label()
    properties |> List.iter (Label.apply lbl)
    Control lbl



  (* ----------------------------------------
   * TextBox
   * ---------------------------------------- *)
  module private TextBox =
    let apply (txt: System.Windows.Forms.TextBox) p =
      match p with
      | _ -> internals.apply txt p

  let input (properties: Property list) =
    let txt = new System.Windows.Forms.TextBox()
    properties |> List.iter (TextBox.apply txt)
    Control txt