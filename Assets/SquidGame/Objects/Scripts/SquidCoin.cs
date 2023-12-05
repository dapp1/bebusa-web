using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidCoin : MonoBehaviour
{
    public Transform player;
    public float moveSpead;
    [SerializeField] private float rotationSpead;

    private CoinMove coinMoveScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        coinMoveScript = gameObject.GetComponent<CoinMove>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpead * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DemoControl.Instance.CoinPickup();
            Destroy(gameObject);
        }
        if (other.tag == "CoinDetector")
        {
            coinMoveScript.enabled = true;
        }
    }
}
