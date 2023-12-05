using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquidPlayerController : MonoBehaviour
{
    private AudioSource audio;
    public FloatingJoystick joystick;
    public Button jumpButton;
    public Animator animator;
    public float speed = 5f;
    private CharacterController characterController;
    private Vector3 moveDirection;
    private Rigidbody rb;
    private float ySpeed;
    public float minYSpeed;
    public float gravityForceMultiply = 1f;
    private float originalStepOffset;
    public float jumpSpeed;
    public bool isDead = false;
    public bool isWin = false;
    private bool isDeadCoroutine = false;
    private Transform camTrans;
    [SerializeField] private bool isGB = false;
    private bool fall = false;
    public GameObject endScreen;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        jumpButton.onClick.AddListener(Jump);
        camTrans = Camera.main.transform;
        isDead = false;
        isWin = false;
    }

    private void FixedUpdate()
    {
        if (isDead && !isDeadCoroutine)
        {
            audio.Play();
            animator.Play("Die");
            joystick.gameObject.SetActive(false);
            return;
        }
        if (isDead) return;
        if (isWin)
        {
            animator.SetFloat("Speed", 0);
            return;
        }
        if (isGB && this.gameObject.transform.position.y < 70 && !fall)
        {
            minYSpeed = -350;
            animator.Play("Falling");
            fall = true;
        }
        if (!RedlightGreenlightController.isGreen && !rb.IsSleeping())
        {
            if (!isDead)
            {
                StartCoroutine(DeadTime());

                return;
            }
        }

        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        horizontalInput += Input.GetAxis("Horizontal");
        verticalInput += Input.GetAxis("Vertical");

        MoveCharacter(horizontalInput, verticalInput);

    }
    private IEnumerator DeadTime()
    {
        if (!isDeadCoroutine)
        {
            isDeadCoroutine = true;
            yield return new WaitForSecondsRealtime(0.3f);
            isDead = true;
            animator.Play("Die");
            yield return new WaitForSecondsRealtime(3f);
            endScreen.SetActive(true);
        }

    }

    private void MoveCharacter(float horizontal, float vertical)
    {
        Vector3 camForward = Vector3.Scale(camTrans.forward, new Vector3(1, 0, 1).normalized);
        Vector3 movementDirection = vertical * camForward + horizontal * camTrans.right;

        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();

        if (magnitude > 0)
        {
            animator.SetFloat("Speed", magnitude);

            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(movementDirection.x, 0f, movementDirection.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        ySpeed += Physics.gravity.y * Time.deltaTime * gravityForceMultiply;
        ySpeed = Mathf.Clamp(ySpeed, minYSpeed, int.MaxValue);

        Vector3 velocity = movementDirection * magnitude * 0.001f;
        velocity.y = ySpeed * Time.deltaTime;

        characterController.Move(velocity);
    }

    private void Jump()
    {
        if (isDead) return;
        if (isWin) return;

        if (characterController.isGrounded)
        {
            characterController.stepOffset = originalStepOffset;

            ySpeed = jumpSpeed;
            animator.Play("JumpFlip");

        }
        else
        {
            characterController.stepOffset = 0;
        }
    }

}
