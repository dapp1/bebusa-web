using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 1f;
    public float zoomSpeed = 1f;
    public float rotationSensitivity = 0.1f;
    public float zoomSensitivity = 0.1f;
    public Vector3 cameraOffset = new Vector3(0, 5, -10);
    public float minYAngle = -80f;
    public float maxYAngle = 80f;

    private Vector2 lastTouchPosition;
    private bool isRotating = false;

    private int touchCount
    {
        get
        {
            if (Input.GetButton("Fire1"))
            {
                return 1;
            }

            return Input.touchCount;
        }
    }

    private void Start()
    {
        Swipe.OnMoved += HandleCameraRotation;
        Swipe.OnTwoTouchMoved += HandleCameraZoom;

        // Загрузка значений из PlayerPrefs
        if (PlayerPrefs.HasKey("CameraZoom"))
        {
            GetComponent<Camera>().fieldOfView = PlayerPrefs.GetFloat("CameraZoom");
        }
        if (PlayerPrefs.HasKey("CameraRotationY"))
        {
            var yRotation = PlayerPrefs.GetFloat("CameraRotationY");
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
        }
    }

    private void OnDestroy()
    {
        Swipe.OnMoved -= HandleCameraRotation;
        Swipe.OnTwoTouchMoved -= HandleCameraZoom;
    }

    // private void Update()
    // {
    //     if (touchCount == 2 && !FloatingJoystick.IsBeingUsed)
    //     {
    //         if (FloatingJoystick.IsBeingUsed)
    //         {
    //             HandleCameraRotation(Input.GetTouch(1));
    //         }
    //         else
    //         {
    //             // HandleCameraZoom(Input.GetTouch(0), Input.GetTouch(1));
    //         }
    //     }
    // }

    private void LateUpdate()
    {
        transform.position = player.position + transform.TransformDirection(cameraOffset);
    }

    private void HandleCameraRotation(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                // Debug.Log("TouchPhase.Began");
                lastTouchPosition = touch.position;
                isRotating = true;
                break;

            case TouchPhase.Moved:
                // Debug.Log("TouchPhase.Moved");
                if (isRotating)
                {
                    // Debug.Log("isRotating");
                    Vector2 delta = touch.position - lastTouchPosition;
                    transform.RotateAround(player.position, Vector3.up, delta.x * rotationSpeed * rotationSensitivity * Time.deltaTime);

                    float newRotationY = transform.eulerAngles.x - delta.y * rotationSpeed * rotationSensitivity * Time.deltaTime;
                    newRotationY = Mathf.Clamp(newRotationY, minYAngle, maxYAngle);
                    transform.eulerAngles = new Vector3(newRotationY, transform.eulerAngles.y, transform.eulerAngles.z);

                    lastTouchPosition = touch.position;

                    // Сохранение угла поворота по оси Y
                    PlayerPrefs.SetFloat("CameraRotationY", transform.eulerAngles.y);
                }
                break;

            case TouchPhase.Ended:
                // Debug.Log("TouchPhase.Ended");
                isRotating = false;
                break;
        }

    }

    private void HandleCameraZoom(Touch firstTouch, Touch secondTouch)
    {
        Vector2 firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
        Vector2 secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

        float prevTouchDeltaMag = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
        float touchDeltaMag = (firstTouch.position - secondTouch.position).magnitude;

        float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
        Debug.Log(deltaMagnitudeDiff);
        ZoomCamera(deltaMagnitudeDiff);
    }

    private void ZoomCamera(float deltaMagnitudeDiff)
    {
        Camera camera = GetComponent<Camera>();
        float newFOV = camera.fieldOfView + deltaMagnitudeDiff * zoomSpeed * zoomSensitivity;
        newFOV = Mathf.Clamp(newFOV, 40f, 90f);

        camera.fieldOfView = newFOV;

        // Сохранение зума камеры
        PlayerPrefs.SetFloat("CameraZoom", newFOV);
    }
}
