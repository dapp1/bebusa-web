using UnityEngine;
using System.Collections.Generic;

public class PlaneTopTrigger : MonoBehaviour
{
    [SerializeField] private Transform platform;

    const string playerTag = "Player";

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.CompareTag(playerTag))
        {
            obj.transform.parent = platform;
        }
        
    }

    private void OnTriggerExit(Collider obj)
    {
        if (obj.CompareTag(playerTag))
        {
            obj.transform.parent = null;
        }
    }
   
}
