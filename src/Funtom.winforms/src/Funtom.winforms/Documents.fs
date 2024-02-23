namespace Funtom.winforms

[<RequireQualifiedAccess>]
type evt =
  | click of (System.EventArgs -> unit)

module document =

  let rec private get_elem_by_id' (id: string) (ctrl: System.Windows.Forms.Control.ControlCollection) =
    match ctrl[id] with
      | null ->
        let ctrls = ctrl |> (Seq.cast >> Seq.toList)
        let rec dig (ctrls: System.Windows.Forms.Control list) =
          match ctrls with
          | [] -> None
          | head :: tail ->
              let (ret: Property option) = get_elem_by_id' id head.Controls
              if ret.IsSome then ret else dig tail
        dig ctrls
      | ctrl -> Some (Control ctrl)
      
  let rec get_elem_by_id (id: string) (property: Property) =
    match property with
      | Form form -> get_elem_by_id' id form.Controls
      | Control ctrl -> get_elem_by_id' id ctrl.Controls
      | _ -> exn $"This property is not supported: {property}" |> raise

  let inline add_event_listener (evt: evt) (property: Property option) =
    let apply (c: System.Windows.Forms.Control) =
      match evt with
      | evt.click action -> c.Click.Add(action)

    match property with
      | Some (Control ctrl) -> apply ctrl
      | Some (Form form) -> apply form
      | None -> ()
      | _ -> exn $"This property is not supported: {property}" |> raise
    
    property