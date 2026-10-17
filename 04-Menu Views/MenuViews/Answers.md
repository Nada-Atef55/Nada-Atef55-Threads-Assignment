# Assignment 04 — Answers

Student name:  Nada Atef Mohamed
Group / ID: 1

## Why Expected and Actual disagree

- because Views is shared data and increment is not atomic operation,
  it is (read + increment + write) so multiple threads can read it halfway and get the same value x then update it to x + 1 so there are many lost updates

## What you changed

- i used Interlocked.Increment() to increment Views safely because it ensures that increment 
  will be atomic operation , no thread can read it halfway

## When would you protect the update differently

- when i only have operation without a condition.
  but if i have condition + operation , i have to use lock on both of them
  to prevent race condition 