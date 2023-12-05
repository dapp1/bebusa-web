using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumperGameOver : MonoBehaviour
{
    public void Restart()
    {
        NextLevelCollider.levelId = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Exit()
    {
        NextLevelCollider.levelId = 1;
        SceneManager.LoadScene(0);
    }
}
