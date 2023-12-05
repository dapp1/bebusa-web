using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomBonus : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnList = new List<GameObject>();
    [Range(0, 1)]
    public float ChanseOfStaying = 0.5f;
    void Start()
    {
        if (Random.value > ChanseOfStaying)
        {
            GameObject bonus = Instantiate(spawnList[Random.Range(0, spawnList.Count)], transform.position, Quaternion.identity);
            bonus.transform.SetParent(gameObject.transform);
        }
    }
}
