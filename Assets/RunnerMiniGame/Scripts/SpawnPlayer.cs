using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    private int curentSkinIndex;
    [SerializeField] private List<GameObject> playerSkin = new List<GameObject>();
    void Awake()
    {
        curentSkinIndex = PlayerPrefs.GetInt("Skin");
        if (curentSkinIndex > playerSkin.Count - 1)
        {
            GameObject actiweSkin = Instantiate(playerSkin[0], transform.position, Quaternion.identity);
            actiweSkin.transform.SetParent(transform);
        }
        else
        {
            GameObject actiweSkin = Instantiate(playerSkin[curentSkinIndex], transform.position, Quaternion.identity);
            actiweSkin.transform.SetParent(transform);
        }
    }

    

}
