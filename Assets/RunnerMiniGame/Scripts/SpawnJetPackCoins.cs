using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnJetPackCoins : MonoBehaviour
{
    private List<GameObject> curentCoins = new List<GameObject>();
    [SerializeField] private GameObject coinsPrefab;
    [SerializeField] private float coinsSpead;
    [SerializeField] private float zOffset;

    private bool isJetPack = false;
    private float coinHaight;
    private float lineOffset;

    void Update()
    {
        if (isJetPack)
        {
            if (transform.position.z - curentCoins[curentCoins.Count - 1].transform.position.z > 8)
            {
                CreateCoins();
            }
        }

        if (curentCoins.Count > 0)
        {
            if (curentCoins[0].transform.position.z < -15)
            {
                DestroyCoins(curentCoins[0]);
            }
        }
    }

    private void FixedUpdate()
    {
        if (curentCoins.Count > 0)
        {
            foreach (GameObject coins in curentCoins)
            {
                coins.transform.position -= new Vector3(0, 0, coinsSpead * Time.deltaTime);
            }
        }
    }

    private void DestroyCoins(GameObject coins)
    {
        curentCoins.Remove(coins);
        Destroy(coins);
    }

    private int oneLineCoinsCount = 0;
    private float randomNumber;
    private void CreateCoins()
    {
        if (oneLineCoinsCount == 0)
        {
            randomNumber = Random.Range(0, 3);
        }
        Vector3 pos = Vector3.zero;


        switch (randomNumber)
        {
            case 0:
                pos = new Vector3(-lineOffset, transform.position.y, transform.position.z);
                break;
            case 1:
                pos = new Vector3(0, transform.position.y, transform.position.z);
                break;
            case 2:
                pos = new Vector3(lineOffset, transform.position.y, transform.position.z);
                break;
        }

        GameObject tile = Instantiate(coinsPrefab, pos, Quaternion.identity);
        curentCoins.Add(tile);
        oneLineCoinsCount++;

        if (oneLineCoinsCount == 5)
        {
            oneLineCoinsCount = 0;
        }
    }
    public void SpawnCoins()
    {
        transform.position = new Vector3(0, coinHaight + 1, zOffset);
        CreateCoins();
        isJetPack = true;
    }

    public void StopSpawn()
    {
        isJetPack = false;
    }

    public void SetSpead(float spead)
    {
        coinsSpead = spead;
    }

    public void SetHaight(float haight)
    {
        coinHaight = haight;
    }

    public void SetLineOffset(float offset)
    {
        lineOffset = offset;
    }
}
