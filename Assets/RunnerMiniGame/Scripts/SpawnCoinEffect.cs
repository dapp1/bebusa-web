using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoinEffect : MonoBehaviour
{
    [SerializeField] private GameObject coinEffectPrefab;

    private GameObject coinEffect;

    public static bool isActive = false;
    public void SpawnEffect(Vector3 spawnPosition)
    {
        if (!isActive)
        {
            coinEffect = Instantiate(coinEffectPrefab, spawnPosition, Quaternion.identity);
            coinEffect.GetComponent<ParticleSystem>().Play();
            isActive = true;
        }
    }
}
