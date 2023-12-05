using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{

    private GameObject defaultPlayerPrefab;
    [SerializeField] private GameObject[] _skins;
    [SerializeField] private FighterLevelGenerator fighterLevelGenerator;


    void Awake()
    {
        ChangeSkin();
        //get selected player from character selection screen
        if (GlobalGameSettings.Player1Prefab)
        {
            loadPlayer(GlobalGameSettings.Player1Prefab);
            return;
        }

        //otherwise load default character
        if (defaultPlayerPrefab)
        {
            loadPlayer(defaultPlayerPrefab);
        }
        else
        {
            Debug.Log("Please assign a default player prefab in the  playerSpawnPoint");
        }

    }

    //load a player prefab
    void loadPlayer(GameObject playerPrefab)
    {
        GameObject player = GameObject.Instantiate(playerPrefab) as GameObject;
        player.transform.position = transform.position;
    }

    private void ChangeSkin()
    {
                SetSkin(0);
    }
    public void SetSkin(int index)
    {
        for (int i = 0; i < _skins.Length; i++)
        {
            if (i == index)
            {
                defaultPlayerPrefab = _skins[i];
                fighterLevelGenerator.playerTransform = _skins[i].transform;
            }

        }
    }
}