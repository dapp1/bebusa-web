using UnityEngine;
using System.Collections;

public class JumperCamera : MonoBehaviour
{
    public GameObject player;
    public Vector3 initialPosition;
    public Vector3[] levelOffsets; // New field to hold the offsets for each level.
    public float speed = 0f;
    public float maxSpeed = 5f;
    public float speedIncreaseDuration = 10f;
    public float rotationDuration = 5.0f; // Duration of the rotation in seconds.

    private float speedIncreaseStartTime;
    private bool isRotating = false; // Added to avoid multiple calls to the coroutine.
    private Quaternion targetRotation; // The target rotation for the camera.

    public int levelIndex;

    void Start()
    {
        levelIndex = 1;
        transform.position = initialPosition;
        speedIncreaseStartTime = Time.time;
        targetRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        Debug.Log(levelIndex);
        Vector3 desiredPosition = player.transform.position + levelOffsets[levelIndex];
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed);
        transform.position = smoothedPosition;

        // Slerp rotation towards the target rotation.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed);

        if (speed < maxSpeed)
        {
            float timeSinceStart = Time.time - speedIncreaseStartTime;
            speed = Mathf.Lerp(0, maxSpeed, timeSinceStart / speedIncreaseDuration);
        }
    }

    public void SetLevelConfig(int levelId)
    {
        int levelIndex = levelId % levelOffsets.Length;
        
        if ( !isRotating)
        {
            StartCoroutine(RotateCameraToTarget(90f, rotationDuration));
            
        }
    }

    private IEnumerator RotateCameraToTarget(float rotationAngle, float duration)
    {
        isRotating = true;

        // Calculate the target rotation.
        Quaternion currentRotation = transform.rotation;
        targetRotation = currentRotation * Quaternion.Euler(0, rotationAngle, 0);

        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is exactly as specified.
        transform.rotation = targetRotation;

        isRotating = false;
    }
}
