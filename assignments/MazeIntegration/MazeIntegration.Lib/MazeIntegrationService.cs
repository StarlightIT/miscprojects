using AtlasCopco.Integration.Maze;
using MazeIntegration.Lib.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MazeIntegration.Lib
{
    public class MazeIntegrationService : IMazeIntegration
    {
        private readonly MazeIntegrationOptions _options;
        private readonly InjuryThresholds _injuryThreasholds;
        private readonly RoomDescription _roomDescriptions;
        private ILogger<MazeIntegrationService> _logger;
        private Room[,] _room;
        private Room _entranceRoom;
        private Room _treasureRoom;
        private bool _adventureEnd = false;

        public MazeIntegrationService(IOptions<MazeIntegrationOptions> options, ILogger<MazeIntegrationService> logger)
        {
            _options = options.Value;
            _injuryThreasholds = _options.InjuryThresholds;
            _roomDescriptions = _options.RoomDescriptions;
            _logger = logger;
        }

        public void BuildMaze(int size)
        {
            GenerateRandomMaze(size);
            GenerateEntrance(size);
            GenerateTreasure(size);
            _adventureEnd = false;
        }

        private void GenerateRandomMaze(int size)
        {
            var random = new Random();
            _room = new Room[size, size];
            int id = 0;
            for (int i = 0; i<size; i++)
            {
                for (int j = 0; j<size; j++)
                {
                    RoomType roomType = (RoomType)random.Next(0, 4);
                    var room = new Room
                    {
                        Id = id,
                        X = i,
                        Y = j,
                        Type = roomType,
                        Description = _roomDescriptions.GetDescription(roomType)
                    };
                    _room[i, j] = room;
                    id++;
                }
            }
        }

        private void GenerateEntrance(int size)
        {
            var random = new Random();
            int row = random.Next(0, size);
            int col;

            if (row == 0 || row == (size-1))
            {
                col = random.Next(0, size);
            }
            else
            {
                int seed = random.Next(0, 2);
                col = (seed == 0) ? 0 : size - 1;
            }
            _room[row, col].Type = RoomType.Entrance;
            _entranceRoom = _room[row, col];
        }

        private void GenerateTreasure(int size)
        {
            var random = new Random();
            int row = GenerateRandomCoordinateDifferentFrom(_entranceRoom.X, size);
            int col = GenerateRandomCoordinateDifferentFrom(_entranceRoom.Y, size);
            _room[row, col].Type = RoomType.Treasure;
            _treasureRoom = _room[row, col];
        }

        private int GenerateRandomCoordinateDifferentFrom(int coordinate, int maxSize)
        {
            var random = new Random();
            int c;
            do
            {
                c = random.Next(0, maxSize);
            } while (c == coordinate);
            return c;
        }

        public bool CausesInjury(int roomId)
        {
            if (_adventureEnd)
            {
                return true;
            }

            var room = FindRoomById(roomId);
            var random = new Random();
            int chance = random.Next(1, 101);

            if (room.Type == RoomType.Marsh && chance <= _injuryThreasholds.MarshInjuryThreshold)
            {
                room.Description += " " + _roomDescriptions.MarshInjury;
                _adventureEnd = true;
                return true;
            }
                
            if (room.Type == RoomType.Desert && chance <= _injuryThreasholds.DesertInjuryThreshold)
            {
                room.Description += " " + _roomDescriptions.DesertInjury;
                _adventureEnd = true;
                return true;
            }

            return false;
        }

        public string GetDescription(int roomId)
        {
            var room = FindRoomById(roomId);
            return room.Description;
        }

        public int GetEntranceRoom()
        {
            return _entranceRoom.Id;
        }

        public int? GetRoom(int roomId, char direction)
        {
            if (_adventureEnd)
            {
                return null;
            }

            var room = FindRoomById(roomId);
            int height = _room.GetLength(0);
            int width = _room.GetLength(1);
            int nextX = room.X, nextY = room.Y;

            switch (direction)
            {
                case 'N':
                    nextY++;
                    break;
                case 'S':
                    nextY--;
                    break;
                case 'E':
                    nextX++;
                    break;
                case 'W':
                    nextX--;
                    break;
                default:
                    return null;
            }

            if (nextX < 0 || nextX == height || nextY < 0 || nextY == width)
            {
                return null;
            }

            return _room[nextX, nextY].Id;
        }

        public bool HasTreasure(int roomId)
        {
            return (roomId == _treasureRoom.Id);
        }

        private Room FindRoomById(int id)
        {
            for(int i = 0; i<_room.GetLength(0); i++)
            {
                for(int j=0; j<_room.GetLength(1); j++)
                {
                    var room = _room[i, j];
                    if (room.Id == id)
                    {
                        return room;
                    }
                }
            }

            return null;
        }
    }
}
