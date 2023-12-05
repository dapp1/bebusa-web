using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedlightGreenlightBotController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audio;

    public float speed = 5f;
    private CharacterController characterController;
    private float ySpeed;
    public float minYSpeed;
    public float gravityForceMultiply = 1f;
    private Rigidbody rb;
    private bool isDead = false;
    private float moveTime = 0f;
    public float timeToGo = 0.2f;
    public bool isWin = false;
    private bool isWinHandled = false;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

        RedlightGreenlightController.onGreen += Move;
        
           
        
    }
    private void Move(){
         if (!isDead && !isWin)
                StartCoroutine(StartMoving());
    }
    private void OnDestroy(){
        RedlightGreenlightController.onGreen -= Move;

    }
    public IEnumerator StartMoving()
    {
        bool die = false;
        if (RedlightGreenlightController.needKills > 0)
        {
            die = Random.Range(0, 50) < 10;
            if (die)
            {
                RedlightGreenlightController.needKills--;
            }
        }

        float randomMultiplier = die ? Random.Range(1.05f, 1.1f) : Random.Range(0.6f, 0.8f);
        timeToGo = RedlightGreenlightController.currentInterval * randomMultiplier;

        yield return new WaitForSecondsRealtime(Random.Range(0f, 0.2f));

        while (timeToGo > 0f)
        {
            timeToGo -= Time.deltaTime;
            MoveForward();
            yield return new WaitForFixedUpdate();
        }

        StopMoving();

        yield return new WaitForSecondsRealtime(0.1f);

        if (die)
        {
            this.isDead = true;
            audio.Play();
            animator.Play("Die");
        }
    }

    private void Win()
    {
        StartCoroutine(win());
        IEnumerator win()
        {
            yield return new WaitForSecondsRealtime(
                Random.Range(0.1f, 0.2f)
            );
            animator.Play("Win");
            StopMoving();
            StopAllCoroutines();
        }
    }


    private void MoveForward()
    {
        if (isWin && !isWinHandled)
        {
            isWinHandled = true;
            Win();
        }

        float forwardSpeed = speed;
        Vector3 velocity = transform.forward * forwardSpeed;
        ySpeed += Physics.gravity.y * Time.deltaTime * gravityForceMultiply;
        ySpeed = Mathf.Clamp(ySpeed, minYSpeed, int.MaxValue);
        velocity.y = ySpeed * Time.deltaTime;
        characterController.Move(velocity);
        animator.SetFloat("Speed", forwardSpeed);
    }

    private void StopMoving()
    {
        ySpeed = 0f;
        animator.SetFloat("Speed", 0f);
        characterController.Move(Vector3.zero);
    }
}