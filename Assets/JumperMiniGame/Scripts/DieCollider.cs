using UnityEngine;
using System.Collections;

public class DieCollider : MonoBehaviour
{
    [SerializeField] private GroundScript groundScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            NextLevelCollider.levelId = 0;
            
            groundScript.die = true;
           
            // Destroy(this.gameObject);
        }
    }
    
}
