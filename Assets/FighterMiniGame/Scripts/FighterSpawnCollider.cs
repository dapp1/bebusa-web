using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterSpawnCollider : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            blockPrefab.SetActive(false);
            FighterLevelGenerator.Instance.SpawnSegment();
           
        }
    }
}
