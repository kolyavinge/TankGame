module GameLogic

type Point = { x: int; y: int }

type Size = { width: int; height: int }

type WallState = |Normal|DamageLow|DamageMedium|DamageHight

type Subtype = int

type WorldItemKind =
    |Road of Subtype
    |Wall of Subtype * WallState

type WorldItem = {
    pos: Point
    kind: WorldItemKind
}

type Weapon = {
    damage: int
}

type Tank = {
    coord: Point
    health: int
    weapon: Weapon
}

type World = {
    size: Size
    defaultRoad: Subtype
    items: WorldItem list
    player: Tank
    enemies: Tank list
}

