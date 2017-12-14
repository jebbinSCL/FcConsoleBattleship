module Domain

type Ship = {
    Positions: (int * int) []
}

type GameState = {
    Ship : Ship
    Grid : string [][] // Jagged array (Array of arrays)
    // Grid : string [,] 2D Array
}

let intialState = {
    Ship = { Positions = [|(1, 1); (1, 2) ; (1, 3) |] }
    Grid = Array.init 10 (fun y -> Array.init 10 (fun x -> " "))
}