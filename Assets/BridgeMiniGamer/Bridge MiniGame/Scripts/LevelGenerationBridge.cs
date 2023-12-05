using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityStandardAssets.Water;

public class LevelGenerationBridge : MonoBehaviour
{
    [Header("Platforms")]
    private int _waterValueSpawn;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _waterPrefabs;
    private List<GameObject> _spawnedWaterPlatform = new List<GameObject>();

    [Header("Cliff/Environment Settings")]
    [SerializeField] private GameObject _cliff;
    [SerializeField] private GameObject[] _environment;
    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private AnimationCurve spawnIncreaseObstacle;
    private float chanceToSpawnObstacle;

    [Header("Bridge Settings")]
    [SerializeField] private GameObject[] _startBrigdes;
    [SerializeField] private GameObject[] _middleBrigdes;

    [Header("Wood Settings")]
    [SerializeField] private GameObject _woodPrefab;
    [SerializeField] private AnimationCurve _woodsToSpawn;
    [SerializeField] private int countOfAdditionalWoods;
    private int _countWoodsSpawn;

    [Header("Coin Settings")]
    public AnimationCurve spawnCoinsSpawn;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private int coinsMustSpawn;

    [Header("Length Settings")]
    [SerializeField] private float _platformLength = 5.95f;
    [SerializeField] private float _startBridgeLenght = 4.57f;
    [SerializeField] private float _middleBridgeLenght = 1.15f;
    [SerializeField] private float _environmentLenght = 28f;

    private List<GameObject> pointWoods = new List<GameObject>();
    int _spawnNumber = 0;
    private float _spawnPos = 0;
    private float _environmentPos;

    private void Start()
    {
        _spawnPos = _spawnPoint.transform.position.z;
        _environmentPos = -_environmentLenght;
    }

    private void LateUpdate()
    {
        if (PlayerBridgeController.Instance.transform.position.z > _spawnPos - _platformLength * 24)
            SpawnNewPath();

        if (PlayerBridgeController.Instance.transform.position.z > _environmentPos - _environmentLenght * 2f)
            SpawnEnvironment();
    }

    public void SpawnNewPath()
    {
        coinsMustSpawn = Mathf.FloorToInt(spawnCoinsSpawn.Evaluate(_spawnNumber));
        _countWoodsSpawn = Mathf.FloorToInt(_woodsToSpawn.Evaluate(_spawnNumber));
        chanceToSpawnObstacle = Mathf.FloorToInt(spawnIncreaseObstacle.Evaluate(_spawnNumber));
        _waterValueSpawn = _countWoodsSpawn + countOfAdditionalWoods + 3;

        _spawnNumber++;

        if (_spawnNumber <= 1) SpawnStartedPlatform();

        NewWaterTile().GetComponentInChildren<WaterTile>().AddComponent<StartPath>().Initialized(_spawnNumber, _countWoodsSpawn);

        for (int i = 0; i < _waterValueSpawn; i++)
            _spawnedWaterPlatform.Add(NewWaterTile());

        SpawnNewWoods();
        SpawnObstacles();
        SpawnNewCoin();

        _spawnedWaterPlatform.Clear();
        pointWoods.Clear();

        NewWaterTile();
        NewCliff();
        SpawnBridge();
        NewCliff();
        _spawnPos -= _platformLength + 6f;
    }

    Vector3 GetRandomPosition(Vector3 platformPosition)
    {
        Vector3[] possiblePositions = new Vector3[]
        {
        platformPosition,
        platformPosition + new Vector3(-4.25f, 0.5f, 0),
        platformPosition + new Vector3(4.25f, 0.5f, 0),
        platformPosition + new Vector3(2.25f, 0.5f, 0),
        platformPosition + new Vector3(-2.25f, 0.5f, 0)
        };

        return possiblePositions[UnityEngine.Random.Range(0, possiblePositions.Length)];
    }

    #region Spawn Main
    void SpawnNewWoods()
    {
        if (_spawnedWaterPlatform.Count == 0) return;

        for (int i = 0; i < _countWoodsSpawn; i++)
            NewWood(_spawnedWaterPlatform);

            for (int i = 0; i < countOfAdditionalWoods; i++)
                NewWood(_spawnedWaterPlatform);
    }
    void SpawnNewCoin()
    {
        if(coinsMustSpawn > _spawnedWaterPlatform.Count)
        {
            Coin(_spawnedWaterPlatform.Count);

            for(int i = 0; i < coinsMustSpawn - _spawnedWaterPlatform.Count; i++)
            {
                int objectIndexForCoin = UnityEngine.Random.Range(0, pointWoods.Count);
                Vector3 coinPosition = GetRandomPosition(pointWoods[objectIndexForCoin].transform.position);

                foreach (var obstacle in obstaclePos)
                {
                    if (Vector3.Distance(coinPosition, obstacle) < 2)
                    {
                        coinPosition = -obstacle;
                    }
                }

                Instantiate(_coinPrefab, coinPosition, Quaternion.identity);

                pointWoods.RemoveAt(objectIndexForCoin);
            }
        } 
        else
        {
            Coin(coinsMustSpawn);
        }
    }

    private List<Vector3> obstaclePos = new List<Vector3>();
    void SpawnObstacles()
    {
        foreach (GameObject point in pointWoods)
        {
            int spawnChance = UnityEngine.Random.Range(0, 100);

            if (spawnChance < chanceToSpawnObstacle)
            {
                int random = UnityEngine.Random.Range(0, 5);

                Vector3 right = new Vector3(point.transform.position.x + UnityEngine.Random.Range(2.5f, 3.5f), point.transform.position.y - 0.5f, point.transform.position.z);
                Vector3 left = new Vector3(point.transform.position.x - UnityEngine.Random.Range(2.5f, 3.5f), point.transform.position.y - 0.5f, point.transform.position.z);
                Vector3 forward = new Vector3(point.transform.position.x, point.transform.position.y - 0.5f, point.transform.position.z + UnityEngine.Random.Range(2.5f, 3.5f));
                Vector3 behind = new Vector3(point.transform.position.x, point.transform.position.y - 0.5f, point.transform.position.z - UnityEngine.Random.Range(3f, 4.5f));

                switch (random)
                {
                    case 0: NewObstacle(right); break;
                    case 1: NewObstacle(left); break;
                    case 2: NewObstacle(forward); break;
                    case 3: NewObstacle(behind); break;
                    case 4:
                        NewObstacle(right);
                        NewObstacle(left);
                        break;
                }
            }
        }
    }

    void SpawnBridge()
    {
        int enterenceBridgeIndex = UnityEngine.Random.Range(0, _startBrigdes.Length);
        Instantiate(_startBrigdes[enterenceBridgeIndex], transform.forward * (_spawnPos - 10.22f) + new Vector3(0, -3f, 0), _startBrigdes[enterenceBridgeIndex].transform.rotation)
                    .AddComponent<BridgeActivator>();
        _spawnPos += 4.57f - 10.22f;

        for (int i = 0; i < _countWoodsSpawn; i++)
        {
            int middleBridgeIndex = UnityEngine.Random.Range(0, _middleBrigdes.Length);
            var bridge = Instantiate(_middleBrigdes[middleBridgeIndex], transform.forward * _spawnPos + new Vector3(0, 0.88f, 0), _middleBrigdes[middleBridgeIndex].transform.rotation);
            bridge.GetComponentInChildren<MeshRenderer>().enabled = false;
            _spawnPos += _middleBridgeLenght;
        }

        _spawnPos += 3.47f;

        var newStartedPlatform = Instantiate(_startBrigdes[enterenceBridgeIndex], transform.forward * _spawnPos + new Vector3(0, -3f, 0), _startBrigdes[enterenceBridgeIndex].transform.rotation * new Quaternion(0, -1, 0, 0))
                                 .AddComponent<StartPath>();
        newStartedPlatform.Initialized(_spawnNumber, _countWoodsSpawn);
        _spawnPos += _startBridgeLenght;
    }
    void Coin(int coinsMustSpawn)
    {
        for (int i = 0; i < coinsMustSpawn; i++)
        {
            int objectIndexForCoin = UnityEngine.Random.Range(0, _spawnedWaterPlatform.Count);
            Vector3 coinPosition = GetRandomPosition(_spawnedWaterPlatform[objectIndexForCoin].transform.position);

            foreach (var obstacle in obstaclePos)
            {
                if(Vector3.Distance(coinPosition, obstacle) < 2)
                {
                    coinPosition = -obstacle;
                }
            }
            Instantiate(_coinPrefab, coinPosition, Quaternion.identity);
            _spawnedWaterPlatform.RemoveAt(objectIndexForCoin);
        }
    }
    #endregion

    #region Spawn Other 
    void SpawnEnvironment()
    {
        Instantiate(_environment[UnityEngine.Random.Range(0, _environment.Length)], transform.forward * _environmentPos, Quaternion.identity);
        _environmentPos += _environmentLenght;
    }
    void SpawnStartedPlatform()
    {
        _spawnPos -= _platformLength;
        for (int i = 0; i < 3; i++)
            NewWaterTile();
    }
    #endregion

    #region Objects
    GameObject NewWood(List<GameObject> spawnPos)
    {
        int objectIndex = UnityEngine.Random.Range(0, spawnPos.Count);
        GameObject wood = Instantiate(_woodPrefab, GetRandomPosition(spawnPos[objectIndex].transform.position), _woodPrefab.transform.rotation);
        pointWoods.Add(wood);
        spawnPos.RemoveAt(objectIndex);
        return wood;
    }
    GameObject NewWaterTile()
    {
        GameObject newPlatform = Instantiate(_waterPrefabs, transform.forward * _spawnPos, Quaternion.identity);
        _spawnPos += _platformLength;
        return newPlatform;
    }
    GameObject NewCliff()
    {
        GameObject first = Instantiate(_cliff, transform.forward * (_spawnPos - 2.7f), Quaternion.identity);
        first.transform.position = new Vector3(first.transform.position.x + 5.43f, first.transform.position.y - 7.39f, first.transform.position.z);
        _spawnPos += _platformLength + 7.1f;

        return first;
    }
    GameObject NewObstacle(Vector3 position)
    {
        GameObject additionalSecond = Instantiate(_obstacles[UnityEngine.Random.Range(0, _obstacles.Length)]);
        additionalSecond.transform.position = position;
        obstaclePos.Add(position);
        return additionalSecond;
    }
    #endregion
}