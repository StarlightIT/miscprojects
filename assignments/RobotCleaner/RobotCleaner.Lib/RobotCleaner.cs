using System.Collections.Generic;
using System.Drawing;
using System.Security.Claims;

namespace RobotCleaner.Lib
{
    public class RobotCleaner : IRobotCleaner
    {
        private int NumberOfCommands { get; set; }
        private Point StartingPoint { get; set; }
        public Point LastEndPoint { get; private set; }
        private readonly HashSet<Point> _uniquePlacesCleaned = new HashSet<Point>();
        
        public void SetNumberOfCommands(int numberOfCommands)
        {
            NumberOfCommands = numberOfCommands;
        }

        public void SetStartingCoordinates(int x, int y)
        {
            StartingPoint = new Point(x, y);
            LastEndPoint = new Point(x, y);
            _uniquePlacesCleaned.Add(StartingPoint);
        }

        public void Move(string direction, int steps)
        {
            var lastEndPoint = LastEndPoint;
            
            switch (direction)
            {
                case "N":
                    lastEndPoint = MoveUp(steps);
                    break;
                case "S":
                    lastEndPoint = MoveDown(steps);
                    break;
                case "E":
                    lastEndPoint = MoveRight(steps);
                    break;
                case "W":
                    lastEndPoint = MoveLeft(steps);
                    break;
            }

            LastEndPoint = lastEndPoint;
        }

        public int GetUniquePlacesCleaned()
        {
            return _uniquePlacesCleaned.Count;
        }

        private Point MoveUp(int steps)
        {
            var index = LastEndPoint.Y+1;
            var lastIndex = LastEndPoint.Y + steps + 1;
            for (; index < lastIndex; index++)
            {
                AddCleanedPoint(LastEndPoint.X, index);
            }
            return new Point(LastEndPoint.X, index - 1);
        }

        private Point MoveDown(int steps)
        {
            var index = LastEndPoint.Y - 1;
            var lastIndex = LastEndPoint.Y - steps - 1;
            for (; index > lastIndex; index--)
            {
                AddCleanedPoint(LastEndPoint.X, index);
            }
            return new Point(LastEndPoint.X, index + 1);
        }

        private Point MoveRight(int steps)
        {
            var index = LastEndPoint.X + 1;
            var lastIndex = LastEndPoint.X + steps + 1;
            for (; index < lastIndex; index++)
            {
                AddCleanedPoint(index, LastEndPoint.Y);
            }
            return new Point(index - 1, LastEndPoint.Y);
        }

        private Point MoveLeft(int steps)
        {
            var index = LastEndPoint.X - 1;
            var lastIndex = LastEndPoint.X - steps - 1;
            for (; index > lastIndex; index--)
            {
                AddCleanedPoint(index, LastEndPoint.Y);
            }

            return new Point(index + 1, LastEndPoint.Y);
        }

        private void AddCleanedPoint(int x, int y)
        {
            var cleaned = new Point(x, y);
            _uniquePlacesCleaned.Add(cleaned);
        }
    }
}