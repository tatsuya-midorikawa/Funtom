namespace Funtom.winforms

open Funtom.winforms.controls

module forms =
  let form (properties: Property list) =    
    let f = new System.Windows.Forms.Form()
    f.SuspendLayout()
    properties |> suspend_layouts
    properties |> List.iter (internals.apply f)
    properties |> resume_layouts false
    f.ResumeLayout(false)
    Form f

  let show (property: Property) =
    match property with Form f -> f.Show(); property | _ -> exn $"This property is not supported: {property}" |> raise
    
  let show_dialog (property: Property) =
    match property with Form f -> f.ShowDialog() | _ -> exn $"This property is not supported: {property}" |> raise