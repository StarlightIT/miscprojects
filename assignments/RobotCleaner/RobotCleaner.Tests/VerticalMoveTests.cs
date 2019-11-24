using System.Drawing;
using RobotCleaner.Lib;
using Xunit;

namespace RobotCleaner.Tests
{
    /**
     * Integration tests that move the robot vertically and verify important parameters.
     */
    public class VerticalMoveTests
    {
        public Lib.RobotCleaner _robotCleaner;

        public VerticalMoveTests()
        {
            _robotCleaner = new Lib.RobotCleaner();
            _robotCleaner.SetStartingCoordinates(2,1);
        }

        [Fact]
        public void MoveUpTwoPlaces()
        {
            _robotCleaner.Move("N", 1);
            Assert.Equal(2, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,2 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveUpThreePlaces()
        {
            _robotCleaner.Move("N", 1);
            _robotCleaner.Move("N", 1);
            Assert.Equal(3, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2, 3), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveUpFivePlaces()
        {
            _robotCleaner.Move("N", 2);
            _robotCleaner.Move("N", 2);
            Assert.Equal(5, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,5 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveDownTwoPlaces()
        {
            _robotCleaner.Move("S", 1);
            Assert.Equal(2, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,0 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveDownThreePlaces()
        {
            _robotCleaner.Move("S", 1);
            _robotCleaner.Move("S", 1);
            Assert.Equal(3, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,-1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveDownFivePlaces()
        {
            _robotCleaner.Move("S", 2);
            _robotCleaner.Move("S", 2);
            Assert.Equal(5, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,-3 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveUpFirstTwoPlaces()
        {
            _robotCleaner.Move("N", 1);
            _robotCleaner.Move("S", 1);
            Assert.Equal(2, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveDownFirstTwoPlaces()
        {
            _robotCleaner.Move("S", 1);
            _robotCleaner.Move("N", 1);
            Assert.Equal(2, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveUpFirstFourPlaces()
        {
            _robotCleaner.Move("N", 2);
            _robotCleaner.Move("S", 3);
            Assert.Equal(4, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,0 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveDownFirstFourPlaces()
        {
            _robotCleaner.Move("S", 2);
            _robotCleaner.Move("N", 3);
            Assert.Equal(4, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,2 ), _robotCleaner.LastEndPoint);
        }
    }
}