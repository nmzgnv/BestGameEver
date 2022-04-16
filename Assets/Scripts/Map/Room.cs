using System;

namespace Map
{
    public class Room
    {
        public CellType[,] Map;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public CellType this[int x, int y]
        {
            get => Map[x, y];
            set
            {
                if (!IsCellEmpty(x, y))
                    throw new ArgumentException();
                Map[x, y] = value;
            }
        }

        public Room(int width, int height)
        {
            Map = new CellType[width, height];
            Width = width;
            Height = height;
        }

        public bool IsCellEmpty(int x, int y)
        {
            return Map[x, y] == CellType.Empty;
        }
    }
}