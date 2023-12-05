using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SquidUILevelController : MonoBehaviour
{
    public int winExp = 0;
    public int winMoney = 0;
    public int loseExp = 0;
    public int loseMoney = 0;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private int levelNumber;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private GameObject audio;

    public void FinalUI(bool isWin)
    {
        Destroy(audio);
        restartButton.SetActive(isWin);
        nextButton.SetActive(isWin);
        quitButton.SetActive(!isWin);
        if (isWin)
        {
            UpdateWinRewards();
            //resultText.text = Assets.SimpleLocalization.LocalizationManager.Localize("Game.Complete") + levelNumber + "!";
            if (levelNumber != 7)
            {
                PlayClip(0);
            }
            if (levelNumber == 7)
            {
                PlayClip(2);
                restartButton.SetActive(isWin);
                quitButton.SetActive(isWin);
                nextButton.SetActive(!isWin);
            }
        }
        else
        {
            UpdateLoseRewards();
            PlayClip(1);
            //resultText.text = Assets.SimpleLocalization.LocalizationManager.Localize("Game.Lose");
        }
    }
    public void UpdateWinRewards()
    {
        expText.text = winExp.ToString();
        moneyText.text = winMoney.ToString();
        Money.GainMoney(winMoney);
    }
    public void UpdateLoseRewards()
    {
        expText.text = loseExp.ToString();
        moneyText.text = loseMoney.ToString();
        Money.GainMoney(loseMoney);

    }
    private void PlayClip(int clipIndex)
    {
        audioSource.clip = audioClips[clipIndex];
        audioSource.Play();
    }
}
