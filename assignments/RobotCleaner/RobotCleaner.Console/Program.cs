using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using RobotCleaner.Console;

namespace RobotCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new TextRunner(System.Console.In, System.Console.Out);
            runner.RunCleaningSession();
        }
    }
}