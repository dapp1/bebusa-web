using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class squidSkinController : MonoBehaviour
{
    [SerializeField] private GameObject[] skins;
    [SerializeField] public FloatingJoystick joystick;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private Button jumpButton;
    [SerializeField] private SquidTimerController timerController;
    [SerializeField] private GameObject endScreen;


    public static squidSkinController instance;
    public Vector3 startPosition = new Vector3(0f, 0f, -76f);

    void Start()
    {

        switch (PlayerPrefs.GetInt("Skin"))
        {
            case 0:
                SetSkin(0);
                break;
            case 1:
                SetSkin(1);
                break;
            case 2:
                SetSkin(2);
                break;
            case 3:
                SetSkin(3);
                break;
            case 4:
                SetSkin(4);
                break;
            case 5:
                SetSkin(5);
                break;
            case 6:
                SetSkin(6);
                break;
            case 7:
                SetSkin(7);
                break;
            case 8:
                SetSkin(8);
                break;
            case 9:
                SetSkin(9);
                break;
            case 10:
                SetSkin(10);
                break;
            case 11:
                SetSkin(11);
                break;

        }
    }

    private void SetSkin(int index)
    {

        for (int i = 0; i < skins.Length; i++)
        {
            if (i == index)
            {
                GameObject skin = Instantiate(skins[i], startPosition, Quaternion.identity);
                SquidPlayerController characterMove = skin.GetComponent<SquidPlayerController>();
                characterMove.jumpButton = jumpButton;
                characterMove.joystick = joystick;
                cameraMove.player = skin.transform;
                cameraMove.player = characterMove.transform;
                timerController.player = skin.gameObject;
                if(endScreen != null){
                    characterMove.endScreen = endScreen;
                }
            }

        }
    }
}
