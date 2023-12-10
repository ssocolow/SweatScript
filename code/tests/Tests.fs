namespace tests

open Evaluator
open Parser
open AST

open System
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.TestMethodPassing () =
        Assert.IsTrue(true);

    [<TestMethod>]
    member this.checkParsing () =
        let input = "date 04112023 up 0700 h2o 37 run 60 mins sleep 2330"

        let n1 = -1
        let testActivities: Activity list = [{name="h2o"; modifiers={ duration=37; avgHR=n1}}; {name="run"; modifiers={ duration=60; avgHR=n1}}]
        let expected: Day = {date="04112023"; wakeTime=700; bedTime=2330; activities=testActivities }
        
        let result = parse input
        match result with
        | Some warr -> Assert.AreEqual(expected, warr[0])
        | None -> Assert.IsTrue false

    [<TestMethod>]
        member this.checkEvaluator () =
            let startTime = 0840
            let endTime = 2340

            let correct = 900

            let result = calcTimeSub endTime startTime

            Assert.AreEqual(correct, result)

