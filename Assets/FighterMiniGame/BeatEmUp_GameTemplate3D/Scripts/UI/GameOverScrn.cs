using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScrn : UISceneLoader
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI subtext;
    //public string TextRestart = Assets.SimpleLocalization.LocalizationManager.Localize("Game.Any.Key");
    //public string TextNextLevel = Assets.SimpleLocalization.LocalizationManager.Localize("Game.Any.Key");
    public Gradient ColorTransition;
    public float speed = 3.5f;
    private bool restartInProgress = false;

    private void Awake()
    {
        //string TextRestart = Assets.SimpleLocalization.LocalizationManager.Localize("Game.Any.Key");
        //string TextNextLevel = Assets.SimpleLocalization.LocalizationManager.Localize("Game.Any.Key");
        InputManager.onInputEvent += OnInputEvent;

        //display subtext
        //if (subtext != null)
        //{
        //    subtext.text = (GlobalGameSettings.LevelData.Count > 0 && !lastLevelReached()) ? TextNextLevel : TextRestart;
        //}
        //else
        //{
        //    Debug.Log("no subtext assigned");
        //}

        restartInProgress = false;
    }

    //private void OnEnable()
    //{
    //    InputManager.onInputEvent += OnInputEvent;

    //    //display subtext
    //    if (subtext != null)
    //    {
    //        subtext.text = (GlobalGameSettings.LevelData.Count > 0 && !lastLevelReached()) ? TextNextLevel : TextRestart;
    //    }
    //    else
    //    {
    //        Debug.Log("no subtext assigned");
    //    }

    //    restartInProgress = false;
    //}

    private void OnDisable()
    {
        InputManager.onInputEvent -= OnInputEvent;
    }

    //input event
    private void OnInputEvent(string action, BUTTONSTATE buttonState)
    {
        if (buttonState != BUTTONSTATE.PRESS) return;

        // Always load the 0th scene
        LoadLevel(0, 0);
    }

    void Update()
    {
        //text effect
        if (text != null && text.gameObject.activeSelf)
        {
            float t = Mathf.PingPong(Time.time * speed, 1f);
            text.color = ColorTransition.Evaluate(t);
        }

        //alternative input events
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {
            OnInputEvent("AnyKey", BUTTONSTATE.PRESS);
        }
    }

    //restarts the current level
    void LoadLevel(int buildIndex, int levelId)
    {
        if (!restartInProgress)
        {
            restartInProgress = true;

            // Destroy UI elements in the "Don't Destroy On Load" object
            if (text != null && text.transform.parent != null && text.transform.parent.CompareTag("DontDestroy"))
            {
                Destroy(text.transform.parent.gameObject);
            }

            if (subtext != null && subtext.transform.parent != null && subtext.transform.parent.CompareTag("DontDestroy"))
            {
                Destroy(subtext.transform.parent.gameObject);
            }

            // ...

            // Play sound effect
            GlobalAudioPlayer.PlaySFX("ButtonStart");

            // Start button flicker
            ButtonFlicker bf = GetComponentInChildren<ButtonFlicker>();
            if (bf != null)
            {
                bf.StartButtonFlicker();
            }

            // Load scene
            GlobalGameSettings.currentLevelId = levelId;
            SceneManager.LoadScene(buildIndex);
        }
    }

    //returns the name of the next scene
    string GetNextSceneName()
    {
        return GlobalGameSettings.LevelData[GlobalGameSettings.currentLevelId + 1].sceneToLoad;
    }

    //returns true if we are currently at the last level
    bool lastLevelReached()
    {
        int totalNumberOfLevels = Mathf.Clamp(GlobalGameSettings.LevelData.Count - 1, 0, GlobalGameSettings.LevelData.Count);
        return GlobalGameSettings.currentLevelId == totalNumberOfLevels;
    }
}