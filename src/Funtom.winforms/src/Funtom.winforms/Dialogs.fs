namespace Funtom.winforms

module dialogs =
  (* ----------------------------------------
   * FolderBrowserDialog
   * ---------------------------------------- *)
  type dir_browser () =
    let raw' = new System.Windows.Forms.FolderBrowserDialog()

    interface System.IDisposable with
      member __.Dispose() = raw'.Dispose()

    member __.show (?owner) = match owner with Some v -> raw'.ShowDialog(v) | None -> raw'.ShowDialog()
    member __.reset () = raw'.Reset()

    member __.raw with get() = raw'
    member __.selected with get() = raw'.SelectedPath and set v = raw'.SelectedPath <- v
    member __.description with get() = raw'.Description and set v = raw'.Description <- v
    member __.show_new_dir with get() = raw'.ShowNewFolderButton and set v = raw'.ShowNewFolderButton <- v
    member __.root_dir with get() = raw'.RootFolder and set v = raw'.RootFolder <- v
    member __.init_dir with get() = raw'.InitialDirectory and set v = raw'.InitialDirectory <- v
    member __.show_hiddens with get() = raw'.ShowHiddenFiles and set v = raw'.ShowHiddenFiles <- v
    


  (* ----------------------------------------
   * OpenFileDialog
   * ---------------------------------------- *)
  type file_dialog () =
    let raw' = new System.Windows.Forms.OpenFileDialog()

    interface System.IDisposable with
      member __.Dispose() = raw'.Dispose()

    interface System.Collections.Generic.IEnumerable<string> with
      member __.GetEnumerator() = (raw'.FileNames :> System.Collections.Generic.IEnumerable<string>).GetEnumerator()

    interface System.Collections.IEnumerable with
      member __.GetEnumerator() = raw'.FileNames.GetEnumerator()

    member __.show (?owner) = match owner with Some o -> raw'.ShowDialog o | None -> raw'.ShowDialog ()
    member __.open_file () = new System.IO.StreamReader(raw'.OpenFile ())
    member __.reset () = raw'.Reset()

    member __.raw with get() = raw'
    member __.filter with get () = raw'.Filter and set v = raw'.Filter <- v
    member __.index with get () = raw'.FilterIndex and set v = raw'.FilterIndex <- v
    member __.init_dir with get () = raw'.InitialDirectory and set v = raw'.InitialDirectory <- v
    member __.restore_dir with get () = raw'.RestoreDirectory and set v = raw'.RestoreDirectory <- v
    member __.multi with get () = raw'.Multiselect and set v = raw'.Multiselect <- v
    member __.title with get () = raw'.Title and set v = raw'.Title <- v
    member __.filename with get () = raw'.FileName and set v = raw'.FileName <- v
    member __.filenames with get () = raw'.FileNames
    member __.readonly with get () = raw'.ReadOnlyChecked and set v = raw'.ReadOnlyChecked <- v
    member __.select_readonly with get () = raw'.SelectReadOnly and set v = raw'.SelectReadOnly <- v
    member __.fileok(e: System.ComponentModel.CancelEventArgs -> unit) = raw'.FileOk.Add e

    member __.Item with get (i: int) = raw'.FileNames.[i]