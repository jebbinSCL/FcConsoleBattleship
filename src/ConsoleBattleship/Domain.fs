module Domain

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

let initialState () =
    let ships = 
        [ 
            //{ Positions = [(1, 1); (1, 2) ; (1, 3) ]; Id= "HMS Tom" };
            //{ Positions = [(3, 2); (3, 3) ; (3, 4) ]; Id= "HMS Sinking-Hewitt" }
            { Positions = [(0, 0) ]; Id= "Poor man's Shek" }
        ] 
    let grid = Array.init 10 (fun _ -> Array.init 10 (fun x -> {Hit = false; Contents = Water}))
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

