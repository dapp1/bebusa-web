using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LosePanel : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("RunnerMiniGame");
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
