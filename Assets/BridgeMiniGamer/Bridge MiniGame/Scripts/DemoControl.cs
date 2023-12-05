using Pixelplacement;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoControl : Singleton<DemoControl>
{
    public static int coinCount;
    public static int countToNextLevel;

    public static string currentLevel = SceneKey.bridge;

    public static bool restart = false;

    [SerializeField] private int _countToNextLevel;
    public TextMeshProUGUI cointText;
    public GameObject toNextLvl;

    private void Start()
    {
        Time.timeScale = 1;
        if (!restart)
        {
            countToNextLevel += _countToNextLevel;
        }
        cointText.text = coinCount.ToString();
        cointText.text = coinCount.ToString() + "/" + countToNextLevel.ToString();
    }

    //For Bridge MiniGame
    public void CoinPickup()
    {
        coinCount += 1;
        cointText.text = coinCount.ToString() + "/" + countToNextLevel.ToString();

        if(coinCount >= countToNextLevel )
        {
            Time.timeScale = 0;
            toNextLvl.SetActive(true);
        }
    }

    //For All scenes
    public void RestartBridge()
    {
        if(coinCount < countToNextLevel)
        {
            restart = true;
            SceneManager.LoadScene(currentLevel);
        } else
        {
            restart = false;
            Level();
        }
    }

    void Level()
    {
        switch (currentLevel)
        {
            case SceneKey.bridge:
                currentLevel = SceneKey.jumper;
                SceneManager.LoadSceneAsync(currentLevel, LoadSceneMode.Single);
                break;
            case SceneKey.jumper:
                currentLevel = SceneKey.runner;
                SceneManager.LoadSceneAsync(currentLevel, LoadSceneMode.Single);
                break;
            case SceneKey.runner:
                currentLevel = SceneKey.squid;
                SceneManager.LoadSceneAsync(currentLevel, LoadSceneMode.Single);
                break;
            case SceneKey.squid:
                currentLevel = SceneKey.fighter;
                SceneManager.LoadSceneAsync(currentLevel, LoadSceneMode.Single);
                break;
        }
    }
}

public static class SceneKey
{
    public const string bridge = "Bridge";
    public const string jumper = "Jumper";
    public const string runner = "Runner";
    public const string fighter = "Fighter";
    public const string squid = "Squid";
}