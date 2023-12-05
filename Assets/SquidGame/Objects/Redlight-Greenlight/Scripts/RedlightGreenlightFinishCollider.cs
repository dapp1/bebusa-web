using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedlightGreenlightFinishCollider : MonoBehaviour
{
    private GameObject player;
    private GameObject bot;
    public GameObject endScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SquidPlayerController>())
        {
            player = other.gameObject;
            player.gameObject.GetComponent<SquidPlayerController>().isWin = true;
            endScreen.SetActive(true);
            endScreen.GetComponent<SquidUILevelController>().FinalUI(true);
        }
        if (other.gameObject.GetComponent<RedlightGreenlightBotController>())
        {
            bot = other.gameObject;
            bot.gameObject.GetComponent<RedlightGreenlightBotController>().isWin = true;
        }
    }
}
