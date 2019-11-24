using System.Drawing;
using RobotCleaner.Lib;
using Xunit;

namespace RobotCleaner.Tests
{
    /**
     * Integration tests that move the robot horizontally and verify important parameters.
     */
    public class HorizontalMoveTests
    {
        public Lib.RobotCleaner _robotCleaner;

        public HorizontalMoveTests()
        {
            _robotCleaner = new Lib.RobotCleaner();
            _robotCleaner.SetStartingCoordinates(2,1);
        }
        
        [Fact]
        public void MoveRightTwoPlaces()
        {
            _robotCleaner.Move("E", 1);
            Assert.Equal(2, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(3,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveRightThreePlaces()
        {
            _robotCleaner.Move("E", 1);
            _robotCleaner.Move("E", 1);
            Assert.Equal(3, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(4,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveRightFivePlaces()
        {
            _robotCleaner.Move("E", 2);
            _robotCleaner.Move("E", 2);
            Assert.Equal(5, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(6,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveLeftTwoPlaces()
        {
            _robotCleaner.Move("W", 1);
            Assert.Equal(2, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(1,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveLeftThreePlaces()
        {
            _robotCleaner.Move("W", 1);
            _robotCleaner.Move("W", 1);
            Assert.Equal(3, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(0,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveLeftFivePlaces()
        {
            _robotCleaner.Move("W", 2);
            _robotCleaner.Move("W", 2);
            Assert.Equal(5, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(-2, _robotCleaner.LastEndPoint.X);
            Assert.Equal(1, _robotCleaner.LastEndPoint.Y);
        }

        [Fact]
        public void MoveRightFirstTwoPlaces()
        {
            _robotCleaner.Move("E", 1);
            _robotCleaner.Move("W", 1);
            Assert.Equal(2, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MOveLeftFirstTwoPlaces()
        {
            _robotCleaner.Move("W", 1);
            _robotCleaner.Move("E", 1);
            Assert.Equal(2, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(2, _robotCleaner.LastEndPoint.X);
            Assert.Equal(1, _robotCleaner.LastEndPoint.Y);
        }
        
        [Fact]
        public void MoveRightFirstFourPlaces()
        {
            _robotCleaner.Move("E", 2);
            _robotCleaner.Move("W", 3);
            Assert.Equal(4, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(1,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveLeftFirstFourPlaces()
        {
            _robotCleaner.Move("W", 2);
            _robotCleaner.Move("E", 3);
            Assert.Equal(4, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(3,1 ), _robotCleaner.LastEndPoint);
        }
    }
}