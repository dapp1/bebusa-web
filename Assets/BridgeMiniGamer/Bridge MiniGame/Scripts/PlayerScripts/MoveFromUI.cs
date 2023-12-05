using UnityEngine;

public class MoveFromUI : MonoBehaviour
{
    // Start is called before the first frame update
    public void MoveLeftPointerDown()
    {
        PlayerBridgeController.Instance.moveLeft = true;
    }

    public void MoveLeftPointerExit()
    {
        PlayerBridgeController.Instance.moveLeft = false;
    }

    public void MoveRightPointerDown()
    {
        PlayerBridgeController.Instance.moveRight = true;
    }

    public void MoveRightPointerExit()
    {
        PlayerBridgeController.Instance.moveRight = false;
    }

    public void Jump()
    {
        PlayerBridgeController.Instance.Jump();
    }
}
