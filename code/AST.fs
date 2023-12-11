module AST

type Date = string

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
