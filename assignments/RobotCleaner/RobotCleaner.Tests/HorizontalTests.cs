using System.Drawing;
using RobotCleaner.Lib;
using Xunit;

namespace RobotCleaner.Tests
{
    public class HorizontalTests
    {
        public Lib.RobotCleaner _robotCleaner;

        public HorizontalTests()
        {
            _robotCleaner = new Lib.RobotCleaner();
            _robotCleaner.SetStartingCoordinates(2,1);
        }
        
        [Fact]
        public void MoveRightTwoPlacesTest()
        {
            _robotCleaner.SetNumberOfCommands(1);
            _robotCleaner.Move("E", 1);
            Assert.Equal(2, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(3,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveRightThreePlacesTest()
        {
            _robotCleaner.SetNumberOfCommands(2);
            _robotCleaner.Move("E", 1);
            _robotCleaner.Move("E", 1);
            Assert.Equal(3, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(4,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveRightFivePlacesTest()
        {
            _robotCleaner.SetNumberOfCommands(2);
            _robotCleaner.Move("E", 2);
            _robotCleaner.Move("E", 2);
            Assert.Equal(5, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(6,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveLeftTwoPlacesTest()
        {
            _robotCleaner.SetNumberOfCommands(1);
            _robotCleaner.Move("W", 1);
            Assert.Equal(2, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(1,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveLeftThreePlacesTest()
        {
            _robotCleaner.SetNumberOfCommands(2);
            _robotCleaner.Move("W", 1);
            _robotCleaner.Move("W", 1);
            Assert.Equal(3, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(0,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveLeftFivePlacesTest()
        {
            _robotCleaner.SetNumberOfCommands(2);
            _robotCleaner.Move("W", 2);
            _robotCleaner.Move("W", 2);
            Assert.Equal(5, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(-2, _robotCleaner.LastEndPoint.X);
            Assert.Equal(1, _robotCleaner.LastEndPoint.Y);
        }

        [Fact]
        public void RightFirstCombinedTwoPlacesTest()
        {
            _robotCleaner.SetNumberOfCommands(2);
            _robotCleaner.Move("E", 1);
            _robotCleaner.Move("W", 1);
            Assert.Equal(2, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void LeftFirstCombinedTwoPlacesTest()
        {
            _robotCleaner.SetNumberOfCommands(2);
            _robotCleaner.Move("W", 1);
            _robotCleaner.Move("E", 1);
            Assert.Equal(2, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(2, _robotCleaner.LastEndPoint.X);
            Assert.Equal(1, _robotCleaner.LastEndPoint.Y);
        }
        
        [Fact]
        public void RightFirstCombinedTFourPlacesTest()
        {
            _robotCleaner.SetNumberOfCommands(2);
            _robotCleaner.Move("E", 2);
            _robotCleaner.Move("W", 3);
            Assert.Equal(4, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(1,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void LeftFirstCombinedFourPlacesTest()
        {
            _robotCleaner.SetNumberOfCommands(2);
            _robotCleaner.Move("W", 2);
            _robotCleaner.Move("E", 3);
            Assert.Equal(4, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(3,1 ), _robotCleaner.LastEndPoint);
        }
    }
}