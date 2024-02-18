namespace Funtom.winforms

[<System.Flags>]
type Anchors =
  | none   = 0
  | top    = (1 <<< 0)
  | bottom = (1 <<< 1)
  | left   = (1 <<< 2)
  | right  = (1 <<< 3)

[<Struct; System.Runtime.CompilerServices.IsReadOnly>]
type Size = { width: int; height: int; }

type Property =
  | Anchor of Anchors
  | Size of Size
  | Text of string

[<AutoOpen>]
module Property =
 let inline anchor (anchors: Anchors) = Anchor anchors
 let inline size (size: Size) = Size size
 let inline text (text: string) = Text text

module private Anchors =
  let convert (anchors: Anchors) = anchors |> (int >> enum<System.Windows.Forms.AnchorStyles>)

module private Size =
  let convert size =
    System.Drawing.Size(width = size.width, height= size.height)
