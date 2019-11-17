namespace RobotCleaner.Lib
{
    public interface IRobotCleaner
    {
        void SetNumberOfCommands(int commands);
        void SetStartingCoordinates(int x, int y);
        void Move(string direction, int steps);

        int GetUniquePlacesCleaned();
    }
}