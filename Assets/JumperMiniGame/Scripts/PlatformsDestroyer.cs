using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsDestroyer : MonoBehaviour
{
    private List<GameObject> platforms = new List<GameObject>();
    public GroundScript groundScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform") || other.CompareTag("PlaneMove"))
        {
            for (int i = 0; i < platforms.Count; i++)
            {
                if (platforms[i] == other.gameObject)
                {
                    return;
                }
            }
            platforms.Add(other.gameObject);
            DestroyPlatform();
        }
    }

    private void DestroyPlatform()
    {
        if (platforms.Count >= 5)
        {
            Destroy(platforms[0]);
            platforms.RemoveAt(0);
            groundScript.die = true;
        }
    }
}
