using Bridge;
using System;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]
public class StartPath : NetworkBehaviour
{
    private float _countOfWood;
    private int _spawnIndex;

    private bool collisionExit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIController.Instance.ChangeWoodCount(_countOfWood);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!collisionExit)
            {
                var inventory = collision.gameObject.GetComponent<Inventory>();
                inventory.index = 0;

                collision.gameObject.GetComponent<BoxCollider>().enabled = true;
                collision.gameObject.GetComponent<SphereCollider>().enabled = true;
                var rb = collision.gameObject.GetComponent<Rigidbody>();
                AnimationsC.Instance.SetAnimatorBool("onBridge", false);
                PlayerBridgeController.Instance.SetBoatScale(new Vector3(0.7f, 0.6f, 0.6f), 0.0865f);
                if (_spawnIndex > 1)
                {

                        PlayerSpawn.Instance.SetAcceleration(_spawnIndex);

                    collisionExit = true;
                }
            }
        }
    }

    public void Initialized(int index, float countWoods)
    {
        _spawnIndex = index;
        _countOfWood = countWoods;
    }
}
