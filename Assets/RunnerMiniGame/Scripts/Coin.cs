using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
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
        if(other.TryGetComponent<PlayerMovment>(out PlayerMovment player))
        {
            player.gameObject.GetComponent<SpawnCoinEffect>().SpawnEffect(transform.position);
            //RunnerMoney.instance.PickUpCoin();
            Money.GainMoney(1);
            Destroy(gameObject);

            if (FindObjectOfType<DemoControl>() != null)
            {
                DemoControl.Instance.CoinPickup();
            }
        }
        if(other.tag == "CoinDetector")
        {
            coinMoveScript.enabled = true;
        }
    }
}
