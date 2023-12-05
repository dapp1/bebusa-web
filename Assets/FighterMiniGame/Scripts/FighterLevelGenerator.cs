using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterLevelGenerator : MonoBehaviour
{
    public GameObject[] levelSegments; // Массив сегментов уровня
    public Transform playerTransform; // Позиция игрока, для определения когда спавнить новый сегмент уровня

    public Vector3 levelSpawnPosition = new Vector3(-38.5f, 0, 0); // Позиция спавна первого сегмента
    private float segmentOffset = 40.95f; // Расстояние между сегментами

    private float segmentLength; // Длина одного сегмента уровня
    private int currentSegmentIndex = 0; // Индекс текущего сегмента

    private List<GameObject> spawnedSegments = new List<GameObject>(); // Список спавнутых сегментов уровня
    private bool spawningEnabled = false; // Флаг, определяющий, разрешено ли спавнить новые сегменты
    public static FighterLevelGenerator Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Убедитесь, что у вашего сегмента уровня есть MeshRenderer и он находится на том же уровне в иерархии объектов, что и сам сегмент.
        // Если MeshRenderer находится в дочернем объекте, вам нужно будет изменить этот код, чтобы он мог найти его.
        if (levelSegments.Length > 0 && levelSegments[0].GetComponent<MeshRenderer>() != null)
        {
            segmentLength = levelSegments[0].GetComponent<MeshRenderer>().bounds.size.x;
        }
        else
        {
            Debug.LogError("Level segment does not have a MeshRenderer component or levelSegments array is empty. Cannot determine segment length.");
        }
        SpawnInitialSegments(); // Спавним первые три сегмента уровня
    }
    private void LateUpdate()
    {
        if (!spawningEnabled)
            return;

        // Проверяем, если игрок достиг конца текущего сегмента
        if (playerTransform.position.x <= levelSpawnPosition.x)
        {
            DestroySegment();

            SpawnSegment();

            levelSpawnPosition.x -= segmentLength; // Обновляем координату спавна для следующего сегмента
        }
    }

    public void SpawnSegment()
    {
        if (currentSegmentIndex >= levelSegments.Length)
        {
            currentSegmentIndex = 0; // Если достигнут конец массива сегментов, начинаем сначала
        }

        // Destroy the oldest segment if there are more than a certain number
        if (spawnedSegments.Count >= 3)
        {
            DestroySegment();
        }

        GameObject newSegment = Instantiate(levelSegments[currentSegmentIndex], levelSpawnPosition, Quaternion.identity);
        spawnedSegments.Add(newSegment);

        // Move to the next segment
        currentSegmentIndex++;

        // Update the spawn position for the next segment
        levelSpawnPosition.x -= segmentOffset;
    }

    private void DestroySegment()
    {
        if (spawnedSegments.Count > 0)
        {
            GameObject segmentToRemove = spawnedSegments[0];
            spawnedSegments.RemoveAt(0);
            Destroy(segmentToRemove);
        }
    }

    public void StartSpawning()
    {
        spawningEnabled = true;
        SpawnInitialSegments();
    }

    public void StopSpawning()
    {
        spawningEnabled = false;
    }

    public void SpawnInitialSegments()
    {
        for (int i = 0; i < 1; i++) // Спавним первые три сегмента уровня
        {
            SpawnSegment();
            
        }
    }
}