module Render

open Domain
open System

let letters = [|'A'..'J'|] |> Array.map string

let renderGame (state: GameState) = 
    Console.Clear()
    Console.WriteLine("BATTLESHIP")
    Console.WriteLine("  | 0 1 2 3 4 5 6 7 8 9 ")
    Console.WriteLine("--+-------------------- ")
    for y = 0 to 9 do 
        Console.Write(letters.[y] + " | ") 
        Console.BackgroundColor <- ConsoleColor.Blue
        for x = 0 to 9 do
            let displayString =
                match state.Grid.[y].[x].Hit, state.Grid.[y].[x].Contents with 
                | false, _ -> " "
                | true , Water -> "M"
                | true , ShipSection _ -> "H"
            Console.Write(displayString + " ")
        Console.BackgroundColor <- ConsoleColor.Black
        Console.Write('\n')
    Console.WriteLine("--+-------------------- ")
    if state.ShipSunk then 
        Console.WriteLine("You sank my battleship  ") 
