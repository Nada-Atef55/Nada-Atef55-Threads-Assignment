# Assignment 03 — Answers

Student name:  Nada Atef Mohamed
Group / ID: 1

1. it is used to protect shared data by allowing only one thread to access it at a time

2. it is compiled to Monitor.Enter(obj) and Monitor.Exit(obj) with try/finally block
   to ensure that lock is released  

3. Monitor : more manual code , should have try/finally 
   lock : more readable, just a block that has the critical section

4. TryEnter() => when you want to specify how long a thread can wait to acquire the lock

   Wait() => allows the thread to release the lock but wait for a notification by another thread 
   to complete its work or timeout

   Pulse() => notifies the waiting thread that it can complete work when acquiring the lock
   after the current thread completes its work

5. ![using lock](lock)
   ![using Monitor](monitor)




Links:

- GPT => https://chatgpt.com/share/6ad3d999-7794-83ea-8552-39d678c7131d

  dotnet documentation => https://learn.microsoft.com/en-us/dotnet/api/system.threading.monitor?view=net-10.0