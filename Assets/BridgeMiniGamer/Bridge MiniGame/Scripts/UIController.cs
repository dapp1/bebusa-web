using Pixelplacement;
using System;
using TMPro;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    [SerializeField] private GameObject loseGame;
    [SerializeField] private TextMeshProUGUI woodText;
    public bool lost;

    private float woodCount;

    private void Start()
    {
        woodCount = 0;
        woodText.text = $"0/{woodCount}";
    }

    private void LateUpdate()
    {
        ChangeTextWood();
        BoolChecker();
    }

    private void ChangeTextWood()
    {
        woodText.fontSize = 50;

        woodText.text = $"{Inventory.Instance.woodsPicked.Count}/{woodCount}";

        if (woodCount == Inventory.Instance.woodsPicked.Count)
        {
            woodText.fontSize = 35;
            //woodText.text = Assets.SimpleLocalization.LocalizationManager.Localize("Game.Complete");
        }
    }

    public float ChangeWoodCount(float wood)
    {
        return woodCount = wood;
    }

    public float WoodCount()
    {
        return woodCount;
    }

    private void BoolChecker()
    {
        if (lost)
        {
            Time.timeScale = 0;
            loseGame.SetActive(true);
        }
    }

    public void SetPause()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
