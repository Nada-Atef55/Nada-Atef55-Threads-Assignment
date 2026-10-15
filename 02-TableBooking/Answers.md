# Assignment 02 — Answers

Student name: Nada Atef Mohamed 
Group / ID: 1

## What is wrong

- the code causes race condition problem , so
  the 5 left tables may be booked by more than 5 customers 

- there are lost updates because increment and decrement are not atomic operations   

## How you fixed it

- i used a lock on the critical part that checks TablesLeft and apply increment/decrement.
  Therefore only one thread executes this critical part at a time
  
  