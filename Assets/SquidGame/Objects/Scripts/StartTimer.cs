using System.Collections;
using TMPro;
using UnityEngine;

public class StartTimer : MonoBehaviour
{
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject timerUI;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float time;
    [SerializeField] private GameObject startOffObject;

    private PlayerBridgeController bridgePlayer;
    private UIManager fighter;
    private InputManager inputManager;
    private void Start()
    {
        bridgePlayer = FindObjectOfType<PlayerBridgeController>();
        fighter = FindObjectOfType<UIManager>();
        inputManager = FindObjectOfType<InputManager>();
        if (bridgePlayer != null)
        {
            bridgePlayer.moveSpeedForward = 0;
        }

        if (fighter != null)
        {
            inputManager.floatingJoystick = GameObject.FindObjectOfType<FloatingJoystick>();
            startUI = fighter.gameObject;
        }

        startUI.SetActive(false);
        if (startOffObject != null)
        {
            startOffObject.SetActive(false);
        }

        StartCountdown(time);
    }

    public void StartCountdown(float seconds)
    {
        time = seconds;
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        float countdownTime = time;

        while (countdownTime > 0)
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                Time.timeScale = 1;
                break; 
            }
            timerText.text = Mathf.CeilToInt(countdownTime).ToString();
            countdownTime -= Time.deltaTime;
            yield return null;
        }

        timerText.text = "0";
        startUI.SetActive(true);
        if (startOffObject != null)
        {
            startOffObject.SetActive(true);
        }

        if (bridgePlayer != null)
        {
            bridgePlayer.moveSpeedForward = 8;
        }

        Destroy(timerUI);
    }
}
