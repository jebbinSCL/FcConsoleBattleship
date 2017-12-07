open System 

type GameState = {
    Grid : string [][] // Jagged array (Array of arrays)
    // Grid : string [,] 2D Array
}

let letters = [|'A'..'Z'|] |> Array.map string

let intialState = {
    Grid = Array.init 10 (fun y -> Array.init 10 (fun x -> " "))
}

let updateGameState state = 
    let grid = state.Grid
    let random = new Random()
    let newGrid = 
        grid
        |> Array.map (fun row -> 
            row 
            |> Array.map (fun _ -> if random.NextDouble() > 0.5 then "X" else "M")
            ) 
    {state with Grid = newGrid }

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

let rec gameCycle (state: GameState) = 
    renderGame state
    let input = Console.ReadLine()
    match input.Trim() with
    | "exit" -> ()
    | _ -> 
        let newState = updateGameState state
        gameCycle newState

[<EntryPoint>]
let main _ = 
    Console.WriteLine("BATTLESHIP")
    gameCycle intialState
    0
