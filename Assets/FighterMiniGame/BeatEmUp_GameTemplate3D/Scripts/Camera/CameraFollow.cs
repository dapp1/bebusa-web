using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Vector3 MiddlePosition;

    [Header("Player Targets")]
    public GameObject[] targets;

    [Header("Follow Settings")]
    public float distanceToTarget = 10;
    public float heightOffset = -2;
    public float viewAngle = -6;
    public Vector3 AdditionalOffset;
    public bool FollowZAxis;

    [Header("Damp Settings")]
    public float DampX = 3f;
    public float DampY = 2f;
    public float DampZ = 3f;

    [Header("Wave Area collider")]
    public bool UseWaveAreaCollider;
    public BoxCollider CurrentAreaCollider;
    public float AreaColliderViewOffset;

    private bool firstFrameActive;

    void Start()
    {
        UpdatePlayerTargets();
        firstFrameActive = true;
    }

    void Update()
    {
        if (targets.Length > 0)
        {
            MiddlePosition = Vector3.zero;

            if (targets.Length == 1)
            {
                if (targets[0] != null) MiddlePosition = targets[0].transform.position;
            }
            else
            {
                int count = 0;
                for (int i = 0; i < targets.Length; i++)
                {
                    if (targets[i])
                    {
                        MiddlePosition += targets[i].transform.position;
                        count++;
                    }
                }
                MiddlePosition = MiddlePosition / count;
            }

            float currentX = transform.position.x;
            float currentY = transform.position.y;
            float currentZ = transform.position.z;

            currentX = Mathf.Lerp(currentX, MiddlePosition.x, DampX * Time.deltaTime);
            currentY = Mathf.Lerp(currentY, MiddlePosition.y - heightOffset, DampY * Time.deltaTime);

            if (FollowZAxis)
            {
                currentZ = Mathf.Lerp(currentZ, MiddlePosition.z + distanceToTarget, DampZ * Time.deltaTime);
            }
            else
            {
                currentZ = distanceToTarget;
            }

            if (firstFrameActive)
            {
                firstFrameActive = false;
                currentX = MiddlePosition.x;
                currentY = MiddlePosition.y - heightOffset;
                currentZ = FollowZAxis ? (MiddlePosition.z + distanceToTarget) : distanceToTarget;
            }

            if (!UseWaveAreaCollider)
            {
                transform.position = new Vector3(currentX, currentY, currentZ) + AdditionalOffset;
            }
            else
            {
                transform.position = new Vector3(currentX, currentY, currentZ) + AdditionalOffset;
            }

            transform.rotation = new Quaternion(0, 180f, viewAngle, 0);
        }
    }

    public void UpdatePlayerTargets()
    {
        targets = GameObject.FindGameObjectsWithTag("Player");
    }
}