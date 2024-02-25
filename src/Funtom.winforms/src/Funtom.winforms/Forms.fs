namespace Funtom.winforms

open Funtom.winforms.controls

module forms =
  let form (properties: Property list) =    
    let f = new System.Windows.Forms.Form()
    properties |> List.iter (internals.apply f)
    Form f

  let show (property: Property) =
    match property with Form f -> f.Show(); property | _ -> exn $"This property is not supported: {property}" |> raise
    
  let show_dialog (property: Property) =
    match property with Form f -> f.ShowDialog() | _ -> exn $"This property is not supported: {property}" |> raise