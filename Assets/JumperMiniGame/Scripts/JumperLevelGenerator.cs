using UnityEngine;

public class JumperLevelGenerator : MonoBehaviour
{
    [SerializeField] private int level = 0;
    public GameObject[] levelPrefabs;
    public GameObject[] startLevelPrefabs;
    public Vector3[] spawnPositions = new Vector3[]
    {
        new Vector3(0, 0, 0),
        new Vector3(10, 0, 30),
        new Vector3(40, 0, 20),
        new Vector3(30, 0, -10),
        new Vector3(0, 0, 0),
    };
    public Quaternion[] spawnRotations = new Quaternion[]
    {
        Quaternion.Euler(0, 0, 0),
        Quaternion.Euler(0, 90, 0),
        Quaternion.Euler(0, 180, 0),
        Quaternion.Euler(0, 270, 0),
        Quaternion.Euler(0, 0, 0),
    };

    private int currentLevelIndex = 0;
    private float currentYOffset = -30f;
    private bool isFirstSpawn = true;

    [SerializeField] private AnimationCurve _coinSpawnChance;
    private int _spawnNumber;

    private void Start()
    {
        SpawnNextLevel();
        SpawnNextLevel();
    }
    private int spawnPositionIndex = 0; // new variable for spawn position index
    public void SpawnNextLevel()
    {
        _spawnNumber++;

        Vector3 currentPosition = spawnPositions[spawnPositionIndex]; // use the new index here
        currentPosition.y = currentYOffset;

        Quaternion currentRotation = spawnRotations[spawnPositionIndex]; // and here

        GameObject levelPrefab = GetNextLevelPrefab();
        GameObject newLevel = Instantiate(levelPrefab, currentPosition, currentRotation);

        JumperLevel jumperLevel = newLevel.GetComponent<JumperLevel>();
        SpawnCoin spawnCoin = newLevel.GetComponent<SpawnCoin>();

        spawnCoin.Initiazided(Mathf.FloorToInt(_coinSpawnChance.Evaluate(_spawnNumber)));

        currentYOffset += 30f;
        isFirstSpawn = false;

        spawnPositionIndex++; // increment the spawn position index each time you spawn a level
        if (spawnPositionIndex >= spawnPositions.Length) // if you've reached the end of the array, loop back to the start
        {
            spawnPositionIndex = 0;
        }
    }

    private GameObject GetNextLevelPrefab()
    {
        if (isFirstSpawn)
        {
            int randomIndex = Random.Range(0, startLevelPrefabs.Length);
            //  randomIndex = Mathf.Clamp(level, 0, startLevelPrefabs.Length - 1);
            currentLevelIndex++;
            return startLevelPrefabs[randomIndex];
        }
        else
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, levelPrefabs.Length);
            }
            while (randomIndex == currentLevelIndex);

            // randomIndex = level;

            currentLevelIndex = randomIndex;
            return levelPrefabs[randomIndex];
        }
    }

}
