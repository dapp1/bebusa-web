using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Improvements : MonoBehaviour
{
    public float activeMagnitTime;
    public float activeJumpSneakersTime;
    [SerializeField] private GameObject coinDetector;
    private PlayerMovment playerMovment;
    private Animator animator;

    //private bool activeMagnit;
    public bool activeJumpSneakers;

    [SerializeField] private GameObject magnit;
    [SerializeField] private GameObject LeftShose;
    [SerializeField] private GameObject RightShose;

    //[SerializeField] private BonusTimer magnitIndicator;
    [SerializeField] private BonusTimer shoseIndicator;


    void Start()
    {
        playerMovment = GetComponent<PlayerMovment>();
        //activeMagnit = false;
        activeJumpSneakers = false;

        //magnit = GetComponentInChildren<PlayerMagnit>().gameObject;
        LeftShose = GetComponentInChildren<LeftShose>().gameObject;
        RightShose = GetComponentInChildren<RightShose>().gameObject;
        animator = GetComponentInChildren<Animator>();

        magnit.SetActive(false);
        LeftShose.SetActive(false);
        RightShose.SetActive(false);
    }

    //public void ActiveMagnit()
    //{
    //    if (activeMagnit)
    //    {
    //        StopCoroutine(MagnitCoroutine());
    //        StartCoroutine(MagnitCoroutine());

    //    }
    //    else
    //    {
    //        StartCoroutine(MagnitCoroutine());
    //    }
    //}

    public void ActiveJumpSneakers()
    {
        if (activeJumpSneakers)
        {
            StopCoroutine(JumpSneakersCoroutine());
            StartCoroutine(JumpSneakersCoroutine());
        }
        else
        {
            StartCoroutine(JumpSneakersCoroutine());
        }
    }

    public void ActiveJetPack()
    {
        playerMovment.JetPack();
    }

    //private IEnumerator MagnitCoroutine()
    //{
    //    activeMagnit = true;
    //    magnitIndicator.gameObject.SetActive(true);
    //    magnitIndicator.ResetTimer();
    //    coinDetector.SetActive(true);
    //    magnit.SetActive(true);
    //    yield return new WaitForSeconds(activeMagnitTime);
    //    coinDetector.SetActive(false);
    //    magnit.SetActive(false);
    //    magnitIndicator.gameObject.SetActive(false);
    //    activeMagnit = false;

    //}

    private IEnumerator JumpSneakersCoroutine()
    {
        activeJumpSneakers = true;
        shoseIndicator.gameObject.SetActive(true);
        shoseIndicator.ResetTimer();
        playerMovment.NewJumpPower();
        animator.SetBool("isJumpShoes", true);
        LeftShose.SetActive(true);
        RightShose.SetActive(true);
        yield return new WaitForSeconds(activeJumpSneakersTime);
        playerMovment.ResetJumpPower();
        LeftShose.SetActive(false);
        RightShose.SetActive(false);
        animator.SetBool("isJumpShoes", false);
        shoseIndicator.gameObject.SetActive(false);
        activeJumpSneakers = false;
    }
}
