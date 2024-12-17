namespace Funtom.winforms.values

[<AutoOpen>]
module Values =
  [<Literal>]
  let enable = true
  [<Literal>]
  let disable = false
  [<Literal>]
  let allow = true
  [<Literal>]
  let disallow = false

[<Struct>]
type SizeF = { width: float32; height: float32 }
[<RequireQualifiedAccess>]
module SizeF =
  let inline toNative (size: SizeF) = System.Drawing.SizeF(float32 size.width, float32 size.height)


[<Struct>]
type Size = { width: int; height: int }
[<RequireQualifiedAccess>]
module Size =
  let inline toNative (size: Size) = System.Drawing.Size(size.width, size.height)
  

[<Struct>]
type Point = { x: int; y: int }
[<RequireQualifiedAccess>]
module Point =
  let inline toNative (p: Point) = System.Drawing.Point(p.x, p.y)


[<Struct>]
type Position = { top: int; left: int }
[<RequireQualifiedAccess>]
module Position =
  let inline toNative (position: Position) = System.Drawing.Point(position.left, position.top)


[<Struct; RequireQualifiedAccess>]
type AutoScaleMode =
  | none = 0
  | font = 1
  | dpi = 2
  | inheritant = 3
[<RequireQualifiedAccess>]
module AutoScaleMode =
  let inline toNative (mode: AutoScaleMode) = int mode |> enum<System.Windows.Forms.AutoScaleMode>


[<Struct; System.Flags; RequireQualifiedAccess>]
type Anchors =
  | none   = 0
  | top    = (1 <<< 0)
  | bottom = (1 <<< 1)
  | left   = (1 <<< 2)
  | right  = (1 <<< 3)
[<RequireQualifiedAccess>]
module Anchors =
  let inline toNative (anchors: Anchors) = int anchors |> enum<System.Windows.Forms.AnchorStyles>

  
[<Struct; RequireQualifiedAccess>]
type Dock =
  | none = 0
  | top = 1
  | bottom = 2
  | left = 3
  | right = 4
  | fill = 5
[<RequireQualifiedAccess>]
module Dock =
  let inline toNative (dock: Dock) = int dock |> enum<System.Windows.Forms.DockStyle>


[<Struct; RequireQualifiedAccess>]
type Direction =
  | left2right = 0
  | topdown = 1
  | right2left = 2
  | bottomup = 3
[<RequireQualifiedAccess>]
module Direction =
  let inline toNative (direction: Direction) = int direction |> enum<System.Windows.Forms.FlowDirection>
  
[<Struct; RequireQualifiedAccess>]
type ImageLayout =
  | none = 0
  | title = 1
  | center = 2
  | stretch = 3
  | zoom = 4
[<RequireQualifiedAccess>]
module ImageLayout =
  let inline toNative (layout: ImageLayout) = int layout |> enum<System.Windows.Forms.ImageLayout>

  
[<Struct; RequireQualifiedAccess>]
type ScrollBars =
  | none = 0
  | horizontal = 1
  | vertical = 2
  | both = 3
[<RequireQualifiedAccess>]
module ScrollBars =
  let inline toNative (scrollbars: ScrollBars) = int scrollbars |> enum<System.Windows.Forms.ScrollBars>


type Color = System.Drawing.Color
type Icon = System.Drawing.Icon
type Image = System.Drawing.Image
type MenuStripItem = System.Windows.Forms.ToolStripMenuItem

(* System.Windows.Forms.Control *)
type Button = System.Windows.Forms.Button
type CheckBox = System.Windows.Forms.CheckBox
type ComboBox = System.Windows.Forms.ComboBox
type Control = System.Windows.Forms.Control
type GroupBox = System.Windows.Forms.GroupBox
type FlowLayoutPanel = System.Windows.Forms.FlowLayoutPanel
type Form = System.Windows.Forms.Form
type Label = System.Windows.Forms.Label
type ListBox = System.Windows.Forms.ListBox
type ListView = System.Windows.Forms.ListView
type MenuStrip = System.Windows.Forms.MenuStrip
type NumericUpDown = System.Windows.Forms.NumericUpDown
type Panel = System.Windows.Forms.Panel
type PictureBox = System.Windows.Forms.PictureBox
type RadioButton = System.Windows.Forms.RadioButton
type TextBox = System.Windows.Forms.TextBox
