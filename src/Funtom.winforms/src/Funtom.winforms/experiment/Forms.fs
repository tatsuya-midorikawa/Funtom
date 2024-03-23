namespace Funtom.winforms.exp

open System.Windows.Forms
open System.Runtime.CompilerServices

module controls =

  let apply (style: Style) (ctrl: 'T when 'T :> Control) = 
    match style with
      | Size s -> ctrl.Size <- s
      | Location l -> ctrl.Location <- l
      | Anchor a -> ctrl.Anchor <- a
      | Dock d -> ctrl.Dock <- d
      | AutoSize s -> ctrl.AutoSize <- s
      | Text t -> ctrl.Text <- t
      | Name n -> ctrl.Name <- n
      | BackgroundImage img -> ctrl.BackgroundImage <- img
      | Command cmd -> ctrl.Click.Add(cmd)
      | _ -> raise (exn $"Not supported styles: {style} ({ctrl})")

  (*
  * Common Control Functions
  *)
  let inline show (self: 'T when 'T :> Control) = self.Show(); self
  let inline hide (self: 'T when 'T :> Control) = self.Hide(); self
  let inline add (ctrl: 'U when 'U :> Control) (self: 'T when 'T :> Control) = self.Controls.Add(ctrl); self
  let inline add_range (ctrls: Control list) (self: 'T when 'T :> Control) = self.Controls.AddRange(ctrls |> List.toArray); self
  let inline remove (ctrl: 'U when 'U :> Control) (self: 'T when 'T :> Control) = self.Controls.Remove(ctrl); self
  let inline suspend (self: 'T when 'T :> Control) = self.SuspendLayout(); self
  let inline resume (perform_layout: bool) (self: 'T when 'T :> Control) = self.ResumeLayout(perform_layout); self
  let inline controls (self: 'T when 'T :> Control) = self.Controls
  let inline children (self: 'T when 'T :> Control) = self.Controls |> Seq.cast<Control> |> List.ofSeq
  
  (*
  * Form Functions
  *)
  let inline show_dialog (self: Form) = self.ShowDialog()
  let inline close (self: Form) = self.Close()



module forms =
  // ------------------------------------------
  // System.Windows.Forms.Form
  // ------------------------------------------
  type form (property: Property) as self =
    inherit System.Windows.Forms.Form()
    do
      let rec apply (styles: Style list) (frm: System.Windows.Forms.Form) =
        match styles with
          | [] -> frm
          | style::rest -> 
              match style with
                | Icon ico -> frm.Icon <- ico
                | _ -> controls.apply style frm
              apply rest frm

      self
      |> controls.suspend
      |> apply property.property
      |> controls.add_range property.controls
      |> controls.resume false
      |> ignore

    new (styles: Style list) = new form { property= styles; controls= [] }
    new (controls: Control list) = new form { property= []; controls= controls }
    

  // ------------------------------------------
  // System.Windows.Forms.Button
  // ------------------------------------------
  type button (property: Property) as self =
    inherit System.Windows.Forms.Button()
    do
      let rec apply (styles: Style list) (btn: System.Windows.Forms.Button) =
        match styles with
          | [] -> btn
          | style::rest -> 
              match style with
                | Image (img, align) -> btn.Image <- img; btn.ImageAlign <- align
                | _ -> controls.apply style btn
              apply rest btn
      self
      |> controls.suspend
      |> apply property.property
      |> controls.add_range property.controls
      |> controls.resume false
      |> ignore

    new (styles: Style list) = new button { property= styles; controls= [] }
    new (controls: Control list) = new button { property= []; controls= controls }


  // ------------------------------------------
  // System.Windows.Forms.FlowLayoutPanel
  // ------------------------------------------
  type FlowBreak() = inherit System.Windows.Forms.Control()
  let flowbreak = new FlowBreak()

  type flowlayout (property: Property) as self =
    inherit System.Windows.Forms.FlowLayoutPanel()
    do
      let rec apply (styles: Style list) (panel: System.Windows.Forms.FlowLayoutPanel) =
        match styles with
          | [] -> panel
          | style::rest -> 
              match style with
                | Direction d -> panel.FlowDirection <- d
                | _ -> controls.apply style panel
              apply rest panel

      let rec add_range (controls: Control list) (panel: System.Windows.Forms.FlowLayoutPanel) =
        match controls with
          | [] -> panel
          | ctrl::rest ->
              match ctrl with
                | :? FlowBreak ->
                  if panel.Controls.Count > 0 then
                    panel.SetFlowBreak(panel.Controls.[panel.Controls.Count - 1], true)
                | _ -> panel.Controls.Add(ctrl)
              add_range rest panel

      self
      |> controls.suspend
      |> apply property.property
      |> add_range property.controls
      |> controls.resume false
      |> ignore

    new (styles: Style list) = new flowlayout { property= styles; controls= [] }
    new (controls: Control list) = new flowlayout { property= []; controls= controls }


  
  // ------------------------------------------
  // System.Windows.Forms.Panel
  // ------------------------------------------
  type panel (property: Property) as self = 
    inherit System.Windows.Forms.Panel ()
    do
      let rec apply (styles: Style list) (pnl: System.Windows.Forms.Panel) =
        match styles with
          | [] -> pnl
          | style::rest -> 
              match style with
                | _ -> controls.apply style pnl
              apply rest pnl
      self
      |> controls.suspend
      |> apply property.property
      |> controls.add_range property.controls
      |> controls.resume false
      |> ignore



  // ------------------------------------------
  // System.Windows.Forms.Label
  // ------------------------------------------
  type label (property: Property) as self = 
    inherit System.Windows.Forms.Label ()
    do
      let rec apply (styles: Style list) (lbl: System.Windows.Forms.Label) =
        match styles with
          | [] -> lbl
          | style::rest -> 
              match style with
                | _ -> controls.apply style lbl
              apply rest lbl
      self
      |> controls.suspend
      |> apply property.property
      |> controls.add_range property.controls
      |> controls.resume false
      |> ignore



  // ------------------------------------------
  // System.Windows.Forms.LinkLabel
  // ------------------------------------------
  type link (property: Property) as self = 
    inherit System.Windows.Forms.LinkLabel ()
    do
      let rec apply (styles: Style list) (lnk: System.Windows.Forms.LinkLabel) =
        match styles with
          | [] -> lnk
          | style::rest -> 
              match style with
                | _ -> controls.apply style lnk
              apply rest lnk
      self
      |> controls.suspend
      |> apply property.property
      |> controls.add_range property.controls
      |> controls.resume false
      |> ignore



  // ------------------------------------------
  // System.Windows.Forms.CheckBox
  // ------------------------------------------
  type checkbox (property: Property) as self = 
    inherit System.Windows.Forms.CheckBox ()
    do
      let rec apply (styles: Style list) (chk: System.Windows.Forms.CheckBox) =
        match styles with
          | [] -> chk
          | style::rest -> 
              match style with
                | Checked c -> chk.Checked <- c
                | _ -> controls.apply style chk
              apply rest chk
      self
      |> controls.suspend
      |> apply property.property
      |> controls.add_range property.controls
      |> controls.resume false
      |> ignore



  // ------------------------------------------
  // System.Windows.Forms.ComboBox
  // ------------------------------------------
  type combobox (property: Property) as self = 
    inherit System.Windows.Forms.ComboBox ()
    do
      let mutable i : int option = None
      let rec apply (styles: Style list) (cmb: System.Windows.Forms.ComboBox) =
        match styles with
          | [] -> cmb
          | style::rest -> 
              match style with
                | Index v -> i <- Some v
                | _ -> controls.apply style cmb
              apply rest cmb
      self
      |> controls.suspend
      |> apply property.property
      |> controls.add_range property.controls
      |> controls.resume false
      |> ignore

      i |> Option.iter (fun i -> self.SelectedIndex <- i)



  // ------------------------------------------
  // System.Windows.Forms.GroupBox
  // ------------------------------------------
  type groupbox (property: Property) as self = 
    inherit System.Windows.Forms.GroupBox ()
    do
      let rec apply (styles: Style list) (gb: System.Windows.Forms.GroupBox) =
        match styles with
          | [] -> gb
          | style::rest -> 
              match style with
                | _ -> controls.apply style gb
              apply rest gb
      self
      |> controls.suspend
      |> apply property.property
      |> controls.add_range property.controls
      |> controls.resume false
      |> ignore



  // ------------------------------------------
  // System.Windows.Forms.RadioButton
  // ------------------------------------------
  type radio (property: Property) as self = 
    inherit System.Windows.Forms.RadioButton ()
    do
      let rec apply (styles: Style list) (rb: System.Windows.Forms.RadioButton) =
        match styles with
          | [] -> rb
          | style::rest -> 
              match style with
                | Checked c -> rb.Checked <- c
                | _ -> controls.apply style rb
              apply rest rb
      self
      |> controls.suspend
      |> apply property.property
      |> controls.add_range property.controls
      |> controls.resume false
      |> ignore



  // ------------------------------------------
  // System.Windows.Forms.ToolStripMenuItem
  // ------------------------------------------
  type menuitem (property: Property) = 
    inherit System.Windows.Forms.Control ()
    let self = new System.Windows.Forms.ToolStripMenuItem()
    do
      let rec apply (styles: Style list) (menuitem: System.Windows.Forms.ToolStripMenuItem) =
        match styles with
          | [] -> menuitem
          | style::rest -> 
              match style with
                | Size s -> menuitem.Size <- s
                | Checked c -> menuitem.Checked <- c
                | Index i -> menuitem.MergeIndex <- i
                | Anchor a -> menuitem.Anchor <- a
                | Dock d -> menuitem.Dock <- d
                | AutoSize s -> menuitem.AutoSize <- s
                | Text t -> menuitem.Text <- t
                | Name n -> menuitem.Name <- n
                | BackgroundImage img -> menuitem.BackgroundImage <- img
                | Image (img, align) -> menuitem.Image <- img; menuitem.ImageAlign <- align
                | Command cmd -> menuitem.Click.Add(cmd)
                | _ -> raise (exn $"Not supported styles: {style} ({menuitem})")
              apply rest menuitem

      let rec add_range (controls: Control list) (menuitem: System.Windows.Forms.ToolStripMenuItem) =
        match controls with
            | [] -> menuitem
            | ctrl::rest ->
                match ctrl with
                  | :? menuitem as item -> menuitem.DropDownItems.Add(item.raw :> ToolStripItem) |> ignore
                  | _ -> raise (exn $"This control is not supported: {ctrl}")
                add_range rest menuitem

      self
      |> apply property.property
      |> add_range property.controls
      |> ignore

    member __.raw with get() = self



  // ------------------------------------------
  // System.Windows.Forms.MenuStrip
  // ------------------------------------------
  type menustrip (property: Property) as self = 
    inherit System.Windows.Forms.MenuStrip ()
    do
      let rec apply (styles: Style list) (ms: System.Windows.Forms.MenuStrip) =
        match styles with
          | [] -> ms
          | style::rest -> 
              match style with
                | _ -> controls.apply style ms
              apply rest ms

      let rec add_range (controls: Control list) (menu: System.Windows.Forms.MenuStrip) =
        match controls with
          | [] -> menu
          | ctrl::rest ->
              match ctrl with
                | :? menuitem as item -> menu.Items.Add(item.raw :> ToolStripItem) |> ignore
                | _ -> menu.Controls.Add ctrl
              add_range rest menu

      self
      |> controls.suspend
      |> apply property.property
      |> add_range property.controls
      |> controls.resume false
      |> ignore