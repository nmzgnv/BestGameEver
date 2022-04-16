namespace Map
{
    public class RoomConfig
    {
        public int Height { get; private set; }

        public int Width { get; private set; }

        public int DestroyableObjectsCount { get; private set; }

        public int EnemySpawnersCount { get; private set; }

        public int MaxRetryCount { get; private set; }

        public RoomConfig(int height, int width, int destroyableObjectCount, int enemySpawnersCount,
            int maxRetryCount = 3)
        {
            Height = height;
            Width = width;
            DestroyableObjectsCount = destroyableObjectCount;
            EnemySpawnersCount = enemySpawnersCount;
            MaxRetryCount = maxRetryCount;
        }
    }
}