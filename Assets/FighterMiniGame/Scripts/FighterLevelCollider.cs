using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterLevelCollider : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Block is trigger");
            blockPrefab.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
