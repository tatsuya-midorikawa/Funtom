namespace Funtom.winforms.exp

open System.Windows.Forms

[<System.Flags>]
type Anchor = none= 0 | top= (1 <<< 0) | bottom= (1 <<< 1) | left= (1 <<< 2) | right= (1 <<< 3)
module private Anchors = let cast (anchors: Anchor) =  anchors |> (int >> enum<AnchorStyles>)

type Direction = left_to_right= 0 | top_down= 1 | ritght_to_left= 2 | bottom_up= 3
module private Direction = let cast (direction: Direction) = direction |> (int >> enum<FlowDirection>)

type Dock = none= 0 | top= 1 | bottom= 2 | left= 3 | right= 4 | fill= 5
module private Dock = let cast (dock: Dock) = dock |> (int >> enum<DockStyle>)


type Style =
  | Size of System.Drawing.Size
  | Location of System.Drawing.Point
  | Anchor of Anchor
  | Directon of Direction
  | AutoSize of bool
  | Text of string
  | Name of string
  | Image of System.Drawing.Image
  | Icon of System.Drawing.Icon
  | Checked of bool
  | Index of int

[<AutoOpen>]
module Style =
  let inline size (width, height) = System.Drawing.Size (width, height) |> Style.Size
  let inline location (x, y) = System.Drawing.Point (x, y) |> Style.Location
  let inline anchor anchor = anchor |> Style.Anchor
  let inline direction direction = direction |> Style.Directon
  let inline auto_size auto = auto |> Style.AutoSize
  let inline text text = text |> Style.Text
  let inline name name = name |> Style.Name
  let inline identifier id = id |> Style.Name
  let inline image (img_path: string) = Style.Image (new System.Drawing.Bitmap (img_path))
  let inline bitmap (img_path: string) = Style.Image (new System.Drawing.Bitmap (img_path))
  let inline icon (ico_path: string) = Style.Icon (new System.Drawing.Icon (ico_path))
  let inline marked marked = marked |> Style.Checked
  let inline index id = id |> Style.Index



[<Struct>]
type Property = { 
  styles: Style list
  controls: Control list
}
with member __.empty = { styles = []; controls = []}
