# Funtom.winforms

## Overview

This library makes WinForms development in F# convenient.
It is primarily intended to be invoked with F# script (.fsx), and the purpose is to provide a more lightweight way to start GUI programming.

---
## Usage

This library is used as shown in the following codes:

```fsharp
#I @"C:\Program Files\dotnet\shared\Microsoft.WindowsDesktop.App\8.0.2"
#r "System.Windows.Forms"
#r "nuget: Funtom.winforms, 0.0.3-alpha"

open Funtom.winforms

form [
  style [ size { width= 640<px>; height= 480<px> }; text "Hello F# GUI App!!" ]
]
|> show_dialog
|> ignore
```
