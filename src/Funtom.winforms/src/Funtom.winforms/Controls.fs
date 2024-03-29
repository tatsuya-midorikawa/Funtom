﻿namespace Funtom.winforms

module controls =

  module internals =
    let apply (ctrl: System.Windows.Forms.Control) p =
      match p with
        | Styles styles ->
          let apply' = function
            | Anchor anchor -> ctrl.Anchor <- Anchors.convert &anchor
            | Dock dock -> ctrl.Dock <- Dock.convert &dock
            | Size size -> ctrl.Size <- Size.convert &size
            | AutoSize auto_size -> ctrl.AutoSize <- auto_size
            | Position location -> ctrl.Location <- Position.convert &location
            | Location location -> ctrl.Location <- Location.to_point &location; ctrl.Size <- Location.to_size &location
            | Text text -> ctrl.Text <- text
            | Name name -> ctrl.Name <- name
            | Image img -> ctrl.BackgroundImage <- img
            | _ -> ()
          styles |> List.iter apply'
        | FlowBreak _ -> ()
        | Form form -> ctrl.Controls.Add form
        | MenuStrip menu -> ctrl.Controls.Add menu
        | Control c -> ctrl.Controls.Add c
        | Controls cs -> ctrl.Controls.AddRange (cs |> List.toArray)
        | _ -> exn $"This property is not supported: %A{p}" |> raise



  (* ----------------------------------------
   * Button
   * ---------------------------------------- *)
  module private Button =
    let apply (btn: System.Windows.Forms.Button) p =
      match p with
        | Command cmd -> btn.Command <- cmd
        | _ -> internals.apply btn p

  let button (properties: Property list) =
    let btn = new System.Windows.Forms.Button()
    btn.SuspendLayout ()
    properties |> List.iter (Button.apply btn)
    btn.ResumeLayout false
    Control btn
    
    
    
  (* ----------------------------------------
    * FlowLayoutPanel
    * ---------------------------------------- *)
  module private FlowLayoutPanel =
    let apply (panel: System.Windows.Forms.FlowLayoutPanel) p =
      let apply' = function Direction d -> panel.FlowDirection <- Direction.convert &d | _ -> ()
      match p with
        | Styles s -> s |> List.iter apply'
        | FlowBreak b -> panel.SetFlowBreak(panel.Controls.[panel.Controls.Count - 1], b)
        | _ -> ()
      internals.apply panel p
    
  let flow_layout (properties: Property list) =
    let panel = new System.Windows.Forms.FlowLayoutPanel()
    panel.SuspendLayout ()
    properties |> List.iter (FlowLayoutPanel.apply panel)
    panel.ResumeLayout false
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
    lbl.SuspendLayout ()
    properties |> List.iter (Label.apply lbl)
    lbl.ResumeLayout false
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
    txt.SuspendLayout ()
    properties |> List.iter (TextBox.apply txt)
    txt.ResumeLayout false
    Control txt



  (* ----------------------------------------
   * CheckBox
   * ---------------------------------------- *)
  module private CheckBox =
    let apply (chk: System.Windows.Forms.CheckBox) p =
      match p with
        | Styles styles ->
          let apply' = function
            | Checked c -> chk.Checked <- c
            | _ -> internals.apply chk p
          styles |> List.iter apply'
        | _ -> internals.apply chk p

  let check_box (properties: Property list) =
    let chk = new System.Windows.Forms.CheckBox()
    chk.SuspendLayout ()
    properties |> List.iter (CheckBox.apply chk)
    chk.ResumeLayout false
    Control chk



  (* ----------------------------------------
   * ComboBox
   * ---------------------------------------- *)
  module private ComboBox =
    let apply (cmb: System.Windows.Forms.ComboBox) p =
      match p with
        | Items items -> cmb.Items.AddRange items
        | _ -> internals.apply cmb p

    let rec get_index (xs: Property list) =
      match xs with
        | [] -> None
        | x::ys ->
          match x with
            | Styles styles ->
                styles 
                |> List.tryFind (function Index i -> true | _ -> false)
                |> (function 
                    | Some style -> match style with Index i -> Some i | _ -> None
                    | None -> None)
            | _ -> get_index ys

  let combo_box (properties: Property list) =
    let cmb = new System.Windows.Forms.ComboBox()
    cmb.SuspendLayout ()
    properties |> List.iter (ComboBox.apply cmb)
    match ComboBox.get_index properties with Some i -> cmb.SelectedIndex <- i | None -> ()
    cmb.ResumeLayout false
    Control cmb



  (* ----------------------------------------
   * GroupBox
   * ---------------------------------------- *)
  module private GroupBox =
    let apply (gb: System.Windows.Forms.GroupBox) p =
      match p with
        | _ -> internals.apply gb p

  let group (properties: Property list) =
    let gb = new System.Windows.Forms.GroupBox()
    gb.SuspendLayout ()
    properties |> List.iter (GroupBox.apply gb)
    gb.ResumeLayout false
    Control gb



  (* ----------------------------------------
   * RadioButton
   * ---------------------------------------- *)
  module private RadioButton =
    let apply (rb: System.Windows.Forms.RadioButton) p =
      match p with
        | _ -> internals.apply rb p

  let radio_button (properties: Property list) =
    let rb = new System.Windows.Forms.RadioButton()
    rb.SuspendLayout ()
    properties |> List.iter (RadioButton.apply rb)
    rb.ResumeLayout false
    Control rb



  (* ----------------------------------------
   * MenuStrip
   * ---------------------------------------- *)
  module private MenuStrip =
    let apply (menu: System.Windows.Forms.MenuStrip) p =
      match p with
        | MenuStripItem item -> menu.Items.Add item |> ignore
        | Command cmd -> menu.Click.Add cmd.Execute
        | _ -> internals.apply menu p

  let menu (properties: Property list) =
    let menu = new System.Windows.Forms.MenuStrip()
    menu.SuspendLayout ()
    properties |> List.iter (MenuStrip.apply menu)
    menu.ResumeLayout false
    MenuStrip menu



  (* ----------------------------------------
   * MenuStripItem
   * ---------------------------------------- *)
  module private MenuStripItem =
    let apply (item: System.Windows.Forms.ToolStripMenuItem) p =
      match p with
        | Styles styles ->
          let apply' = function
            | Anchor anchor -> item.Anchor <- Anchors.convert &anchor
            | Dock dock -> item.Dock <- Dock.convert &dock
            | Size size -> item.Size <- Size.convert &size
            | AutoSize auto_size -> item.AutoSize <- auto_size
            | Text text -> item.Text <- text
            | Name name -> item.Name <- name
            | Image img -> item.Image <- img
            | _ -> exn $"This property is not supported: %A{p}" |> raise
          styles |> List.iter apply'
        | MenuStripItem item' -> item.DropDownItems.Add item' |> ignore
        | Command cmd -> item.Click.Add cmd.Execute
        | _ -> exn $"This property is not supported: %A{p}" |> raise

  let menu_item (properties: Property list) =
    let item = new System.Windows.Forms.ToolStripMenuItem()
    properties |> List.iter (MenuStripItem.apply item)
    MenuStripItem item