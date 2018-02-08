module Domain

open System

type Ship = {
    Positions: (int * int) list
    Id: string
}

type SquareOccupant = 
    | Water 
    | ShipSection of Id : string


type NauticalSquare = {
    Hit : bool
    Contents : SquareOccupant
}

type GameState = {
    Grid : NauticalSquare [][] // Jagged array (Array of arrays)
    Ships: Ship list 
    Wrecks: Ship list
    LastShipSunk : Ship option
}

let generateRandomPosition (nextInt) = 
    (nextInt(),nextInt())

let generateRandomShip (nextInt) (length: int) (name: string)  = 
    let startPos = generateRandomPosition nextInt 
    { Positions = [startPos]; Id = name}
        
let initialState () =
    let gridSize = 10
    let rand = Random()
    let nextInt ()  =  rand.Next(0, gridSize)

    let generateShipInGrid = generateRandomShip nextInt

    let ships = 
        [ 
            //{ Positions = [(1, 1); (1, 2) ; (1, 3) ]; Id= "HMS Tom" };
            //{ Positions = [(3, 2); (3, 3) ; (3, 4) ]; Id= "HMS Sinking-Hewitt" }
            //{ Positions = [generateRandomPosition(gridSize) ]; Id= "Poor man's Shek" }
            generateShipInGrid 1 "Random1"
            generateShipInGrid 1 "Random2"
            generateShipInGrid 1 "Random3"
        ] 
    let grid = Array.init gridSize (fun _ -> Array.init gridSize (fun x -> {Hit = false; Contents = Water}))
    //TODO make functional then update domainTransitions
    // Talk about function first programming and functional - imperative spectrum 
    (*
        POssible functional way of doing this: 
        ships -> list of position and id (Or create map)
        then init grid, and if position list contains the current position, use the corresponding id
    *)
    //Reminder: Below adds the ship sections to the grid to complete the initial state. 
    let placeShip (ship: Ship) = 
        let rec recer positions =
            match positions with
            | [] -> ()
            | (y : int, x : int)::rest -> 
                grid.[y].[x] <- {Hit= false; Contents = ShipSection(Id = ship.Id )}
                recer rest
        recer ship.Positions

    let shipIterator = List.iter placeShip
    shipIterator ships

    List.iter placeShip ships

    {
        Grid = grid
        Ships = ships
        LastShipSunk = None
        Wrecks = []
    }

