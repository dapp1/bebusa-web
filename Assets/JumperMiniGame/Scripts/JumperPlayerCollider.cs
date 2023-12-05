using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperPlayerCollider : MonoBehaviour
{

    public GameObject player;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PlaneTopTrigger"))
        {
            player.transform.parent = collision.transform;
        }

    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("PlaneTopTrigger"))
        {
            player.transform.parent = null;
        }

    }


}
