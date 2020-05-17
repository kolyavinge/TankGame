module WorldView

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Media
open System.Windows.Media.Imaging
open System.Windows.Shapes
open GameLogic

let tileSize = 32

let tilesImage = BitmapImage(Uri("main_tiles.png", UriKind.Relative))

let tile x y = CroppedBitmap(tilesImage, Int32Rect(x * tileSize, y * tileSize, tileSize, tileSize))

// изображения дорог: номер дороги, основное изображение
let roads = [
    ( 1, tile  3  3)   // асфальт
    ( 2, tile  9  3)   // асфальт с тенью
    ( 3, tile  0  5)   // асфальт с травой
    ( 4, tile  1  5)
    ( 5, tile  2  5)
    ( 6, tile  3  5)
    ( 7, tile  4  5)
    ( 8, tile  5  5)
    ( 9, tile  6  5)
    (10, tile  7  5)

    (11, tile  4  3)   // песок
    (12, tile 10  3)   // песок с тенью
    (13, tile  8  5)   // песок с травой
    (14, tile  9  5)
    (15, tile 10  5)
    (16, tile 11  5)
    (17, tile  9  4)
    (18, tile  8  4)
    (19, tile 10  4)
    (20, tile 11  4)

    (21, tile  6  3)   // вода
    (22, tile  8  3)   // вода с тенью
    (23, tile  4  6)   // вода с травой
    (24, tile  5  6)
    (25, tile  6  6)
    (26, tile  7  6)
    (27, tile  3  6)
    (28, tile  2  6)
    (29, tile  0  6)
    (30, tile  1  6)

    (40, tile  2  4)   // мост вертикаль
    (41, tile  3  4)   // мост вертикаль песок низ
    (42, tile  4  4)   // мост вертикаль песок верх
    (43, tile  8  6)   // мост горизонт
    (44, tile  9  6)   // мост горизонт трава лево
    (45, tile 10  6)   // мост горизонт трава право
]

let findRoad subtype =
    roads
    |> List.find (fun (s, _) -> s = subtype)
    |> (fun (_, tile) -> tile)

// изображения стен: номер стены, основное изображение, слабо поврежденное, средне, сильно
let walls = [
    ( 1, [tile 10  1; tile  9  7; tile 10  7; tile 11  7])   // белый бетон
    ( 2, [tile  2  3; tile  9  8; tile 10  8; tile 11  8])   // зеленый бетон

    (10, [tile  3  1; tile  3  7; tile  4  7; tile  5  7])   // зеленый ящик вертикаль
    (11, [tile  3  2; tile  6  9; tile  7  9; tile  8  9])   // коричневый ящик вертикаль
    (12, [tile  8  2; tile  3 10; tile  4 10; tile  5 10])   // белый ящик вертикаль
    (13, [tile  0  3; tile 12  6; tile 12  7; tile 12  8])   // белый ящик горизонт
    (14, [tile  9  2; tile  6 10; tile  7 10; tile  8 10])   // синий ящик вертикаль

    (20, [tile  4  1; tile  0  7; tile  1  7; tile  2  7])   // зеленый контейнер горизонт
    (21, [tile  6  1; tile  0 11; tile  1 11; tile  2 11])   // зеленый контейнер вертикаль
    (22, [tile  4  2; tile  3  8; tile  4  8; tile  5  8])   // синий контейнер горизонт
    (23, [tile  5  1; tile  6  7; tile  7  7; tile  8  7])   // синий контейнер вертикаль
    (24, [tile  9  1; tile  0 10; tile  1 10; tile  2 10])   // белый контейнер горизонт
    (25, [tile  7  2; tile  9  9; tile 10  9; tile 11  9])   // белый контейнер вертикаль
    (26, [tile 11  1; tile  0  8; tile  1  8; tile  2  8])   // коричневый контейнер горизонт
    (27, [tile 10  2; tile  9 10; tile 10 10; tile 11 10])   // коричневый контейнер вертикаль
    (28, [tile  1  4; tile  3  9; tile  4  9; tile  5  9])   // красный контейнер горизонт
    (29, [tile  0  4; tile  0  9; tile  1  9; tile  2  9])   // красный контейнер вертикаль
]

let findWallByState (states: CroppedBitmap list) state =
    match state with
    |Normal -> states.[0]
    |DamageLow -> states.[1]
    |DamageMedium -> states.[2]
    |DamageHight -> states.[3]

let findWall (subtype, state) =
    walls
    |> List.find (fun (s, _) -> s = subtype)
    |> (fun (_, states) -> findWallByState states state)

let itemToTile worldItem =
    match worldItem.kind with
    |Road subtype -> findRoad subtype
    |Wall (subtype, state) -> findWall (subtype, state)

let itemToImage (worldItem: WorldItem) =
    let image = Image(Width = float tileSize,
                      Height = float tileSize,
                      Margin = Thickness(float(worldItem.pos.x * tileSize), float(worldItem.pos.y * tileSize), 0.0, 0.0),
                      Source = itemToTile worldItem)
    RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.NearestNeighbor) // default for all !!
    image

let renderGrid (world: World) (layout: Panel) =
    for row in 0 .. world.size.height - 1 do
        for col in 0 .. world.size.width - 1 do
            let rect = Rectangle(Width = double tileSize,
                                 Height = double tileSize,
                                 Margin = Thickness(double(col*tileSize), double(row*tileSize), 0.0, 0.0),
                                 Stroke = Brushes.Black,
                                 StrokeThickness = 0.25)
            layout.Children.Add(rect) |> ignore

let renderItems (world: World) (layout: Panel) =
    layout.Background <- ImageBrush(ImageSource = findRoad world.defaultRoad,
                                    TileMode = TileMode.FlipXY,
                                    Viewport = Rect(0.0, 0.0, float tileSize, float tileSize),
                                    ViewportUnits = BrushMappingMode.Absolute)
    world.items |> List.iter (fun worldItem -> layout.Children.Add(itemToImage worldItem) |> ignore)

let render (world: World) (window: Window) =
    let layout = Canvas(Width = float(tileSize * world.size.width), Height = float(tileSize * world.size.height))
    renderItems world layout
    renderGrid world layout
    window.Content <- layout
