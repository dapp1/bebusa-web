using UnityEngine;

public class CameraController : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position= new Vector3(transform.position.x, PlayerBridgeController.Instance.transform.position.y + 6, PlayerBridgeController.Instance.transform.position.z - 5);
    }
}
