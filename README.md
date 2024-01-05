# SweatScript
  
The SweatScript programming language is an open-source way of tracking health information like sleep, exercise, and hydration.  Graphs that visually display these metrics are created by an evaluator that takes in text descriptions of activities throughout the day.  SweatScript is a group final project (with Rafay Syed and I) for our Principles of Programming Languages class.  

A video explaining our project can be found [here](https://drive.google.com/file/d/13YSW_v95SYJZI9h1CKGNbrzBc8MXVOMw/view?usp=sharing) and a pdf specification document can be found [here](https://github.com/ssocolow/SweatScript/blob/main/docs/specification.pdf)  

## Examples
example-2.ss  
```
date 04052023 up 0758 h2o 18 squash 20 mins h2o 15 sleep 2200
date 04062023 up 0809 h2o 12 h2o 11 berg 60 mins avghr 130 h2o 18 sleep 2300
date 04072023 up 0700 h2o 46 run 47 mins avghr 135 sleep 2330
date 04082024 up 0800 h2o 37 berg 36 mins squash 43 mins sleep 2320
```
generates this html page:
![alt text](https://raw.githubusercontent.com/ssocolow/SweatScript/main/docs/example-2-expectedOutput.png "Example Output")
  
## Quickstart
First, [install dotnet](https://dotnet.microsoft.com/en-us/download) as the parser and evaluator are written in F#.  

Then, clone this repository with `git clone https://github.com/ssocolow/SweatScript.git`, and move to the directory with example files with `cd SweatScript/code/`.  

Next, install the Plotly package which is used for generating graphs `dotnet add package Plotly.NET --version 4.2.0`.  

Run `dotnet build` to build this project.  

Finally, generate an example html file output using example-2.ss with `dotnet run example-2.ss > sweatscript-test.html` and open the generated html file in a web browser.  You should see the page pictured above in Examples.
