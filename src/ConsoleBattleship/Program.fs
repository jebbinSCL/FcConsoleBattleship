open System 
open Domain

open Render
open DomainTransitions

// Could do this with a Sequence or observable if we wanted too....
let rec gameCycle isDebugMode (state: GameState) = 
    renderGame state isDebugMode
    let input = Console.ReadLine()
    match input.Trim(), state.Ships.Length = 0 with
    | "exit", _ -> ()
    | input, false ->
        state 
        |> updateGameState input
        |> gameCycle isDebugMode
    | _, true ->
        initialState () |> gameCycle isDebugMode


[<EntryPoint>]
let main args = 
    let isDebugMode = function | [|"debug"|] -> true | _ -> false
    initialState () |> gameCycle (isDebugMode args)
    0
