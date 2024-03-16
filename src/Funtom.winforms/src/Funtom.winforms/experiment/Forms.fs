namespace Funtom.winforms.exp

open System.Windows.Forms
open System.Runtime.CompilerServices

module controls =

  let apply (style: Style) (ctrl: 'T when 'T :> Control) = 
    match style with
      | Size s -> ctrl.Size <- s
      | Location l -> ctrl.Location <- l
      | Anchor a -> ctrl.Anchor <- Anchors.cast a
      | AutoSize s -> ctrl.AutoSize <- s
      | Text t -> ctrl.Text <- t
      | Name n -> ctrl.Name <- n
      | Image i -> ctrl.BackgroundImage <- i
      | _ -> raise (exn $"Not supported styles: {style} ({ctrl})")

  let inline add (ctrls: Control list) (ctrl: 'T when 'T :> Control) =
    ctrl.Controls.AddRange(ctrls |> List.toArray)
    ctrl

  let inline suspend (ctrl: 'T when 'T :> Control) = ctrl.SuspendLayout(); ctrl
  let inline resume (perform_layout: bool) (ctrl: 'T when 'T :> Control) = ctrl.ResumeLayout(perform_layout); ctrl

module forms =
  // ------------------------------------------
  // System.Windows.Forms.Form
  // ------------------------------------------
  [<Struct; IsReadOnly;>]
  type form (self: System.Windows.Forms.Form) =
    interface System.IDisposable with member __.Dispose() = self.Dispose()

    new (property: Property) = 
      let rec apply (styles: Style list) (frm: System.Windows.Forms.Form) =
        match styles with
          | [] -> frm
          | style::rest -> 
              match style with
                | Icon ico -> frm.Icon <- ico
                | _ -> controls.apply style frm
              apply rest frm

      let frm =
        new System.Windows.Forms.Form()
        |> controls.suspend
        |> apply property.styles
        |> controls.add property.controls
        |> controls.resume false
      new form(frm)

    new (styles: Style list) = new form { styles= styles; controls= [] }
    new (controls: Control list) = new form { styles= []; controls= controls }

    member __.show () = self.Show()
    member __.show_dialog () = self.ShowDialog()
    member __.hide () = self.Hide()
    member __.close () = self.Close()
    member __.add (ctrl: Control) = self.Controls.Add(ctrl)
    member __.add (ctrls: Control list) = self.Controls.AddRange(ctrls |> List.toArray)
    member __.remove (ctrl: Control) = self.Controls.Remove(ctrl)
    member __.suspend () = self.SuspendLayout()
    member __.resume (perform_layout: bool) = self.ResumeLayout(perform_layout)
    member __.controls with get () = self.Controls
    member __.children () = self.Controls |> Seq.cast<Control> |> List.ofSeq
    

  // ------------------------------------------
  // System.Windows.Forms.Button
  // ------------------------------------------
  type button (self: System.Windows.Forms.Button) =
    interface System.IDisposable with member __.Dispose() = self.Dispose()
    
    new (property: Property) = 
      let rec apply (styles: Style list) (btn: System.Windows.Forms.Button) =
        match styles with
          | [] -> btn
          | style::rest -> 
              match style with
                | _ -> controls.apply style btn
              apply rest btn
      let btn =
        new System.Windows.Forms.Button()
        |> controls.suspend
        |> apply property.styles
        |> controls.add property.controls
        |> controls.resume false
      new button(btn)

    new (styles: Style list) = new button { styles= styles; controls= [] }
    new (controls: Control list) = new button { styles= []; controls= controls }

