using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Header("Data Item")]
    public ItemData itemData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (InventoryManager.Instance != null && itemData != null)
            {
                bool berhasil = InventoryManager.Instance.TambahItem(itemData);

                if (berhasil)
                {
                    Debug.Log("Item diambil: " + itemData.namaItem);
                    Destroy(gameObject);
                }
            }
        }
    }
}