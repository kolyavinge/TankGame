module WorldLoader

open GameLogic

let t = 29

let loadWorld: World = {
    size = { width = 30; height = 20 }
    items = [
        for x in 0..29 do yield { pos = { x = x; y = 10 }; kind = Road(21) }
        for x in 0..29 do yield { pos = { x = x; y = 11 }; kind = Road(21) }

        yield { pos = { x = 5; y =  9 }; kind = Road(40) }
        yield { pos = { x = 6; y =  9 }; kind = Road(40) }
        yield { pos = { x = 5; y = 10 }; kind = Road(40) }
        yield { pos = { x = 5; y = 11 }; kind = Road(40) }
        yield { pos = { x = 6; y = 10 }; kind = Road(40) }
        yield { pos = { x = 6; y = 11 }; kind = Road(40) }
        yield { pos = { x = 5; y = 12 }; kind = Road(40) }
        yield { pos = { x = 6; y = 12 }; kind = Road(40) }

        yield { pos = { x = 15; y =  9 }; kind = Road(40) }
        yield { pos = { x = 16; y =  9 }; kind = Road(40) }
        yield { pos = { x = 15; y = 10 }; kind = Road(40) }
        yield { pos = { x = 15; y = 11 }; kind = Road(40) }
        yield { pos = { x = 16; y = 10 }; kind = Road(40) }
        yield { pos = { x = 16; y = 11 }; kind = Road(40) }
        yield { pos = { x = 15; y = 12 }; kind = Road(40) }
        yield { pos = { x = 16; y = 12 }; kind = Road(40) }

        for x in 0..4 do yield { pos = { x = x; y = 12 }; kind = Road(17) }
        for x in 7..14 do yield { pos = { x = x; y = 12 }; kind = Road(17) }
        for x in 17..29 do yield { pos = { x = x; y = 12 }; kind = Road(17) }

        for x in 0..4 do yield { pos = { x = x; y = 9 }; kind = Road(18) }
        for x in 7..14 do yield { pos = { x = x; y = 9 }; kind = Road(18) }
        for x in 17..29 do yield { pos = { x = x; y = 9 }; kind = Road(18) }
    ]
    defaultRoad = 11
    player = {
        coord = { x = 5; y = 0 }
        health = 1
        weapon = { damage = 1 }
    }
    enemies = [
    ]
}
