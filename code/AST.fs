module AST

type Date = int
type Time = float
type Up = Time
type Sleep = Time

type H2o = Time
type H2oList = H2o list

// type Run = {time: Time; avgHR: HeartRate option}
// type RunList = Run list

// type Bike = {time: Time; avgHR: HeartRate}
// type BikeList = Bike list

// type Berg = {time: Time; avgHR: HeartRate}
// type BergList = Berg list

// type Activity = [RunList; BikeList; BergList]

// type Activity = 
// | Run of exercise List
// | Bike of exercise List
// | Berg of exercise List
// | Squash of exercise List
// and exercise
// | {time: Time; avgHR: HeartRate}

//names of each possible activity, currently NOT the way this works
type ActivityName = 
| Run
| Bike
| Berg
| Squash

//some events like h2o or sleep only use time, so other fields will be set to -1
type ActivityModifiers = { duration: int; avgHR: int}

//combinations of the activity with activity modifiers
type Activity = {name: string; modifiers: ActivityModifiers}

//GOAL: make 3 graphs with dates on y axis for totalHydration totalExercise totalSleep 
//and possibly make an svg of most recent day's hydration over the day
//all displayed in one html page

//activity is a list of records
type Day = {date: Date; wakeTime: int; bedTime: int; activities: Activity list}

type History = Day list

let CANVAS_SZ = 1000

type Coordinate = { x: float; y: float }

