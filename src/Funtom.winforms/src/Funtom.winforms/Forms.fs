namespace Funtom.winforms

open Funtom.winforms.controls

module forms =

  let form (properties: Property list) =
    let f = new System.Windows.Forms.Form()
    f.SuspendLayout()
    f.AutoScaleDimensions <- System.Drawing.SizeF(7f, 15f)
    f.AutoScaleMode <- System.Windows.Forms.AutoScaleMode.Font
    
    
    let mutable menu = None
    let mutable (panels) = ResizeArray<System.Windows.Forms.Panel>()
    let apply (form: System.Windows.Forms.Form) p =
      match p with
        | MenuStrip m -> menu <- Some m
        | Control ctrl ->
          match ctrl with
            | :? System.Windows.Forms.Panel as p -> panels.Add p
            | _ -> internals.apply form p
        | _ -> internals.apply form p

    properties |> Property.suspend_layouts
    properties |> List.iter (apply f)
    
    // Panel -> MenuStrip でないと正しくレイアウトされないため、そこをハンドリングする
    panels |> Seq.iter (fun p -> f.Controls.Add p)
    menu |> Option.iter (fun m -> f.Controls.Add m; f.MainMenuStrip <- m)

    
    properties |> Property.resume_layouts false
    f.ResumeLayout(false)
    Form f

  let show (property: Property) =
    match property with Form f -> f.Show(); property | _ -> exn $"This property is not supported: {property}" |> raise
    
  let show_dialog (property: Property) =
    match property with Form f -> f.ShowDialog() | _ -> exn $"This property is not supported: {property}" |> raise