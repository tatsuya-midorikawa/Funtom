# Funtom.winforms

## Overview

This library makes WinForms development in F# convenient.
It is primarily intended to be invoked with F# script (.fsx), and the purpose is to provide a more lightweight way to start GUI programming.

---
## Usage

1. Install the dotnet template for the F# Script WinForms project.

```powershell
dotnet new install Funtom.winforms.lit.template
```

2. Create a project and initialize.

```powershell
dotnet new winforms.lit -o C:\pj\MyWinformApp

cd C:\pj\MyWinformApp

dotnet fsi lib/init.fsx
```

3. Run the following script in PowerShell.

```powershell
dotnet fsi ./script.fsx
```

4. Edit script.fsx and have fun!
