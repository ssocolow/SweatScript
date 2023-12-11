module Parser

open Evaluator
open Combinator
open System
open AST

(*GRAMMAR*)
let number = (pmany1 pdigit) |>> (fun ds -> stringify ds |> int)  <!> "number"
let numberString = (pmany1 pdigit) |>> (fun ds -> stringify ds)  <!> "numberString"
let date = pright (pstr ("date ")) numberString <!> "date"

let fillh2o = pright (pstr(" h2o ")) ((pmany1 pdigit)) |>> (fun ds -> ds |> stringify |> int |> (fun x -> {name = "h2o"; modifiers= {duration = x; avgHR = -1} })) <!> "fillh2o"
let up = pright (pstr (" up ")) ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> int)) <!> "up"
let sleep = pright (pstr (" sleep ")) ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> int)) <!> "sleep"

let activity (a: string) = 
    (pseq 
        (pbetween 
            (pstr (" " + a + " ")) 
                ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> int)) 
            (pstr " mins")) 
        (pright 
            (pstr " avghr ") ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> int))) 
    (fun (dur,HR) -> {name=a; modifiers={duration=dur; avgHR=HR}}))
    <|> (pbetween 
            (pstr (" " + a + " ")) 
                ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> int)) 
            (pstr " mins") |>> 
        (fun dur -> {name=a; modifiers={duration = dur; avgHR = -1}})) <!> "activity"
    
let run = activity "run" <!> "run"
let bike = activity "bike" <!> "bike"
let berg = activity "berg" <!> "berg"
let erg = activity "erg" <!> "erg"
let squash = activity "squash" <!> "squash"

let insideDayExpressions = run <|> bike <|> berg <|> erg <|> squash <|> fillh2o <!> "inside day exp"

let expr = pseq (pseq date up (fun (theDate,upTime) -> (theDate,upTime))) (pseq (pmany0 insideDayExpressions) sleep (fun (allActivities, downTime) -> (allActivities, downTime))) (fun ((theDate,upTime),(allActivities, downTime)) -> {date=theDate; wakeTime=upTime; bedTime=downTime; activities=allActivities}) <!> "exp"
let exprList = pmany1 ((pleft expr (pstr "\n")) <|> expr)

//full grammar
let grammar = pleft exprList peof

let parse (input: string) : History option =
    let DEBUG = false
    //let i = prepare input
    let i = if DEBUG then debug input else prepare input
    match grammar i with
    | Success(ast, _) -> Some ast
    | Failure(_,_) -> None