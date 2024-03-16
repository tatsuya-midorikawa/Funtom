namespace Funtom.winforms.exp

open System.Windows.Forms

[<System.Flags>]
type Anchors = none= 0 | top= (1 <<< 0) | bottom= (1 <<< 1) | left= (1 <<< 2) | right= (1 <<< 3)
module private Anchors = let cast (anchors: Anchors) =  anchors |> (int >> enum<AnchorStyles>)

type Style =
  | Size of System.Drawing.Size
  | Location of System.Drawing.Point

[<Struct>]
type Property = { 
  styles: Style list
  controls: Control list
}
with member __.empty = { styles = []; controls = []}

module forms =
  [<Struct; System.Runtime.CompilerServices.IsReadOnly>]
  type form (self: Form) =
    interface System.IDisposable with member __.Dispose() = self.Dispose()

    new (property: Property) = 
      let f = new Form()
      property.styles |> List.iter (function
        | Size s -> f.Size <- s
        | Location l -> f.Location <- l
        )
      new form (f)
    new (styles: Style list) = new form { styles= styles; controls= [] }
    new (controls: Control list) = new form { styles= []; controls= controls }


