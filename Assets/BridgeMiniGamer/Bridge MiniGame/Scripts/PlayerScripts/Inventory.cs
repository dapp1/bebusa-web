using Pixelplacement;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    public List<GameObject> woodsPicked = new List<GameObject>();
    public List<GameObject> woodsInBoat = new List<GameObject>();
    [HideInInspector] public int spawnedBridges;
    [HideInInspector] public int index;

     private BoxCollider _boxCollider;
    [SerializeField] private PlayerBridgeController _playerController;

    private void Start()
    {
        _boxCollider = GetComponentInChildren<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TriggerPlatform"))
        {
            if (woodsPicked.Count > 0)
            {
                collision.gameObject.GetComponent<BoxCollider>().enabled = true;
                collision.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
                woodsPicked.RemoveAt(0);
            }
            else
            {
                _boxCollider.enabled = false;
                UIController.Instance.lost = true;
            }

            foreach (GameObject obj in woodsInBoat)
            {
                obj.SetActive(false);
            }

            AnimationsC.Instance.SetAnimatorBool("onBridge", true);
        }

        if (collision.gameObject.CompareTag("Lose"))
        {
            UIController.Instance.lost = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("TriggerPlatform"))
        {
            AnimationsC.Instance.SetAnimatorBool("fallFromBridge", true);
        }
    }
}
