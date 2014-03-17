using System;
using System.Diagnostics;

namespace AsyncApplication
{
    public class Util
    {
        public static void WriteLog(String text, int threadNumber, Stopwatch stopwatch = null)
        {
            if (threadNumber == 1)
                Console.ForegroundColor = ConsoleColor.White;
            else if (threadNumber == 2)
                Console.ForegroundColor = ConsoleColor.Red;
            else if (threadNumber == 3)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (threadNumber == 4)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Cyan;

            if (stopwatch != null)
            {
                stopwatch.Stop();
                Console.Write("[" + stopwatch.Elapsed.ToString() + "] ");
            }

            Console.Write(text + "\n");
        }
    }
}
