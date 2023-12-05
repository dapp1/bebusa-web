using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnit : MonoBehaviour
{
    [SerializeField] private float rotationSpead;
    void Update()
    {
        transform.Rotate(0, rotationSpead * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Improvements>(out Improvements improvements))
        {
            //improvements.ActiveMagnit();
            Destroy(gameObject);
        }
    }
}
