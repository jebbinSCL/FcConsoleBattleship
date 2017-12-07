module Render

open System
open Domain

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