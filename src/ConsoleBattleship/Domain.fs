module Domain

open System
open System.ComponentModel

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

let gridSize = 10
let rand = Random()
let nextInt ()  =  rand.Next(0, gridSize)

let flipCoin heads tails =
    if rand.NextDouble() <= 0.5 then
        heads
    else 
        tails

let nextTranslator () = 
    let translateByX translateBy (x,y) = (x + translateBy, y)
    let translateByY translateBy (x,y) = (x, y + translateBy)
    flipCoin translateByX translateByY
    
let generateRandomPosition (nextInt) = 
    (nextInt(),nextInt())

let generateRandomShip (nextInt) (length: int) (name: string)  = 
    let startPos = generateRandomPosition nextInt 
    let translator = nextTranslator ()
    let positions = List.init length (fun index -> translator (index-1) startPos)
    { Positions = positions; Id = name}

let clashes ships ship =
    let shipPositions = ships |> List.collect(fun ship -> ship.Positions) |> Set.ofList
    Set.intersect (shipPositions) (Set.ofList ship.Positions)  |> Seq.isEmpty |> not

let positionOutsideGrid (X, Y) =
    X < 0 || X >= gridSize || Y < 0 || Y >= gridSize

let shipOutsideGrid ship =
    ship.Positions |> List.exists positionOutsideGrid   

let shipIsInvalid ships ship = 
    clashes ships ship || shipOutsideGrid ship


let rec generateShips currentShips lengths = 
    match lengths with
    | [] -> currentShips
    | nextLength::tail -> 
        let ship = generateRandomShip nextInt nextLength "sdfoisdjf"
        if shipIsInvalid currentShips ship then
            generateShips currentShips lengths
        else
            generateShips (ship::currentShips) tail
        
let initialState () =

    let generateShipInGrid = generateRandomShip nextInt

    let ships = generateShips [] <| List.init 5 (fun _ -> rand.Next(1, 5))
        //[ 
        //    //{ Positions = [(1, 1); (1, 2) ; (1, 3) ]; Id= "HMS Tom" };
        //    //{ Positions = [(3, 2); (3, 3) ; (3, 4) ]; Id= "HMS Sinking-Hewitt" }
        //    //{ Positions = [generateRandomPosition(gridSize) ]; Id= "Poor man's Shek" }
        //    generateShipInGrid 1 "Random1"
        //    generateShipInGrid 1 "Random2"
        //    generateShipInGrid 1 "Random3"
        //]         

    let grid = Array.init gridSize (fun _ -> Array.init gridSize (fun x -> {Hit = false; Contents = Water}))
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

