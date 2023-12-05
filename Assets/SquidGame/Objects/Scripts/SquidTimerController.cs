using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public enum PlayerType
{
    RedGreenPlayer,
    LastGamePlayer,
    BrawlPlayer,
    HoneycombsPlayer
}

public class SquidTimerController : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timerTime;
    private float startTime;
    private bool isTimerRunning = false;

    [SerializeField] private PlayerType currentPlayerType;
    [SerializeField] private GameObject endScreen;

    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            UpdateTimerText();
        }
    }

    private void StartTimer()
    {
        startTime = Time.time;
        isTimerRunning = true;
    }
    private void End(){
        if (endScreen != null)
        {
            endScreen.SetActive(true);
            endScreen.GetComponent<SquidUILevelController>().FinalUI(false);
        }
    }
    private void UpdateTimerText()
    {
        float timeElapsed = Time.time - startTime;
        float timeRemaining = Mathf.Max(0f, timerTime - timeElapsed);

        int seconds = Mathf.FloorToInt(timeRemaining);

        timerText.text = seconds.ToString();

        if (timeRemaining <= 0f)
        {
            isTimerRunning = false;

            switch (currentPlayerType)
            {
                case PlayerType.RedGreenPlayer:
                    SquidPlayerController redGreenPlayer = player.GetComponent<SquidPlayerController>();
                    if (redGreenPlayer != null && !redGreenPlayer.isWin)
                    {
                        redGreenPlayer.isDead = true;
                        End();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
