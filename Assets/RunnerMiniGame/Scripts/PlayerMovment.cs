using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    [Header("Line Moving")]
    [SerializeField] private float lineOffset;
    [SerializeField] private float lineChangeSpead;

    [SerializeField] private Vector3 targetVelociti;
    private float lastVelociti;
    private float startPoint;
    private float endPoint;
    private float lastHight;

    public bool isJumping;
    private bool canMove;
    private bool isRunning;
    [Header("Jump")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpSneakersPower;
    [SerializeField] private float jumpGravity;
    private float curentJumpPower;
    private float normalGravity;

    public Action lose;
    public Action startJetPack;
    public Action endJetPack;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private StartGame startGame;
    [SerializeField] private SpawnJetPackCoins spawnJetPackCoins;
    [SerializeField] private GameObject coinDetector;
    [SerializeField] private GameObject jetPack;
    private CapsuleCollider playerCollider;
    private Improvements improvements;
    [SerializeField] private float rollTime;
    private float rollTimer;
    private bool isRoll = false;

    [Header("JetPack")]
    [SerializeField] private float jetPackUpSpead;
    [SerializeField] private float jetPackHaight;
    public float jetPackActiveTime;
    [SerializeField] private BonusTimer jetPackIndicator;

    public bool isJetpack = false;
    [SerializeField] private GameObject bonusPanel;

    void Start()
    {
        normalGravity = -15f;
        curentJumpPower = jumpPower;
        rb = GetComponent<Rigidbody>();
        isJumping = false;
        canMove = true;
        isRunning = true;

        animator = GetComponentInChildren<Animator>();
        playerCollider = GetComponent<CapsuleCollider>();
        //jetPack = GetComponentInChildren<PlayerJetPack>().gameObject;
        improvements = GetComponent<Improvements>();

        jetPack.SetActive(false);

        spawnJetPackCoins.SetHaight(jetPackHaight);
        spawnJetPackCoins.SetLineOffset(lineOffset);

    }

    void Update()
    {
        rb.WakeUp();
        targetVelociti = rb.velocity;
        if (canMove)
        {
            if (SwipeManager.instance.swipeRight && endPoint < lineOffset)
            {
                ChangeLIne(lineChangeSpead);
                if (isJetpack)
                {
                    animator.Play("Jetpack_Right_Move");
                }
                else if(!isJumping)
                {
                    animator.CrossFade("Run_Right",0.1f);
                }
                else if (isJumping)
                {
                    animator.CrossFade("Jump_Move_Right", 0.1f);
                }
            }
            if (SwipeManager.instance.swipeLeft && endPoint > -lineOffset)
            {
                ChangeLIne(-lineChangeSpead);
                if (isJetpack)
                {
                    animator.Play("Jetpack_Left_Move");
                }
                else if (!isJumping)
                {
                    animator.CrossFade("Run_Left",0.1f);
                }
                else if (isJumping)
                {
                    animator.CrossFade("Jump_Move_Left", 0.1f);
                }
            }
            if (SwipeManager.instance.swipeUp && !isJumping)
            {
                Jump();
            }
            if (SwipeManager.instance.swipeDown && !isJetpack && !isRoll)
            {
                Roll();
            }
        }

        if(transform.position.y < -0.5)
        {
            transform.position = new Vector3(transform.position.x,0,transform.position.z);
        }
        if(rb.velocity.y < -1 && lastHight - transform.position.y > 0.25 && !isJumping && !isRoll)
        {
            isJumping = true;
            animator.CrossFade("Falling_Idle",0.25f);
            rb.velocity = new Vector3(rb.velocity.x,-5f, rb.velocity.z);
        }
    }
    public void ChangeLIne(float lineSpead)
    {
        startPoint = endPoint;
        endPoint += Mathf.Sign(lineSpead) * lineOffset;
        if(startPoint > lineOffset)
        {
            startPoint = lineOffset;
        }
        if(endPoint > lineOffset)
        {
            endPoint = lineOffset;
        }

        StartCoroutine(MoveCorutine(lineSpead));
    }

    private IEnumerator MoveCorutine(float xPos)
    {
        while (Mathf.Abs(startPoint - transform.position.x) < lineOffset)
        {
            yield return new WaitForFixedUpdate();
            rb.velocity = new Vector3(xPos, rb.velocity.y, 0);
            lastVelociti = xPos;
            float x = Mathf.Clamp(transform.position.x, Mathf.Min(startPoint, endPoint), Mathf.Max(startPoint, endPoint));
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(endPoint, transform.position.y, transform.position.z);
        if(transform.position.y > 1 && !isJetpack)
        {
            rb.velocity = new Vector3(rb.velocity.x,-7.5f,rb.velocity.z);
        }
    }

    private void Jump()
    {
        isJumping = true;
        animator.CrossFade("JumpUp",0.25f);
        rb.AddForce(Vector3.up * curentJumpPower, ForceMode.Impulse);
        Physics.gravity = new Vector3(0, jumpGravity, 0);
        StartCoroutine(StopJumping());
    }
    private IEnumerator StopJumping()
    {
        do
        {
            yield return new WaitForFixedUpdate();
        } while (rb.velocity.y != 0 || isJetpack);
        isJumping = false;
        Physics.gravity = new Vector3(0, normalGravity, 0);

    }
    private void Roll()
    {
        StartCoroutine(RollCorutine());
    }

    private IEnumerator RollCorutine()
    {
        isRoll = true;
        animator.CrossFade("Roll",0.25f);
        rb.AddForce(-Vector3.up * curentJumpPower, ForceMode.Impulse);
        rollTimer = 0;
        playerCollider.height = 1;
        playerCollider.center = new Vector3(playerCollider.center.x,0.5f, playerCollider.center.z);
        while(rollTimer <= rollTime)
        {
            rollTimer += Time.deltaTime;
            yield return null;
        }
        playerCollider.height = 2.7f;
        playerCollider.center = new Vector3(playerCollider.center.x, 1.3f, playerCollider.center.z);
        if (canMove && !isJumping)
        {
            animator.CrossFade("Run",0.25f);
        }
        isRoll = false;
    }

    public void StartMove()
    {
        animator.CrossFade("Run",0.1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ramp")
        {
            rb.constraints |= RigidbodyConstraints.FreezePositionZ;
        }
        if (other.tag == "NotLose")
        {
            ChangeLIne(-lastVelociti);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ramp")
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            if (!isJumping)
            {
                rb.velocity = new Vector3(0, 0, 0);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag !="Lose" && collision.gameObject.tag != "NotLose" && rb.velocity.y > -1)
        {
            animator.SetBool("isRoll", true);
        }
        if (collision.gameObject.tag != "Lose" && collision.gameObject.tag != "NotLose")
        {
            if(canMove && isJumping && !isRoll)
            {
                if (improvements.activeJumpSneakers)
                {
                    animator.CrossFade("Shoes_Run", 0.25f);
                }
                else
                {
                    animator.CrossFade("Run", 0.25f);
                }
            }
        }
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
        if (collision.gameObject.tag == "Lose")
        {
            lose?.Invoke();
            animator.Play("Lose");
            canMove = false;
            Invoke("ShowLosePanel", 1);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        animator.SetBool("isRoll", false);
        lastHight = transform.position.y;
    }



    private void ShowLosePanel()
    {
        bonusPanel.SetActive(false);
        losePanel.SetActive(true);
    }

    public void NewJumpPower() 
    {
        curentJumpPower = jumpSneakersPower;
    }

    public void ResetJumpPower()
    {
        curentJumpPower = jumpPower;
    }

    public void JetPack()
    {
        StartCoroutine(JetPackCorutine());
        isJetpack = true;
    }
    private IEnumerator JetPackCorutine()
    {
        StopCoroutine(StopJumping());
        StopCoroutine(RollCorutine());
        spawnJetPackCoins.SpawnCoins();
        coinDetector.transform.position += new Vector3(0, jetPackHaight, 0);
        jetPack.SetActive(true);
        isJumping = true;
        Physics.gravity = Vector3.zero;
        animator.Play("JetStart");
        while (transform.position.y < jetPackHaight)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, jetPackHaight, transform.position.z), jetPackUpSpead * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        animator.Play("JetFlying");
        startJetPack?.Invoke();
        rb.velocity = Vector3.zero;
        jetPackIndicator.gameObject.SetActive(true);
        jetPackIndicator.ResetTimer();
        yield return new WaitForSeconds(jetPackActiveTime);
        isJetpack = false;
        isJumping = false;
        lastHight = transform.position.y;
        jetPackIndicator.gameObject.SetActive(false);
        jetPack.SetActive(false);
        Physics.gravity = new Vector3(0, normalGravity, 0);
        coinDetector.transform.position -= new Vector3(0, jetPackHaight, 0);
        spawnJetPackCoins.StopSpawn();
        animator.Play("Run");
        endJetPack?.Invoke();
    }
    private void OnEnable()
    {
        startGame.startMoving += StartMove;
    }
    private void OnDisable()
    {
        startGame.startMoving -= StartMove;
    }
}
