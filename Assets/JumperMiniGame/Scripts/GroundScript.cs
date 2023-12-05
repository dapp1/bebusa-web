using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public GameObject player;
    public bool die = false;
    [SerializeField] private GameObject gameOverPanel;
    

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject == player && die)
        {
            player.GetComponent<JumperPlayerController>().isDead = true;

            player.GetComponent<Animator>().Play("Die");
            StartCoroutine(GameOver());
        }
    }
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        gameOverPanel.SetActive(true);
    }
}
