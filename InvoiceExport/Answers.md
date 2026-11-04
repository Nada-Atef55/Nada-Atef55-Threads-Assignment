# Assignment 01 — Answers

Student name: Nada Atef Mohamed
Group / ID: 1

## Problems with the current design

- each Thread.Sleep() blocks the calling thread 

## What you changed

- i converted each method that does I/O-bound work to async method

- so then i can use Task.Delay() and await on this task, 
  the calling thread now is not blocked and can do other work instead of being idle
  
- the spent time didn't change because each operation needs to be awaited before continuing 
  so ExportAllPending() is suspended on each of them

- it took 5.9 s before and after changes, but the changes made it better design  