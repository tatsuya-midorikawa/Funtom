namespace Funtom.winforms

open System.Linq

module document =

  let rec private get_elem_by_id'' (id: string) (items:System.Windows.Forms.ToolStripItemCollection) =
    match items[id] with
    | null -> 
      let items = items.Cast<System.Windows.Forms.ToolStripMenuItem>() |> (Seq.cast >> Seq.toList)
      let rec dig (items: System.Windows.Forms.ToolStripMenuItem list) =
        match items with
        | [] -> None
        | head :: tail ->
            let (ret: Property option) = get_elem_by_id'' id head.DropDownItems
            if ret.IsSome then ret else dig tail
      dig items
    | item -> Some (MenuStripItem (item :?> System.Windows.Forms.ToolStripMenuItem))

  let rec private get_elem_by_id' (id: string) (ctrl: System.Windows.Forms.Control.ControlCollection) =
    match ctrl[id] with
      | null ->
        let ctrls = ctrl |> (Seq.cast >> Seq.toList)
        let rec dig (ctrls: System.Windows.Forms.Control list) =
          match ctrls with
          | [] -> None
          | head :: tail ->
              match head with
                | :? System.Windows.Forms.MenuStrip as menu ->
                    match get_elem_by_id'' id menu.Items with Some p -> Some p | _ -> dig tail
                | _ ->
                    let (ret: Property option) = get_elem_by_id' id head.Controls
                    if ret.IsSome then ret else dig tail
        dig ctrls
      | ctrl -> Some (Control ctrl)
      
  let rec get_elem_by_id (id: string) (property: Property) =
    match property with
      | Form form -> get_elem_by_id' id form.Controls
      | Control ctrl -> get_elem_by_id' id ctrl.Controls
      | _ -> exn $"This property is not supported: {property}" |> raise
    |> (fun p -> if p.IsSome then p.Value else exn $"Element with id '{id}' not found." |> raise)

  let inline add_event_listener (evt: evt) (property: Property) =
    let apply (c: System.Windows.Forms.Control) =
      match evt with
      | evt.click action -> c.Click.Add(action)

    let apply' (menu: System.Windows.Forms.ToolStripMenuItem) =
      match evt with
      | evt.click action -> menu.Click.Add(action)

    match property with
      | Control ctrl -> apply ctrl
      | Form form -> apply form
      | MenuStrip menu -> apply menu
      | MenuStripItem item -> apply' item
      | _ -> exn $"This property is not supported: {property}" |> raise
    
    property