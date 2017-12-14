module Domain

type GameState = {
    Grid : string [][] // Jagged array (Array of arrays)
    // Grid : string [,] 2D Array
}

let intialState = {
    Grid = Array.init 10 (fun y -> Array.init 10 (fun x -> if x = 1 then "X" else " "))
}