using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MazeIntegration.Lib.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public RoomType Type { get; set; }
        public string Description { get; set; }
    }
}
