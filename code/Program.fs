open Evaluator
open System
open Parser

[<EntryPoint>]
let main args =
    try
        let file = args[0]
        let text = IO.File.ReadAllText file
        match parse text with
        | Some ast ->
            let svg = startEval ast
            printfn "%A" svg
            0
        | None ->
            printfn "Invalid program."
            1
    with
    | _ -> printfn "Usage: dotnet run day.rs > result.svg"; 1;