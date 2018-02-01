module DomainTransitions 
open System
open Domain
open System.Text.RegularExpressions

//https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/active-patterns 

let letters = Array.map (string) [|'A'..'J'|] 

let (|CharAsIndex|) (str: string) = 
    letters |> Array.findIndex (fun letter -> letter = str.ToUpperInvariant())

let (|Integer|) (str: string) =
    Int32.Parse str

let (|ParseRegex|_|) regex str =
   let m = Regex(regex).Match(str)
   if m.Success
   then Some (List.tail [ for x in m.Groups -> x.Value ])
   else None

let isShipSunk(grid: NauticalSquare[][]) (ship: Ship) : bool =
    List.forall (fun (y, x) -> grid.[y].[x].Hit) ship.Positions

let getShip shipId ships =
    let shipHasId shipId (ship:Ship) = ship.Id = shipId
    ships |> List.find (shipHasId shipId)

let processFire (input: string) (state: GameState) = 
    match input with 
    | ParseRegex "([a-jA-J])([0-9])$" [CharAsIndex y; Integer x] -> 
        let target = {state.Grid.[y].[x] with Hit = true}

        let updatedGrid = state.Grid
        updatedGrid.[y].[x] <- target
        
        let shipSunk, updatedWrecks, updatedShips = 
            match target.Contents with
            | ShipSection shipId -> 
                let ship = getShip shipId state.Ships 
                let isSunk = isShipSunk updatedGrid ship
                if isSunk then 
                    let ships = state.Ships |> List.filter (fun x -> x <> ship)
                    Some ship, ship::state.Wrecks, ships
                else 
                    None, state.Wrecks, state.Ships
            | Water -> None, state.Wrecks, state.Ships

        {state with 
            Grid = updatedGrid; 
            LastShipSunk = shipSunk
            Ships = updatedShips
            Wrecks = updatedWrecks}
    | _ -> state

let updateGameState (input: string) (state: GameState) = 
    processFire input state