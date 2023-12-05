using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    public enum MovementDirection
    {
        LeftRight,
        ForwardBackward,
        UpDown
    }

    public float speed = 1.0f;
    public float maxOffset = 5.0f;
    public MovementDirection movementDirection = MovementDirection.LeftRight;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }


    void FixedUpdate()
    {
        float offset = Mathf.Sin(Time.time * speed) * maxOffset;
        switch (movementDirection)
        {
            case MovementDirection.LeftRight:
                transform.position = startPosition + new Vector3(offset, 0, 0);
                break;
            case MovementDirection.ForwardBackward:
                transform.position = startPosition + new Vector3(0, 0, offset);
                break;
            case MovementDirection.UpDown:
                transform.position = startPosition + new Vector3(0, offset, 0);
                break;
        }
    }
}
