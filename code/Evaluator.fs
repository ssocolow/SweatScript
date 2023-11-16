module Evaluator

type Date = string

type Time = float

type h2o = Time

type Activity =
| h2o list

let CANVAS_SZ = 1000

//keep track of how many refills
let cumulator = 0

type Coordinate = { x: int; y: int }

let startingPoint = "122, 734 "
let startX = 122
let startY = 734
let rightStep = 36
let upStep = 76
let start = {x=startX; y=startY}
// let evalColor (color: Color) : string =
//     match color with
//     | Red -> "rgb(255,0,0)"
//     | Green -> "rgb(0,255,0)"
//     | Blue -> "rgb(0,0,255)"
//     | Purple -> "rgb(128,8,165)"

let evalSingleActivity (h2o: h2o) (whichOne: int) : string =
    let x = string (startX * 35.875 + 122)
    let y = string (startY - whichOne * 76)
    (x + ", " + y + " ")


//if this doesn't work, reverse the list before handing it off
let rec evalActivities (activity: Activity) (whichOne: int): string =
    match activity, whichOne with
    | [],_ -> ""
    | l::ls,x -> (evalSingleActivity l x) + (evalActivities ls (x-1))

let startEval (activity: Activity): string =
    let num = List.length activity
    evalActivities activity num

let eval (activity: Activity) : string =
    let csz = CANVAS_SZ |> string
    "<svg width=\"" + csz + "\" height=\"" + csz + "\"" +
    " xmlns=\"http://www.w3.org/2000/svg\"" +
    " xmlns:xlink=\"http://www.w3.org/1999/xlink\">\n" +
    "<polyline points=\"" + startingPoint + (evalActivities Activity) + "\" stroke=\"blue\" fill=\"none\" style=\"stroke: blue; stroke-width: 10;\"/>" +
    " <image href=\"health_data_graph.svg\" x=\"0\" y=\"0\" height=\"1500\" width=\"1500\"/>\n" +
    "</svg>\n"