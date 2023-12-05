using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffect : MonoBehaviour
{
    [SerializeField] private float effectLifeTime;

    private float timer;
    void Start()
    {
        timer = 0;
    }

    void Update()
    {
        if (timer < effectLifeTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnCoinEffect.isActive = false;
            Destroy(gameObject);
        }
    }
}
