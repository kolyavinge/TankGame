open System
open System.Windows
open System.Windows.Controls
open System.Windows.Media
open System.Windows.Media.Imaging

[<EntryPoint; STAThread>]
let main argv =
    let world = WorldLoader.loadWorld
    let mainWindow = Window(WindowStartupLocation = WindowStartupLocation.CenterScreen)
    WorldView.render world mainWindow
    mainWindow.ShowDialog() |> ignore
    0