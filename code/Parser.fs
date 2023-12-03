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
// let fillh2o = pright (pstr("h2o ")) (pmany1 (pdigit |>> (fun ds -> ds |> stringify |> float))) <!> "fillh2o"
// let fillh2o = (pright (pstr("h2o ")) ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> float))) <|> pright (pstr(" h2o ")) ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> float)) <!> "fillh2o"
let fillh2o = pright (pstr(" h2o ")) ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> int |> (fun x -> {name="h2o"; modifiers={time=x; duration=-1; avgHR=-1}}))) <!> "fillh2o"
let up = pright (pstr (" up ")) ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> int)) <!> "up"
let sleep = pright (pstr (" sleep ")) ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> int)) <!> "sleep"

// let run = pbetween (pstr " run ") ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> float)) (pstr " mins") <!> "run"
// let bike = pbetween (pstr " bike ") ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> float)) (pstr " mins") <!> "bike"
// let berg = pbetween (pstr " berg ") ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> float)) (pstr " mins") <!> "berg"
// let squash = pbetween (pstr " squash ") ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> float)) (pstr " mins") <!> "squash"
// let avgHR = pright (pstr (" avg hr ")) ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> int)) <!> "avg hr"

let run = activity "run" <!> "run"
let bike = activity "bike" <!> "bike"
let berg = activity "berg" <!> "berg"
let squash = activity "squash" <!> "squash"
// let avgHR = activity "avghr" <!> "avg hr"
let activity (a: string) = (pseq (pbetween (pstr (" " + a + " ")) ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> int)) (pstr " mins")) (pright (pstr " avghr ") ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> int))) (fun (dur,HR) -> {name=a; modifiers={time=-1; duration=dur; avgHR=HR}}))
    <|> (pbetween (pstr (" " + a + " ")) ((pmany1 pdigit) |>> (fun ds -> ds |> stringify |> int)) (pstr " mins") |>> (fun dur -> {name=a; modifiers={time=-1; duration=dur; avgHR=-1}})) <!> "activity"

//let activityModifier (a: string) = pbetween (pstr (" " + a + "")) ((pmany1))

//DO BIKE, BERG, SQUASH activities 
// let runActivity = 
//     pseq run avgHr (fun (a,b) -> {time = a; avgHr = Some b}) <|>  
//     run |>> (fun a -> {time = a; avgHr = None}) <!> "run Activity"

let insideDayExpressions = run <|> bike <|> berg <|> squash <|> fillh2o <!> "inside day exp"

//make into their respective types
//FIX EXPR TO INCLUDE EVERYTHING 
let expr = pseq (pseq date up (fun (theDate,upTime) -> (theDate,upTime))) (pseq (pmany0 insideDayExpressions) sleep (fun (allActivities, downTime) -> (allActivities, downTime))) (fun ((theDate,upTime),(allActivities, downTime)) -> {date=theDate; wakeTime=upTime; bedTime=downTime; activities=allActivities}) <!> "exp"
let exprList = pmany1 (pleft expr (pstr "\n"))

//full grammar
let grammar = pleft exprList peof

let parse (input: string) : History option =
    let DEBUG = true
    //let i = prepare input
    let i = if DEBUG then debug input else prepare input
    match grammar i with
    | Success(ast, _) -> Some ast
    | Failure(_,_) -> None