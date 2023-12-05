
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
}
