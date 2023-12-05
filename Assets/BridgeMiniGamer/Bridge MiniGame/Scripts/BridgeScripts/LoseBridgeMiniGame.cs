using UnityEngine;
using UnityEngine.SceneManagement;


public class LoseBridgeMiniGame : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("BridgeMiniGame");
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
