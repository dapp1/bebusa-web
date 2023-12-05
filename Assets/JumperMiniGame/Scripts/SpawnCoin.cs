using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    public  List<GameObject> levelCoins = new List<GameObject>();
    private int _spawnChance;

    private void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        foreach (GameObject obj in levelCoins)
        {
            int randomChance = Random.Range(0, 10);
            if (_spawnChance > randomChance)
            {
                obj.SetActive(true);
            } else
            {
                Destroy(obj);
            }
        }
    }

    public void Initiazided(int spawnChance)
    {
        _spawnChance = spawnChance;
    }
}
