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

let findUnsunkShip(grid: NauticalSquare[][]) (shipId: string) (ship: Ship) : bool =
    let shipSunk  = List.forall (fun (y, x) -> grid.[y].[x].Hit) ship.Positions
    ship.Id = shipId && not shipSunk

let processFire (input: string) (state: GameState) = 
    match input with 
    | ParseRegex "([a-jA-J])([0-9])$" [CharAsIndex y; Integer x]-> 
        let newGrid = state.Grid

        // what ship has been hit?
        let target = newGrid.[y].[x]
        // it is sunk now?
        let sunkShip = 
            match target.Contents with
            | ShipSection shipId -> List.tryFind (findUnsunkShip state.Grid shipId) state.Ships
            | Water -> None
        
        // set flag for ship has just been sunk
        newGrid.[y].[x] <- { newGrid.[y].[x] with Hit = true }  
        {state with Grid = newGrid; ShipSunk = sunkShip |> Option.isSome}
    | _ -> state

let updateGameState (input: string) (state: GameState) = 
    processFire input state