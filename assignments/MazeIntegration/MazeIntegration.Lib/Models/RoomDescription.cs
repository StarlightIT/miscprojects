using System;
using System.Collections.Generic;
using System.Text;

namespace MazeIntegration.Lib.Models
{
    public class RoomDescription
    {
        public string Forest { get; set; } = "Forest";
        public string Marsh { get; set; } = "Marsh";
        public string Desert { get; set; } = "Desert";
        public string Hills { get; set; } = "Hills";
        public string Entrance { get; set; } = "Entrance";
        public string Treasure { get; set; } = "Treasure";
        public string MarshInjury { get; set; } = "Player sunk!";
        public string DesertInjury { get; set; } = "Player dehydrated!";

        public string GetDescription(RoomType type)
        {
            switch (type)
            {
                case RoomType.Forest:
                    return Forest;
                case RoomType.Marsh:
                    return Marsh;
                case RoomType.Desert:
                    return Desert;
                case RoomType.Hills:
                    return Hills;
                case RoomType.Entrance:
                    return Entrance;
                case RoomType.Treasure:
                    return Treasure;
                default:
                    return "Invalid room type";
            }
        }
    }
}
