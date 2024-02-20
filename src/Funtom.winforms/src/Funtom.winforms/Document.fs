namespace Funtom.winforms

[<RequireQualifiedAccess>]
type evt =
  | click of (System.EventArgs -> unit)

module document =
  let inline get_elem_by_id (id: string) (property: Property) =
    match property with
      | Form form -> form.Controls[id]
      | Control ctrl -> ctrl.Controls[id]
      | _ -> exn $"This property is not supported: {property}" |> raise
    |> Control

  
  let inline get_elem_by_id' (id: string) (ctrl: System.Windows.Forms.Control) =
    ctrl.Controls[id]

  let inline add_event_listner (evt: evt) (property: Property) =
    let apply (c: System.Windows.Forms.Control) =
      match evt with
      | evt.click action -> c.Click.Add(action)

    match property with
      | Form form -> apply form
      | Control ctrl -> apply ctrl
      | _ -> exn $"This property is not supported: {property}" |> raise
    
    property