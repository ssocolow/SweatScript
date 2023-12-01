module Parser

open Evaluator
open Combinator
open System

(*GRAMMAR*)
let number = (pmany1 pdigit) |>> (fun ds -> stringify ds |> int)  <!> "number"
//convert this
// let date = pright (pstr ("date ")) ((pleft number (pchar '-')) pseq(pleft(number pchar ('-')) number (fun (a,b) -> ) )) <!> "date"
let date = pright (pstr ("date ")) number <!> "date"

//convert fill h2o time
// let fillh2o = pright (pstr("h2o ")) (pmany1 (pdigit |>> (fun ds -> ds |> stringify |> float))) <!> "fillh20"
let fillh2o = (pright (pstr("h2o ")) ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> float))) <|> pright (pstr(" h2o ")) ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> float)) <!> "fillh2o"

//make into their respective types 
let expr = pseq date (pmany0 fillh2o) (fun (a, b) -> {date = a; activity = b}) <!> "exp"

//full grammar
let grammar = pleft expr peof

let parse (input: string) : Day option =
    let DEBUG = false
    //let i = prepare input
    let i = if DEBUG then debug input else prepare input
    match grammar i with
    | Success(ast, _) -> Some ast
    | Failure(_,_) -> None