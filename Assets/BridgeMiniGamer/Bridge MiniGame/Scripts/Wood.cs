using UnityEngine;

public class Wood : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Inventory inv = FindObjectOfType<Inventory>();

            if (UIController.Instance.WoodCount() > inv.woodsPicked.Count)
            {
                    inv.woodsPicked.Add(gameObject);
            }

            if (inv.index < inv.woodsInBoat.Count)
            {
                inv.woodsInBoat[inv.index].SetActive(true);
                inv.index++;
            }

            Destroy(gameObject);
        }
    }
}
