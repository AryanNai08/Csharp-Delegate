// ===============================================================
// DELEGATE BASIC EXAMPLE (SIMPLE + REAL UNDERSTANDING)
// Demonstrates:
// 1. What is delegate
// 2. Delegate with instance method
// 3. Delegate with static method
// 4. Multicast delegate
// 5. Delegate as parameter
// ===============================================================

using System;
using System.IO;

namespace DelegateBasicExample
{
    class Program
    {
        // STEP 1: Declare delegate
        // Delegate is like a function pointer
        // It can store reference of any method with same signature
        delegate void LogDel(string text);

        static void Main(string[] args)
        {
            // Create object of Log class (for instance methods)
            Log log = new Log();

            // =========================================================
            // STEP 2: Assign instance methods to delegate
            // =========================================================

            // Delegate pointing to instance method (screen)
            LogDel logToScreen = new LogDel(log.LogTextToScreen);

            // Delegate pointing to instance method (file)
            LogDel logToFile = new LogDel(log.LogTextToFile);

            // =========================================================
            // STEP 3: Multicast delegate
            // Multiple methods combined into one delegate
            // =========================================================
            LogDel multiLog = logToScreen + logToFile;

            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            // Calls BOTH methods:
            // 1. Print on screen
            // 2. Save in file
            multiLog(name);

            // =========================================================
            // STEP 4: Delegate with static method
            // =========================================================
            LogDel staticDel = new LogDel(LogTextToScreen);
            staticDel("Static method call example");

            // =========================================================
            // STEP 5: Passing delegate as parameter
            // =========================================================
            LogText(logToFile, "Saved using delegate parameter");

            Console.ReadKey();
        }


        // =========================================================
        // STATIC METHOD → matches delegate signature
        // =========================================================
        static void LogTextToScreen(string text)
        {
            Console.WriteLine($"{DateTime.Now} : {text}");
        }

        // Static method writing into file
        static void LogTextToFile(string text)
        {
            using (StreamWriter sw = new StreamWriter("log.txt", true))
            {
                sw.WriteLine($"{DateTime.Now} : {text}");
                Console.WriteLine("Written into file (static)");
            }
        }

        // =========================================================
        // Delegate passed as parameter
        // Any method can be passed dynamically
        // =========================================================
        static void LogText(LogDel del, string msg)
        {
            del(msg); // invoke delegate
        }
    }


    // ===============================================================
    // CLASS WITH INSTANCE METHODS
    // ===============================================================
    public class Log
    {
        // Instance method → print to screen
        public void LogTextToScreen(string text)
        {
            Console.WriteLine($"{DateTime.Now} : {text}");
        }

        // Instance method → write to file
        public void LogTextToFile(string text)
        {
            using (StreamWriter sw = new StreamWriter("log.txt", true))
            {
                sw.WriteLine($"{DateTime.Now} : {text}");
                Console.WriteLine("Written into file (instance)");
            }
        }
    }
}
