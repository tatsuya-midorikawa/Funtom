namespace Funtom.winforms.exp

type Style =
  | Size of System.Drawing.Size

[<Struct>]
type Property = { 
  styles: Style list
  controls: System.Windows.Forms.Control list
}
with member __.empty = { styles = []; controls = []}

module forms =
  [<Struct; System.Runtime.CompilerServices.IsReadOnly>]
  type form (self: System.Windows.Forms.Form) =
    interface System.IDisposable with member __.Dispose() = self.Dispose()

    new (property: Property) = 
      let f = new System.Windows.Forms.Form()
      new form (f)
    new (styles: Style list) = new form { styles= styles; controls= [] }
    new (controls: System.Windows.Forms.Control list) = new form { styles= []; controls= controls }


