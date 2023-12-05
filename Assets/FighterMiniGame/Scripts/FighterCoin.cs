using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FighterCoin : MonoBehaviour
{

    public int money = 1;
    public string pickupSFX = "";
    public GameObject pickupEffect;
    public float pickUpRange = 1;
    private GameObject[] Players;

    void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        LayerMask itemLayer = LayerMask.GetMask("Item");
        LayerMask enemyLayer = LayerMask.GetMask("Enemy");

    }

    void LateUpdate()
    {
        transform.RotateAround(transform.position, Vector3.up, 100 * Time.deltaTime);

        foreach (GameObject player in Players)
        {
            if (player)
            {
                float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

                //player is in pickup range
                if (distanceToPlayer < pickUpRange)
                    AddMoney();
            }
        }
    }

    //add health to player
    void AddMoney()
    {
        DemoControl.Instance.CoinPickup();

        //show pickup effect
        if (pickupEffect != null)
        {
            GameObject effect = GameObject.Instantiate(pickupEffect);
            effect.transform.position = transform.position;
        }

        //play sfx
        if (pickupSFX != "")
        {
            GlobalAudioPlayer.PlaySFXAtPosition(pickupSFX, transform.position);
        }

        Destroy(gameObject);
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        //Show pickup range
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickUpRange);

    }
#endif
}
