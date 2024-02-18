namespace Funtom.winforms

[<AutoOpen>]
module Form =
  let form (properties: Property array) =
    let apply (f: System.Windows.Forms.Form) p =
      match p with
      | Anchor anchor -> f.Anchor <- Anchors.convert anchor
      | Size size -> f.Size <- Size.convert size
      | Text text -> f.Text <- text
    
    let f = new System.Windows.Forms.Form()
    properties |> Array.iter (apply f)
    f

  let show (f: System.Windows.Forms.Form) = f.Show(); f
  let show_dialog (f: System.Windows.Forms.Form) = f.ShowDialog()