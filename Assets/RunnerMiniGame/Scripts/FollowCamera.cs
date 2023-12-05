using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float cameraHeight;
    [SerializeField] private float cameraSpead;
    
    void FixedUpdate()
    {
        Vector3 pos = new Vector3(player.position.x / 2, cameraHeight + player.position.y, transform.position.z);
        Vector3 smoothPos = Vector3.Lerp(transform.position, pos, cameraSpead * Time.deltaTime);
        transform.position = smoothPos;
    }
}
