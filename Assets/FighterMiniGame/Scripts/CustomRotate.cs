using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRotate : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private float rotX;

    // Define the bounds for the rotation area
    private Rect rotationArea;

    void Start()
    {
        rotationArea = new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height);
    }

    void Update()
    {
        // Initialize the position with a default value
        Vector2 pos = Vector2.zero;

        // If it's a touch screen device or the left mouse button is pressed
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            // If it's a touch screen device
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                pos = touch.position;
                rotX = touch.deltaPosition.x * speed * Time.deltaTime;
            }
            // If the left mouse button is pressed
            else if (Input.GetMouseButton(0))
            {
                pos = Input.mousePosition;
                rotX = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
            }

            // Check if the touch or click is within the rotation area
            if (rotationArea.Contains(pos))
            {
                transform.Rotate(Vector3.up, -rotX);
            }
        }
    }
}
