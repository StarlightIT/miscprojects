using System.Drawing;
using RobotCleaner.Lib;
using Xunit;

namespace RobotCleaner.Tests
{
    public class CombinedTests
    {
        public Lib.RobotCleaner _robotCleaner;

        public CombinedTests()
        {
            _robotCleaner = new Lib.RobotCleaner();
        }
        
        [Fact]
        public void NoMoveTest()
        {
            _robotCleaner.SetStartingCoordinates(0,0);
            Assert.Equal(1, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(0,0), _robotCleaner.LastEndPoint);
        }

        [Fact]
        public void CombinedPatternClockwiseTest()
        {
            _robotCleaner.SetStartingCoordinates(2,1);
            _robotCleaner.Move("E", 5);
            _robotCleaner.Move("N", 5);
            _robotCleaner.Move("W", 5);
            _robotCleaner.Move("S", 5);
            Assert.Equal(20, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,1), _robotCleaner.LastEndPoint);
        }
        
        [Fact]
        public void CombinedPatternCounterClockwiseTest()
        {
            _robotCleaner.SetStartingCoordinates(2,1);
            _robotCleaner.Move("W", 5);
            _robotCleaner.Move("S", 5);
            _robotCleaner.Move("E", 5);
            _robotCleaner.Move("N", 5);
            Assert.Equal(20, _robotCleaner.GetUniquePlacesCleaned());
            Assert.Equal(new Point(2,1), _robotCleaner.LastEndPoint);
        }
    }
}