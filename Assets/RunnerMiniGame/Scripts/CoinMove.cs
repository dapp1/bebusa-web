using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    private CoinRunner coin;
    void Start()
    {
        coin = GetComponent<CoinRunner>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(coin.player.transform.position.x, coin.player.transform.position.y+1, coin.player.transform.position.z), coin.moveSpead * Time.deltaTime);
    }
}
