module DomainTransitions 
open System
open Domain

let updateGameState (state: GameState) = 
    let grid = state.Grid
    let random = new Random()
    let newGrid = 
        grid
        |> Array.map (fun row -> 
            row 
            |> Array.map (fun _ -> if random.NextDouble() > 0.5 then "X" else "M")
            ) 
    {state with Grid = newGrid }