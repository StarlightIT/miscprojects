namespace RobotCleaner.Lib
{
    public interface IRobotCleaner
    {
        void SetStartingCoordinates(int x, int y);
        void Move(string direction, int steps);
        int GetUniquePlacesCleaned();
    }
}