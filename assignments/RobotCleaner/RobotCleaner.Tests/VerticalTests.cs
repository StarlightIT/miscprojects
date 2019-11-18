using System.Drawing;
using RobotCleaner.Lib;
using Xunit;

namespace RobotCleaner.Tests
{
    public class VerticalTests
    {
        public Lib.RobotCleaner _robotCleaner;

        public VerticalTests()
        {
            _robotCleaner = new Lib.RobotCleaner();
            _robotCleaner.SetStartingCoordinates(2,1);
        }

        [Fact]
        public void MoveUpTwoPlacesTest()
        {
            _robotCleaner.Move("N", 1);
            Assert.Equal(2, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,2 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveUpThreePlacesTest()
        {
            _robotCleaner.Move("N", 1);
            _robotCleaner.Move("N", 1);
            Assert.Equal(3, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2, 3), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveUpFivePlacesTest()
        {
            _robotCleaner.Move("N", 2);
            _robotCleaner.Move("N", 2);
            Assert.Equal(5, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,5 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveDownTwoPlacesTest()
        {
            _robotCleaner.Move("S", 1);
            Assert.Equal(2, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,0 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveDownThreePlacesTest()
        {
            _robotCleaner.Move("S", 1);
            _robotCleaner.Move("S", 1);
            Assert.Equal(3, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,-1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void MoveDownFivePlacesTest()
        {
            _robotCleaner.Move("S", 2);
            _robotCleaner.Move("S", 2);
            Assert.Equal(5, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,-3 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void UpFirstCombinedTwoPlacesTest()
        {
            _robotCleaner.Move("N", 1);
            _robotCleaner.Move("S", 1);
            Assert.Equal(2, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void DownFirstCombinedTwoPlacesTest()
        {
            _robotCleaner.Move("S", 1);
            _robotCleaner.Move("N", 1);
            Assert.Equal(2, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,1 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void UpFirstCombinedTFourPlacesTest()
        {
            _robotCleaner.Move("N", 2);
            _robotCleaner.Move("S", 3);
            Assert.Equal(4, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,0 ), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void DownFirstCombinedFourPlacesTest()
        {
            _robotCleaner.Move("S", 2);
            _robotCleaner.Move("N", 3);
            Assert.Equal(4, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,2 ), _robotCleaner.LastEndPoint);
        }
    }
}