namespace Funtom.winforms

module layouts =

  module private FlowLayoutPanel =
    let apply (panel: System.Windows.Forms.FlowLayoutPanel) p =
      match p with
      | Direction direction -> panel.FlowDirection <- Direction.convert &direction
      | _ -> Ctrl.apply panel p

  let flow (properties: Property array) =
    let panel = new System.Windows.Forms.FlowLayoutPanel()
    panel.Dock <- System.Windows.Forms.DockStyle.Fill
    properties |> Array.iter (FlowLayoutPanel.apply panel)
    Control panel