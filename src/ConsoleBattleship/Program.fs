open System 
open Domain

module Render = 

    let letters = [|'A'..'Z'|] |> Array.map string

    let renderGame (state: GameState) = 
        Console.SetCursorPosition(0, 1)
        Console.WriteLine("  | 0 1 2 3 4 5 6 7 8 9")
        Console.WriteLine("--+--------------------")
        for y = 0 to 9 do 
            Console.Write(letters.[y] + " | ") 
            for x = 0 to 9 do 
                Console.Write(state.Grid.[y].[x] + " ")
            Console.Write('\n')
        Console.WriteLine("--+--------------------")

open Render
open DomainTransitions

// Could do this with a Sequence or observable if we wanted too....
let rec gameCycle (state: GameState) = 
    renderGame state
    let input = Console.ReadLine()
    match input.Trim() with
    | "exit" -> ()
    | _ -> 
        state 
        |> updateGameState 
        |> gameCycle

[<EntryPoint>]
let main _ = 
    Console.WriteLine("BATTLESHIP")
    gameCycle intialState
    0
