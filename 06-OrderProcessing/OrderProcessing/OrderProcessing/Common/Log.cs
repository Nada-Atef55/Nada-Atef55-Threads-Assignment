using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Common;

public static class Log
{
    public static void Info(string source, string message)
    {
        Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss.fff")}] " +
            $"[Thread {Thread.CurrentThread.ManagedThreadId}] " +
            $"{source} -> {message}");
    }

    public static void Success(string source, string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss.fff")}] " +
            $"[Thread {Thread.CurrentThread.ManagedThreadId}] " +
            $"{source} -> {message}");

        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void Error(string source, string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;

        Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss.fff")}] " +
            $"[Thread {Thread.CurrentThread.ManagedThreadId}] " +
            $"{source} -> {message}");

        Console.ForegroundColor = ConsoleColor.White;
    }
}
