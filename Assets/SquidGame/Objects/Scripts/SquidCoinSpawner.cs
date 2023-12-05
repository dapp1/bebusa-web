using UnityEngine;
using System.Collections.Generic;

public class SquidCoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private bool isCustomScale = false;
    [SerializeField] private float customScale = 0;
    private void Start()
    {
        SpawnCoins();
    }

    private void SpawnCoins()
    { 

        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        for (int i = 0; i < availableSpawnPoints.Count; i++)
        {
            if (availableSpawnPoints.Count == 0) break;

            int spawnIndex = Random.Range(0, availableSpawnPoints.Count);
            Transform chosenSpawnPoint = availableSpawnPoints[spawnIndex];

            GameObject spawnedCoin = Instantiate(coinPrefab, chosenSpawnPoint.position, Quaternion.identity);

            spawnedCoin.transform.localScale = new Vector3(5, 5, 5);
            if (isCustomScale)
            {
                spawnedCoin.transform.localScale = new Vector3(customScale, customScale, customScale);
            }

            availableSpawnPoints.RemoveAt(spawnIndex);
        }
    }
}
