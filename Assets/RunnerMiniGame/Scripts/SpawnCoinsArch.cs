using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoinsArch : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float coinCount;
    [SerializeField] private float spawnDistance;
    [SerializeField] private float spawnHight;

    private float y;
    private float x;
    private float step;
    private int s = 0;


    void Start()
    {
        coinCount++;
        step = spawnDistance / coinCount;
        x = -(coinCount / 2 * step);
        for (float i = -coinCount / 2; i < coinCount / 2; i++)
        {
            y = Mathf.Max((-1 / 4f * (x * x)) + spawnHight, transform.position.y);
            x += step;
            if (s != 0)
            {
                GameObject coin = Instantiate(coinPrefab, new Vector3(transform.position.x, y, transform.position.z + x), Quaternion.identity);
                coin.transform.SetParent(gameObject.transform);
            }
            s++;
        }
    }
}
