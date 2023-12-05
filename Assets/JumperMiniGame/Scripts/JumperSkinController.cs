using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JumperSkinController : MonoBehaviour
{
    [SerializeField] private GameObject[] skins;
    [SerializeField] private JumperCamera jumperCamera;
    // private NextLevelCollider nextLevelCollider;
    [SerializeField] private GroundScript groundScript;

    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private Button jumpButton;
    private JumperPlayerController jumperPlayerController;

    void Start()
    {
        SetSkin(0);
    }

    private void SetSkin(int index)
    {

        GameObject skin = Instantiate(skins[index], new Vector3(1, -20, 22), Quaternion.identity);
        jumperCamera.player = skin;
        groundScript.player = skin;
        jumperPlayerController = skin.GetComponent<JumperPlayerController>();
        jumperPlayerController.joystick = variableJoystick;
        jumperPlayerController.jumpButton = jumpButton;
        jumperPlayerController.animator = skin.GetComponent<Animator>();
        jumperPlayerController.platformsDestroyer.groundScript = groundScript;
    }

}
