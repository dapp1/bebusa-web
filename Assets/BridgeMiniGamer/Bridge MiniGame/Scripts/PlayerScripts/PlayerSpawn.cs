using Unity.Netcode;
using UnityEngine;

namespace Bridge
{
    public class PlayerSpawn : MonoBehaviour
    {
        public static PlayerSpawn Instance;

        [Header("Spawn Player Settings")]
        [SerializeField] private GameObject boatPrefab;
        [SerializeField] private float _moveSpeedForward;
        [SerializeField] private float _moveSpeedSide;

        [Header("Acceleration per complete")]
        [SerializeField] private AnimationCurve _accelerationForward;
        [SerializeField] private AnimationCurve _accelerationSide;

        [SerializeField] private GameObject[] _skins;
        private GameObject defaultPlayerPrefab;

        private void Awake()
        {
            Instance = this;
            PlayerPrefs.SetInt("Skin", 0);
            ChangeSkin();
            LoadPlayer(defaultPlayerPrefab);
        }

        void LoadPlayer(GameObject playerPrefab)
        {
            GameObject player = Instantiate(boatPrefab);
            player.transform.position = transform.position;
            GameObject skin = Instantiate(playerPrefab);

            skin.transform.position = transform.position + new Vector3(0, 0.879f, 0);

            skin.transform.SetParent(PlayerBridgeController.Instance.models.transform);
        }

        public void SetAcceleration(int spawnNumber)
        {
            PlayerBridgeController.Instance.SetNewSpeed(
                Mathf.Floor(_accelerationForward.Evaluate(spawnNumber)), Mathf.Floor(_accelerationSide.Evaluate(spawnNumber)));
        }        
    

        private void ChangeSkin()
        {
            SetSkin(PlayerPrefs.GetInt("Skin"));
        }

        public void SetSkin(int index)
        {
            for (int i = 0; i < _skins.Length; i++)
            {
                if (i == index)
                {
                    defaultPlayerPrefab = _skins[i];
                }
            }
        }
    }
}