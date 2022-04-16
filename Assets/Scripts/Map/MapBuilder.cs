using System.Collections.Generic;
using Map;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject enemySpawnerPrefab;

    [SerializeField]
    private GameObject destroyableObjectPrefab;

    [SerializeField]
    private GameObject wallPrefab;

    [SerializeField]
    private float cellSize;

    [SerializeField]
    private float enemySpawnerProbability;

    [SerializeField]
    private float destroyableObjectsProbability;

    [SerializeField]
    private int height;

    [SerializeField]
    private int width;

    private Dictionary<CellType, GameObject> _gameObjectByCellType;
    private GameObject _map;

    private int GetCountByProbability(int square, float probability)
    {
        return (int) (square * probability);
    }

    private void Start()
    {
        _gameObjectByCellType = new Dictionary<CellType, GameObject>()
        {
            [CellType.Player] = playerPrefab,
            [CellType.Destroyable] = destroyableObjectPrefab,
            [CellType.EnemySpawner] = enemySpawnerPrefab,
            [CellType.Wall] = wallPrefab,
        };
        _map = Instantiate(new GameObject("Map"), Vector3.zero, Quaternion.identity);
        BuildNewMap();
    }

    private void DestroyMap()
    {
        foreach (Transform child in _map.transform)
            Destroy(child.gameObject);
    }

    public void BuildNewMap()
    {
        DestroyMap();
        var square = height * width;
        var room = RoomGenerator.GenerateRoom(
            new RoomConfig(height, width, GetCountByProbability(square, destroyableObjectsProbability),
                GetCountByProbability(square, enemySpawnerProbability))
        );
        BuildMap(room);
    }

    private void BuildMap(CellType[,] map)
    {
        var enemySpawners = new List<EnemySpawner>();
        Player player = null;

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var cellType = map[x, y];
                if (cellType == CellType.Empty) continue;

                var createdObject = Instantiate(_gameObjectByCellType[cellType],
                    new Vector3(x * cellSize, y * cellSize, 0),
                    Quaternion.identity, _map.transform);

                if (cellType == CellType.Player)
                    player = createdObject.GetComponent<Player>();
                else if (cellType == CellType.EnemySpawner)
                {
                    var spawner = createdObject.GetComponent<EnemySpawner>();
                    if (spawner != null)
                        enemySpawners.Add(spawner);
                    else
                        Debug.LogWarning("Spawned enemy without EnemySpawner component");
                }
            }
        }

        foreach (var spawner in enemySpawners)
        {
            spawner.SetEnemyTarget(player.BulletTarget);
            spawner.StartSpawn();
        }
    }
}