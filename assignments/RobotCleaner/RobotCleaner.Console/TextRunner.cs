using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.CompilerServices;
using RRobotCleaner = RobotCleaner.Lib.RobotCleaner;

namespace RobotCleaner.Console
{
    public class TextRunner
    {
        private TextReader Input { get; set; }
        private TextWriter Output { get; set; }

        private int _numberOfCommands = 0;
        
        private readonly Lib.RobotCleaner _robotCleaner;
        
        public TextRunner(TextReader input, TextWriter output)
        {
            Input = input;
            Output = output;
            _robotCleaner = new Lib.RobotCleaner();
        }

        public void RunCleaningSession()
        {
            GetNumberOfCommands();
            SetStartingCoordinates();
            CollectAndRunCommands();
            OutputResults();
        }

        private void GetNumberOfCommands()
        {
            //Output.WriteLine("Enter number of commands: ");
            var input = Input.ReadLine();
            _numberOfCommands = int.Parse(input);
        }

        private void SetStartingCoordinates()
        {
            //Output.WriteLine("Enter starting coordinates: ");
            var input = Input.ReadLine();
            var start = input?.Split(' ');
            _robotCleaner.SetStartingCoordinates(int.Parse(start[0]), int.Parse(start[1]));
        }

        private void CollectAndRunCommands()
        {
            for (var i = 0; i < _numberOfCommands; i++)
            {
                //Output.WriteLine($"Input command {i+1}: ");
                var command = Input.ReadLine();
                var cmdParts = command?.Split(' ');
                _robotCleaner.Move(cmdParts[0], int.Parse(cmdParts[1]));
            }
        }

        private void OutputResults()
        {
            Output.WriteLine($"=> Cleaned: {_robotCleaner.GetUniquePlacesCleaned()}" );
        }
    }
}