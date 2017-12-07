module DomainTransitions 
open System
open Domain
open System.Text.RegularExpressions

let letters = ['A' .. 'J'] |> List.map string

let (|CharAsIndex|) (str: string) = 
    letters |> List.findIndex (fun letter -> letter = str.ToUpperInvariant())

let (|Integer|) (str: string) =
    Int32.Parse str

let (|ParseRegex|_|) regex str =
   let m = Regex(regex).Match(str)
   if m.Success
   then Some (List.tail [ for x in m.Groups -> x.Value ])
   else None

let processFire (input: string) (state: GameState) = 
    match input with 
    | ParseRegex "([a-jA-J])([0-9])$" [CharAsIndex y; Integer x]-> 
        let newGrid = state.Grid
        newGrid.[y].[x] <- "H"
        {state with Grid = newGrid}
    | _ -> state

let updateGameState (input: string) (state: GameState) = 
    state 
    |> processFire input 