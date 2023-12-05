using System.Collections.Generic;
using UnityEngine;
using System;

public class RedlightGreenlightBotSpawner : MonoBehaviour
{
    [SerializeField] private GameObject botPrefab;
    [SerializeField] private Transform[] botPoints;

    private void Start()
    {
        SpawnBots();
    }

    private void SpawnBots()
    {
        List<GameObject> bots = new List<GameObject>();
        foreach (Transform botPoint in botPoints)
        {
            bots.Add(Instantiate(botPrefab, botPoint.localPosition, botPoint.rotation));
        }

        Shuffle(bots);
    }

    private void Shuffle(List<GameObject> bots)
    {
        System.Random random = new System.Random();
        bots.Sort((x, y) => random.Next(-1, 2));
    }
}
