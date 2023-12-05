using UnityEngine;

public class BridgeActivator : MonoBehaviour
{
    public int spawnIndex;
    private bool inTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!inTrigger)
            {
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                other.gameObject.GetComponent<SphereCollider>().enabled = false;
                other.gameObject.GetComponent<PlayerBridgeController>().SetBoatScale(Vector3.zero, 0.0865f);
                AnimationsC.Instance.SetAnimatorBool("onBridge", true);
                inTrigger = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            collision.gameObject.GetComponent<SphereCollider>().enabled = false;
            AnimationsC.Instance.SetAnimatorBool("onBridge", true);
            collision.gameObject.GetComponent<PlayerBridgeController>().SetBoatScale(Vector3.zero, 0.0865f);
            inTrigger = true;
        }
    }
}
