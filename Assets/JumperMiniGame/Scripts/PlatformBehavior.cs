using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float travelTime;
    private Rigidbody rb;
    private Vector3 currentPos;

    CharacterController cc;
    JumperPlayerController pc;

    Vector3 platformVelocity = Vector3.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        currentPos = Vector3.Lerp(startPoint.position, endPoint.position,
        Mathf.Cos(Time.time / travelTime * Mathf.PI * 2) * -.5f + .5f);
        rb.MovePosition(currentPos);

        if(pc != null)
            pc.platformVelocity = platformVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cc = other.GetComponent<CharacterController>();
            // Debug.Log("Player entered platform");
            pc = other.GetComponent<JumperPlayerController>();
            pc.isPlatform = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            pc = other.GetComponent<JumperPlayerController>();
            pc.isPlatform = false;
            // Debug.Log("Player exited platform");
            platformVelocity = Vector3.zero;
            pc = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            // cc.Move(rb.velocity * Time.deltaTime);
            // Debug.Log("Player is on platform " + pc);
            platformVelocity = rb.velocity * Time.deltaTime;
        }
    }

}