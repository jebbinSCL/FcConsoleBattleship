open System 
open Domain

open Render
open DomainTransitions

// Could do this with a Sequence or observable if we wanted too....
let rec gameCycle (state: GameState) = 
    renderGame state
    let input = Console.ReadLine()
    match input.Trim() with
    | "exit" -> ()
    | input -> 
        state 
        |> updateGameState input
        |> gameCycle


[<EntryPoint>]
let main _ = 
    Console.WriteLine("BATTLESHIP")
    gameCycle intialState
    0
