using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    private List<GameObject> curentTiles = new List<GameObject>();
    private List<int> lustTileIndex = new List<int>();
    [SerializeField] private GameObject[] tilePrefab;
    [SerializeField] private GameObject startTilePrefab;
    //private int lustTileIndex;
    private int randomIndex = 0;

    [SerializeField] private int maxTileCount;
    [SerializeField] private float tileSpead;
    [SerializeField] private float curentTileSpead;
    [SerializeField] private float maxTileSpead;
    [SerializeField] private float tileHeight;
    [SerializeField] private float jetPackTileSpead;
    private float saveTileSpead;
    [SerializeField] private PlayerMovment player;
    [SerializeField] private StartGame startGame;
    [SerializeField] private SpawnJetPackCoins spawnJetPackCoins;
    private bool isLose;

    void Start()
    {
        isLose = false;
        curentTileSpead = 0;
        //lustTileIndex = 0;
        if (curentTiles.Count == 0)
        {
            GameObject tile = Instantiate(startTilePrefab, Vector3.zero, Quaternion.identity);
            curentTiles.Add(tile);
        }

        for (int i = 0; i < maxTileCount - 1; i++)
        {
            CreateTile();
        }
        spawnJetPackCoins.SetSpead(jetPackTileSpead);
    }

    void Update()
    {
        foreach (GameObject tile in curentTiles)
        {
            tile.transform.position -= new Vector3(0, 0, curentTileSpead * Time.deltaTime);
        }

        if (curentTiles[0].transform.position.z < -tileHeight)
        {
            DestroyTile(curentTiles[0]);
            CreateTile();
        }
    }

    private void DestroyTile(GameObject tile)
    {
        curentTiles.Remove(tile);
        Destroy(tile);
    }

    private void CreateTile()
    {
        do
        {
            randomIndex = Random.Range(0, tilePrefab.Length);

            if (lustTileIndex.Count == 0) break;

        }
        while (CheckSpawn(randomIndex));

        if(lustTileIndex.Count < 3)
        {
            lustTileIndex.Add(randomIndex);
        }
        else
        {
            lustTileIndex.RemoveAt(0);
            lustTileIndex.Add(randomIndex);
        }

        Vector3 pos = curentTiles[curentTiles.Count - 1].transform.position + new Vector3(0, 0, tileHeight);
        GameObject tile = Instantiate(tilePrefab[randomIndex], pos, Quaternion.identity);
        curentTiles.Add(tile);
    }

    private bool CheckSpawn(int index)
    {
        for(int i = 0; i < lustTileIndex.Count; i++)
        {
            if (index == lustTileIndex[i]) return true;
        }

        return false;
    }

    public IEnumerator SpeadIncrease()
    {
        while (curentTileSpead < maxTileSpead && !isLose)
        {
            curentTileSpead+=0.5f;
            yield return new WaitForSeconds(10);
        }
    }
    public void StartMoving()
    {
         curentTileSpead = tileSpead;
         StartCoroutine(SpeadIncrease());
    }

    public void StopMoving()
    {
        curentTileSpead = 0;
        isLose = true;
    }

    public void StartJetPack()
    {
        StopCoroutine(SpeadIncrease());
        saveTileSpead = curentTileSpead;
        curentTileSpead = jetPackTileSpead;
        spawnJetPackCoins.SetSpead(curentTileSpead);
    }

    public void EndJetPack()
    {
        curentTileSpead = saveTileSpead;
        StartCoroutine(SpeadIncrease());
    }

    private void OnEnable()
    {
        player.startJetPack += StartJetPack;
        player.endJetPack += EndJetPack;
        player.lose += StopMoving;
        startGame.startMoving += StartMoving;
    }

    private void OnDisable()
    {
        player.startJetPack -= StartJetPack;
        player.endJetPack -= EndJetPack;
        player.lose -= StopMoving;
        startGame.startMoving -= StartMoving;
    }
}
