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
    | _ -> 
        let newState = updateGameState input state
        gameCycle newState

[<EntryPoint>]
let main _ = 
    Console.WriteLine("BATTLESHIP")
    gameCycle intialState
    0
