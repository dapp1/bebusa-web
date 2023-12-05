using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelCollider : MonoBehaviour
{
    public JumperCamera camera;
    public GameObject ground;

    private JumperLevelGenerator levelGenerator;
    private JumperPlayerController playerController;
    public static int levelId = 1;
    public float speed = 0f;
    public float fixedPosition;

    private void Start()
    {
        camera = FindObjectOfType<JumperCamera>();
        playerController = FindObjectOfType<JumperPlayerController>();
        ground = GameObject.FindGameObjectWithTag("Ground");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            camera.levelIndex++;
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        camera.SetLevelConfig(levelId);
        levelGenerator = FindObjectOfType<JumperLevelGenerator>();
        levelGenerator.SpawnNextLevel();
        ground.transform.position += new Vector3(0, 15, 0);

        SetLevelConfiguration(levelId++);

        Destroy(gameObject);
    }

    private void SetLevelConfiguration(int levelId)
    {
        Debug.Log(levelId);
        switch (levelId)
        {
            case 1:
                SetPlayerConfig(false, 1, 28.8f);
                break;
            case 2:
                SetPlayerConfig(true, -1, 39);
                break;
            case 3:
                SetPlayerConfig(false, -1, -9);
                break;
            case 4:
                SetPlayerConfig(true, 1, 1);

                break;
        }
    }

    private void SetPlayerConfig(bool isHorizontalInput, int inversion, float fixedPosition)
    {
        playerController.isHorizontalInput = isHorizontalInput;
        playerController.inversion = inversion;
        playerController.fixedPosition = fixedPosition;

        // Stop any existing MovePlayer coroutines
        playerController.StopCoroutine(playerController.MovePlayer());

        // Start the MovePlayer coroutine
        playerController.StartCoroutine(playerController.MovePlayer());
    }
}