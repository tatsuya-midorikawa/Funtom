namespace Funtom.winforms

open Funtom.winforms.controls

module forms =
  let private apply (form: System.Windows.Forms.Form) p =
    //let mutable menu = None
    //match p with
    //  | MenuStrip m -> menu <- Some m
    //  | _ -> internals.apply form p

    //match menu with
    //  | Some m -> 
    //    form.Controls.Add m
    //    form.MainMenuStrip <- m
    //  | None -> ()

    match p with
      | MenuStrip menu ->
        form.Controls.Add menu
        form.MainMenuStrip <- menu
      | _ -> internals.apply form p

  let form (properties: Property list) =    
    let f = new System.Windows.Forms.Form()
    f.SuspendLayout()
    f.AutoScaleDimensions <- System.Drawing.SizeF(7f, 15f)
    f.AutoScaleMode <- System.Windows.Forms.AutoScaleMode.Font
    properties |> suspend_layouts
    properties |> List.iter (apply f)
    properties |> resume_layouts false
    f.ResumeLayout(false)
    Form f

  let show (property: Property) =
    match property with Form f -> f.Show(); property | _ -> exn $"This property is not supported: {property}" |> raise
    
  let show_dialog (property: Property) =
    match property with Form f -> f.ShowDialog() | _ -> exn $"This property is not supported: {property}" |> raise