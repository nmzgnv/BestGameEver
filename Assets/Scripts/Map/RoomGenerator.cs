using System;

namespace Map
{
    public static class RoomGenerator
    {
        private static void AddBorder(Room room)
        {
            var map = room.Map;
            for (var i = 0; i < room.Width; i++)
            {
                map[i, 0] = CellType.Wall;
                map[i, room.Height - 1] = CellType.Wall;
            }

            for (var i = 0; i < room.Height; i++)
            {
                map[0, i] = CellType.Wall;
                map[room.Width - 1, i] = CellType.Wall;
            }
        }

        private static void TryGenerateObject(CellType cellType, Room room, int count, int maxAttemptCount)
        {
            var random = new Random();
            for (var c = 0; c < count; c++)
            {
                for (var attempt = 0; attempt < maxAttemptCount; attempt++)
                {
                    var x = random.Next(1, room.Width - 1);
                    var y = random.Next(1, room.Height - 1);

                    if (room.IsCellEmpty(x, y))
                    {
                        room.Map[x, y] = cellType;
                        break;
                    }
                }
            }
        }

        public static CellType[,] GenerateRoom(RoomConfig config)
        {
            var room = new Room(config.Width, config.Height);
            var retry = config.MaxRetryCount;
            TryGenerateObject(CellType.Player, room, 1, int.MaxValue);
            TryGenerateObject(CellType.EnemySpawner, room, config.EnemySpawnersCount, retry);
            TryGenerateObject(CellType.Destroyable, room, config.DestroyableObjectsCount, retry);
            AddBorder(room);
            // TODO generate exit
            return room.Map;
        }
    }
}