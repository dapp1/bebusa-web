using UnityEngine;

public class JumperCoin : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    [SerializeField] private GameObject pickUpEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Money.GainMoney(coinValue);
            GameObject effect = GameObject.Instantiate(pickUpEffect);
            effect.transform.position = transform.position;
            Destroy(this.gameObject);

            if (FindObjectOfType<DemoControl>() != null)
            {
                DemoControl.Instance.CoinPickup();
            }
        }
    }
    private void LateUpdate()
    {
        transform.RotateAround(transform.position, Vector3.up, 100 * Time.deltaTime);
    }
}
