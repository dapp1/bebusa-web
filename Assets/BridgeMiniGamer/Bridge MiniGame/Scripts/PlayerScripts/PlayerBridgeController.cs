using UnityEngine;
using System.Collections;
using Bridge;
using Unity.Netcode;

public class PlayerBridgeController : MonoBehaviour
{
    public static PlayerBridgeController Instance;

    public float moveSpeedForward = 5f;
    public float moveSpeedSide = 2f;
    
    private float speedForward;
    private float speedSide;

    [SerializeField] private Animator _boatAnim;
    private Rigidbody rb;
    public GameObject boat;

    public GameObject models;

    private void Awake()
    {
        Instance = this;
        speedForward = moveSpeedForward;
        speedSide = moveSpeedSide;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetNewSpeed(float valueForward, float valueSide)
    {
        moveSpeedForward = speedForward + valueForward;
        moveSpeedSide = speedSide + valueSide;
    }   

    private void FixedUpdate()
    {
        Move();

        if (gameObject.transform.position.y < -1.3f)
        {
            UIController.Instance.lost = true;
        }

        if(transform.rotation.y > 0 || transform.rotation.z > 0) 
        {
            Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
            transform.rotation = newRotation;

        }
    }

    private Vector3 forwardMovement;
    private Vector3 sideMovement;
    public bool moveLeft = false;
    public bool moveRight = false;

    public float jumpForce = 12f;

    private bool onWater;
    private void Move()
    {
        if (onPause) return;

        float moveSpeedFor;
        float moveSpeedS;

            Debug.Log("single");
            moveSpeedS = moveSpeedSide;
            moveSpeedFor = moveSpeedForward;

        forwardMovement = transform.forward * moveSpeedFor * Time.fixedDeltaTime;


        sideMovement = Vector3.zero;
        if (moveLeft)
        {
            sideMovement = Vector3.left * moveSpeedS * Time.fixedDeltaTime;
        }
        else if (moveRight)
        {
            sideMovement = Vector3.right * moveSpeedS * Time.fixedDeltaTime;
        }

        // Общее движение - вперед и вбок
        Vector3 newPosition = rb.position + forwardMovement + sideMovement;

        float targetX = Mathf.Clamp(newPosition.x, -9.25f, 9.25f);
        newPosition.x = targetX;
        rb.MovePosition(newPosition);

        if(onWater)
            AnimationsC.Instance.SetAnimatorBool("onWater", true);
        else
            AnimationsC.Instance.SetAnimatorBool("onWater", false);
    }

    public bool onPause;
    public void SetPause()
    {
        onPause = true;
    }
    public void Jump() 
    {
        if (onWater && !AnimationsC.Instance.animator.GetBool("onBridge"))
        {
            onWater = false;

            float jumpForce;

                jumpForce = this.jumpForce;

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            SetBoatScale(Vector3.zero, 0.0865f);
        }
    }

    public Vector3 PlayerPosition()
    {
        return transform.position;
    }

    private Coroutine scaleCoroutine;

    public void SetBoatScale(Vector3 scale, float duration)
    {
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        scaleCoroutine = StartCoroutine(SmoothScaleChange(scale, duration));

        if (scale == Vector3.zero)
            _boatAnim.enabled = false;
        else
            _boatAnim.enabled = true;
    }

    private IEnumerator SmoothScaleChange(Vector3 targetScale, float duration)
    {
        Vector3 initialScale = boat.transform.localScale;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            boat.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        boat.transform.localScale = targetScale;
        scaleCoroutine = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onWater = true;
            SetBoatScale(new Vector3(0.7f, 0.6f, 0.6f), 0.0865f);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            onWater = false;
            SetBoatScale(Vector3.zero, 0.0865f);
        }
    }
}