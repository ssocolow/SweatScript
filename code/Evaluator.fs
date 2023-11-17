module Evaluator

// type Date = string
type Date = int

type Time = float

type H2o = Time

type Activity = H2o list

type Day = {date: Date; activity: Activity}

type History = Day list

let CANVAS_SZ = 1000

type Coordinate = { x: float; y: float }

let startingPoint = "122, 734 "
let startX = 122.0
let startY = 734.0
let rightStep = 36
let upStep = 76
let start = {x=startX; y=startY}
// let evalColor (color: Color) : string =
//     match color with
//     | Red -> "rgb(255,0,0)"
//     | Green -> "rgb(0,255,0)"
//     | Blue -> "rgb(0,0,255)"
//     | Purple -> "rgb(128,8,165)"

let evalSingleActivity (h2o: H2o) (whichOne: int) : string =
    let x = string (startX + (h2o / 100.0) * 35.875)
    let y = string (startY - ((float whichOne) * 76.0))
    // printfn $"{whichOne} for y = {y} x = {x} h2o = {h2o}"
    (x + ", " + y + " ")


//if this doesn't work, reverse the list before handing it off
let rec evalActivities (activity: Activity) (whichOne: int): string =
    match activity, whichOne with
    | [],_ -> ""
    | l::ls,x -> (evalActivities ls (x-1)) + (evalSingleActivity l x)

let startEval (activity: Activity): string =
    let num = List.length activity
    let newact = List.rev activity
    evalActivities newact num

let eval (day: Day) : string =
    let csz = CANVAS_SZ |> string
    let len = List.length ((fun a -> a.activity) day)
    let points = startEval ((fun a -> a.activity) day)

    let lastPoint = "983, " + string (startY - ((float len) * 76.0))

    "<svg width=\"" + csz + "\" height=\"" + csz + "\"" +
    " xmlns=\"http://www.w3.org/2000/svg\"" +
    " xmlns:xlink=\"http://www.w3.org/1999/xlink\">\n" +
    "<polyline points=\"" + startingPoint + points + lastPoint + "\" stroke=\"blue\" fill=\"none\" style=\"stroke: blue; stroke-width: 5;\"/>\n" +
    " <image href=\"https://raw.githubusercontent.com/ninjahat77/pastebin/main/health_data_graph.svg\" x=\"0\" y=\"0\" height=\"1500\" width=\"1500\"/>\n" +
    "</svg>\n"