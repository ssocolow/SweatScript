module Evaluator
open AST
open Plotly.NET

// let startingPoint = "122, 734 "
// let startX = 122.0
// let startY = 734.0
// let rightStep = 36
// let upStep = 76
// let start = {x=startX; y=startY}
// let evalColor (color: Color) : string =
//     match color with
//     | Red -> "rgb(255,0,0)"
//     | Green -> "rgb(0,255,0)"
//     | Blue -> "rgb(0,0,255)"
//     | Purple -> "rgb(128,8,165)"

let startHTML = "<html><head><script src=\"https://cdn.plot.ly/plotly-2.21.0.min.js\" charset=\"utf-8\"></script><title>Plotly.NET Datavisualization</title><meta charset=\"UTF-8\"><meta name=\"description\" content=\"A plotly.js graph generated with Plotly.NET\"><link id=\"favicon\" rel=\"shortcut icon\" type=\"image/png\" href=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAMAAACdt4HsAAAA1VBMVEVHcEwQnv+gCXURnf+gCXURnf8Rnf+gCXURnf+gCXWgCXURnf+gCHURnf+gCXURnf+gCXURnf+gCXUwke5YVbykBXEijO+gCXURnf8Rnf8Rnf8Rnf8Rnf8Rnf+gCXWIIoygCXUohekRnf8Rnf8Qn/+gCXUQnf8SoP////8ijO+PG4agAnGQLY6gEnrP7f94yP8aof8YwP/DY6jJcrDuz+RlwP/owt0Urv8k/v4e4v9Nr9F1XaSxMoyx3/9rc7Ayq/98UZ3gr9L8+v05rv9Fv9rF5/+7T52h9OprAAAAJHRSTlMAINTUgPmA+gYGNbu7NR9PR/xP/hoh/o74f471R3x8uie60TS1lKLVAAABzUlEQVRYw83X2XKCMBQGYOyK3RdL9x0ChVCkVAHFfXn/RyphKSIBE85Mp8woV/8HOUByIgj/+mg2yb8o1s4/nZHTw2NNobmzf0HOp/d7Ys18Apzv1hHCvJICqIZA8hnAL0T5FYBXiPOrAJ+Q5HMAj5Dm8wC78JtfA1iFLK8oeYBNWM1vvQitltB4QxxCLn8gXD2/NoTjbXZhLX9ypH8c8giFvKJLiEMo5gnALlDyEcAq0PIxwCZQ8wnAItDzKbBZKObNBJDlMCFvEor5YQ8buDfUJdt3kevb1QLl+j2vb4y9OZZ8z0a251feA238uG8qZh/rkmurSLXdqjrQ62eQn5EWsaqS9Dweh3ewDOI7aHdG5ULJ8yM1WE67cQ0604FaJqx/v0leGc6x8aV94+gpWNqiTR3FrShcU68fHqYSA3J47Qwgwnsm3NxtBtR2NVA2BKcbxIC1mFUOoaSIZldzIuDyU+tkAPtjoAMcLwIV4HkVaQDXx0ABOD9HZxIYwcTRJWswQrOBxT8hpBMKIi+xWmdK4pvS4JMqfFqHLyzwpQ2+uMKXd3iDAW9x4E0WvM2DN5rwVhfebMPbffiGA77lgW+64Ns++MYTvvX9m+MHc8vmMWg2fMUAAAAASUVORK5CYII=\"><title>Plotly.NET Datavisualization</title><meta charset=\"UTF-8\"><meta name=\"description\" content=\"A plotly.js graph generated with Plotly.NET\"><link id=\"favicon\" rel=\"shortcut icon\" type=\"image/png\" href=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAMAAACdt4HsAAAA1VBMVEVHcEwQnv+gCXURnf+gCXURnf8Rnf+gCXURnf+gCXWgCXURnf+gCHURnf+gCXURnf+gCXURnf+gCXUwke5YVbykBXEijO+gCXURnf8Rnf8Rnf8Rnf8Rnf8Rnf+gCXWIIoygCXUohekRnf8Rnf8Qn/+gCXUQnf8SoP////8ijO+PG4agAnGQLY6gEnrP7f94yP8aof8YwP/DY6jJcrDuz+RlwP/owt0Urv8k/v4e4v9Nr9F1XaSxMoyx3/9rc7Ayq/98UZ3gr9L8+v05rv9Fv9rF5/+7T52h9OprAAAAJHRSTlMAINTUgPmA+gYGNbu7NR9PR/xP/hoh/o74f471R3x8uie60TS1lKLVAAABzUlEQVRYw83X2XKCMBQGYOyK3RdL9x0ChVCkVAHFfXn/RyphKSIBE85Mp8woV/8HOUByIgj/+mg2yb8o1s4/nZHTw2NNobmzf0HOp/d7Ys18Apzv1hHCvJICqIZA8hnAL0T5FYBXiPOrAJ+Q5HMAj5Dm8wC78JtfA1iFLK8oeYBNWM1vvQitltB4QxxCLn8gXD2/NoTjbXZhLX9ypH8c8giFvKJLiEMo5gnALlDyEcAq0PIxwCZQ8wnAItDzKbBZKObNBJDlMCFvEor5YQ8buDfUJdt3kevb1QLl+j2vb4y9OZZ8z0a251feA238uG8qZh/rkmurSLXdqjrQ62eQn5EWsaqS9Dweh3ewDOI7aHdG5ULJ8yM1WE67cQ0604FaJqx/v0leGc6x8aV94+gpWNqiTR3FrShcU68fHqYSA3J47Qwgwnsm3NxtBtR2NVA2BKcbxIC1mFUOoaSIZldzIuDyU+tkAPtjoAMcLwIV4HkVaQDXx0ABOD9HZxIYwcTRJWswQrOBxT8hpBMKIi+xWmdK4pvS4JMqfFqHLyzwpQ2+uMKXd3iDAW9x4E0WvM2DN5rwVhfebMPbffiGA77lgW+64Ns++MYTvvX9m+MHc8vmMWg2fMUAAAAASUVORK5CYII=\"></head><body>"
let endHTML = "</body></html>"


// let evalSingleActivity (h2o: H2o) (whichOne: int) : string =
//     let x = string (startX + (h2o / 100.0) * 35.875)
//     let y = string (startY - ((float whichOne) * 76.0))
//     // printfn $"{whichOne} for y = {y} x = {x} h2o = {h2o}"
//     (x + ", " + y + " ")


//if this doesn't work, reverse the list before handing it off
// let rec evalActivities (activity: Activity) (whichOne: int): string =
//     match activity, whichOne with
//     | [],_ -> ""
//     | l::ls,x -> (evalActivities ls (x-1)) + (evalSingleActivity l x)

// let startEval (activity: Activity): string =
//     let num = List.length activity
//     let newact = List.rev activity
//     evalActivities newact num

// let eval (day: Day) : string =
//     let csz = CANVAS_SZ |> string
//     let len = List.length ((fun a -> a.activity) day)
//     let points = startEval ((fun a -> a.activity) day)

//     let lastPoint = "983, " + string (startY - ((float len) * 76.0))

//     "<svg width=\"" + csz + "\" height=\"" + csz + "\"" +
//     " xmlns=\"http://www.w3.org/2000/svg\"" +
//     " xmlns:xlink=\"http://www.w3.org/1999/xlink\">\n" +
//     "<polyline points=\"" + startingPoint + points + lastPoint + "\" stroke=\"blue\" fill=\"none\" style=\"stroke: blue; stroke-width: 5;\"/>\n" +
//     " <image href=\"https://raw.githubusercontent.com/ninjahat77/pastebin/main/health_data_graph.svg\" x=\"0\" y=\"0\" height=\"1500\" width=\"1500\"/>\n" +
//     "</svg>\n"

//method for time math, returns in minutes
//expects time in 0612 is 6:12am format and 1342 is 1:42pm
let calcTimeSub (finishTime: int) (startTime: int) = ((finishTime / 100) - (startTime / 100)) * 60 + ((finishTime % 100) - (startTime % 100))

let ifH2o1else0 (a: Activity) = 
    match a.name with
    | "h2o" -> a.modifiers.duration
    | _ -> 0

let getCardioTimes (a: Activity) =
    match a.name with
    | "h2o" -> 0
    | _ -> a.modifiers.duration

let listToNums f (l: Activity list) = l |> List.map (fun a -> f a)

let take2Dlist ll f = ll |> List.map (fun l -> listToNums f l)

let take2DlistToInt ll = ll |> List.map (fun l -> List.sum l)

let makeSleepGraph times dates = Chart.StackedColumn(values = times, Keys = dates) |> Chart.withYAxisStyle (TitleText = "Mins") |> GenericChart.toEmbeddedHTML

let makeH2oGraph sums dates = Chart.StackedColumn(values = sums, Keys = dates) |> Chart.withYAxisStyle (TitleText = "Units of Water") |> GenericChart.toEmbeddedHTML

let makeActivityGraph sums dates = Chart.StackedColumn(values = sums, Keys = dates) |> Chart.withYAxisStyle (TitleText = "Minutes of Cardio") |> GenericChart.toEmbeddedHTML

//goes through list of days, makes list of dates, sleepTimes, and list of lists of activities
let deconstruct (history: History) = 
    //printfn "%A" (List.length history)
    let dates = history |> List.map (fun day -> day.date)
    let sleepTimes = history |> List.map (fun day -> calcTimeSub day.bedTime day.wakeTime)
    let activities2D = history |> List.map (fun day -> day.activities)

    let newlist = take2Dlist activities2D listToNums
    let h2oSums = take2DlistToInt newlist

    let sleepGraph = makeSleepGraph sleepTimes dates
    let h2oGraph = makeH2oGraph h2oSums dates

    let tempActList = take2Dlist activities2D getCardioTimes
    let activityGraph = makeActivityGraph take2DlistToInt tempActList

    sleepGraph + h2oGraph + activityGraph
    // match history with
    // | [] -> ""
    // | head :: tail ->
    //     eval head + destruct tail

let startEval (history: History) = startHTML + (deconstruct history) + endHTML