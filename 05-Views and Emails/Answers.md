# Assignment 05 — Answers

Student name:  Nada Atef Mohamed
Group / ID: 1

## Failures you observed

- exception because a thread tries to add a key that already added by another thread 
  into the dictionary 

- dictionary causes lost updates on views because increment is not atomic operation

- multiple enque operations at the same place by multiple threads at the same time 
  leads to overwrite so many orders are lost and not placed

- multiple threads can dequeue the last email which leads to exception

## What you changed

- i used ConcurrentQueue to allow multiple threads to add or remove elements safely

- i used ConcurrentDictionary to prevent lost updates and exceptions by AddOrUpdate()

## Collection safety vs business-rule safety

- Concurrent collections only make the operations they provide thread safe,
  but to protect a sequence of business rules (operations) like conditons or non atomic operations,
  you have to use other things like lock and Interlocked