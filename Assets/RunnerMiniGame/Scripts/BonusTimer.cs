using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusTimer : MonoBehaviour
{
    public enum Bonus
    {
        JetPack,
        Magnit,
        Shoes
    }

    public Bonus bonus;

    private Improvements improvements;
    private PlayerMovment playerMovment;
    private float maxTime;
    private float leftTime;

    public Image fillImage;


    private float imageColorYellow;
    private float imageColorRed;
    void Start()
    {
        improvements = GameObject.FindGameObjectWithTag("Player").GetComponent<Improvements>();
        playerMovment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>();

        switch (bonus)
        {
            case Bonus.JetPack:
                maxTime = playerMovment.jetPackActiveTime;
                break;
            case Bonus.Magnit:
                maxTime = improvements.activeMagnitTime;
                break;
            case Bonus.Shoes:
                maxTime = improvements.activeJumpSneakersTime;
                break;
        }

        leftTime = maxTime;
        imageColorYellow = maxTime;
        imageColorRed = maxTime;
    }


    void Update()
    {
        leftTime -= Time.deltaTime;
        fillImage.fillAmount = leftTime / maxTime;

        if (fillImage.fillAmount > 0.5)
        {
            imageColorYellow -= Time.deltaTime * 2;
            fillImage.color = Color.Lerp(Color.yellow,Color.green, imageColorYellow / maxTime);
        }
        else
        {
            imageColorRed -= Time.deltaTime * 2;
            fillImage.color = Color.Lerp(Color.red, Color.yellow, imageColorRed / maxTime);
        }
    }

    public void ResetTimer()
    {
        leftTime = maxTime;
        imageColorYellow = maxTime;
        imageColorRed = maxTime;
    }
}
