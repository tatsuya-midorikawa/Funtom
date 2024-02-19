namespace Funtom.winforms

module forms =
  let form (properties: Property array) =    
    let f = new System.Windows.Forms.Form()
    properties |> Array.iter (Ctrl.apply f)
    f

  let show (f: System.Windows.Forms.Form) = f.Show(); f
  let show_dialog (f: System.Windows.Forms.Form) = f.ShowDialog()