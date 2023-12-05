using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JumperPlayerController : MonoBehaviour
{
    public VariableJoystick joystick;
    public Button jumpButton;
    public Animator animator;
    public bool isHorizontalInput = true;
    public int inversion = 1;

    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    public float gravityForceMultiply = 1f;
    public float minYSpeed;
    public PlatformsDestroyer platformsDestroyer;

    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    public bool isDead = false;
    public bool isPlatform = false;

    public Vector3 platformVelocity = Vector3.zero;

    public float fixedPosition;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        jumpButton.onClick.AddListener(Jump);
    }
    public IEnumerator MovePlayer()
    {
        while (true)
        {
            if (isHorizontalInput)
            {
                Vector3 newPosition = new Vector3(fixedPosition, transform.position.y, transform.position.z);
                transform.position = newPosition;
            }
            else
            {
                Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, fixedPosition);
                transform.position = newPosition;
            }

            yield return new WaitForSeconds(5f);  // Wait for 5 seconds
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            joystick.gameObject.SetActive(false);
        }

        float horizontalInput = inversion * (Input.GetAxis("Horizontal") + joystick.Horizontal);
        // Debug.Log("Joystick Horizontal: " + joystick.Horizontal);
        if (joystick.Horizontal > 0)
        {
            joystick.Horizontal = 1;
        }
        if (joystick.Horizontal < 0)
        {
            joystick.Horizontal = -1;
        }


        if (isHorizontalInput)
        {
            Vector3 movementDirection = new Vector3(0, 0, horizontalInput);
            float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
            movementDirection.Normalize();

            if (movementDirection.z != 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(0f, 0f, movementDirection.z));
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
            }

            ySpeed += Physics.gravity.y * Time.deltaTime * gravityForceMultiply;
            ySpeed = Mathf.Clamp(ySpeed, minYSpeed, int.MaxValue);

            Vector3 velocity = movementDirection * magnitude * 0.001f;
            velocity.y = ySpeed * Time.deltaTime;


            characterController.Move(velocity + platformVelocity);
            animator.SetFloat("Speed", Mathf.Abs(movementDirection.z));

            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            Vector3 movementDirection = new Vector3(horizontalInput, 0, 0);
            float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
            movementDirection.Normalize();

            if (movementDirection.x != 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(movementDirection.x, 0f, 0f));
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
            }

            ySpeed += Physics.gravity.y * Time.deltaTime * gravityForceMultiply;
            ySpeed = Mathf.Clamp(ySpeed, minYSpeed, int.MaxValue);

            Vector3 velocity = movementDirection * magnitude * 0.001f;
            velocity.y = ySpeed * Time.deltaTime;


            characterController.Move(velocity + platformVelocity); animator.SetFloat("Speed", Mathf.Abs(movementDirection.x));

            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }

        platformVelocity = Vector3.zero;
    }

    private void Jump()
    {
        if (isDead) return;

        if (characterController.isGrounded || isPlatform)
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