using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    public Transform player;

    public void GoToSpawn()
    {
        player.transform.position = spawnPoint.transform.position;
    }
}
