module Render

open Domain
open System

let letters = [|'A'..'J'|] |> Array.map string

let renderGame (state: GameState) = 
    Console.BackgroundColor <- ConsoleColor.Blue
    Console.SetCursorPosition(0, 1)
    Console.WriteLine("  | 0 1 2 3 4 5 6 7 8 9 ")
    Console.WriteLine("--+-------------------- ")
    for y = 0 to 9 do 
        Console.Write(letters.[y] + " | ") 
        for x = 0 to 9 do
            let displayString =
                match state.Grid.[y].[x].Hit, state.Grid.[y].[x].Contents with 
                | false, _ -> " "
                | true , Water -> "M"
                | true , ShipSection _ -> "H"
            Console.Write(displayString + " ")
        Console.Write('\n')
    Console.WriteLine("--+-------------------- ")
    if state.ShipSunk then Console.WriteLine("You sank my battleship")