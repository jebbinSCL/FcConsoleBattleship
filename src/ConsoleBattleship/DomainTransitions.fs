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

let processFire (input: string) (state: GameState) = 
    match input with 
    | ParseRegex "([a-jA-J])([0-9])$" [CharAsIndex y; Integer x]-> 
        let newGrid = state.Grid
        if state.Grid.[y].[x] = "X" then
            newGrid.[y].[x] <- "H"
        else
            newGrid.[y].[x] <- "M"
        {state with Grid = newGrid}
    | _ -> state

let updateGameState (input: string) (state: GameState) = 
    processFire input state