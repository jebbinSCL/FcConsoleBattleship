module DomainTransitions 
open System
open Domain
open System.Text.RegularExpressions

//https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/active-patterns 

let letters = [|'A'..'J'|]

let updateGameState (input: string) (state: GameState) =
    let xy = input.ToUpperInvariant().ToCharArray()
    let x = letters |> Array.tryFindIndex(fun x -> x = xy.[0]) 
    let y = Int32.Parse (xy.[1].ToString())
    match x,y with
    | None,_ -> state
    | _, y when y > 9 -> state
    | Some x,y ->
        let newGrid = state.Grid 
        newGrid.[x].[y] <- "X"
        let newState = {state with Grid = newGrid}
        newState

let updateGameState' (input: string) (state: GameState) =
    let xy = input.ToUpperInvariant().ToCharArray()
    let x = letters |> Array.tryFindIndex(fun x -> x = xy.[0]) 
    let parseSuccess, y = Int32.TryParse (xy.[1].ToString())
    match x,parseSuccess with
    | None,_ -> state
    | _, false -> state
    | Some x, true when y > 0 && y < 9->
        let newGrid = state.Grid 
        newGrid.[x].[y] <- "X"
        let newState = {state with Grid = newGrid}
        newState
    | _ -> state