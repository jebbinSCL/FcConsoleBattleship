open System 
open Domain

open Render
open DomainTransitions

// Could do this with a Sequence or observable if we wanted too....
let rec gameCycle (state: GameState) = 
    renderGame state
    let input = Console.ReadLine()
    match input.Trim(), state.Ships.Length = 0 with
    | "exit", _ -> ()
    | input, false ->
        state 
        |> updateGameState input
        |> gameCycle
    | _, true ->
        initialState () |> gameCycle


[<EntryPoint>]
let main _ = 
    initialState () |> gameCycle
    0
