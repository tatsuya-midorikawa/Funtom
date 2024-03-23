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
