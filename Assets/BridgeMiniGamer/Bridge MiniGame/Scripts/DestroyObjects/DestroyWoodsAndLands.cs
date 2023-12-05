using UnityEngine;
using System.Collections;

public class DestroyWoodsAndLands : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Delay());
    }

    public IEnumerator Delay()
    {
        while (gameObject != null)
        {
            yield return new WaitForSeconds(0.25f);
            if (PlayerBridgeController.Instance.transform.position.z > transform.position.z + 37f)
            {
                Destroy(gameObject);
            }
        }
    }
}
