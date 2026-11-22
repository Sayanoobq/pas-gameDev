using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image iconImage; // Gambar icon di dalam slot kotak
    private ItemData itemData;

    // Mengisi slot dengan item baru
    public void IsiSlot(ItemData itemBaru)
    {
        itemData = itemBaru;
        if (iconImage != null && itemBaru != null)
        {
            iconImage.sprite = itemBaru.iconItem;
            iconImage.enabled = true; // Munculkan gambar icon
        }
    }

    // Mengosongkan slot jika tidak ada item
    public void KosongkanSlot()
    {
        itemData = null;
        if (iconImage != null)
        {
            iconImage.sprite = null;
            iconImage.enabled = false; // Sembunyikan icon
        }
    }

    // Dipanggil saat tombol slot diklik pemain
    public void KlikSlot()
    {
        if (itemData != null && InventoryManager.Instance != null)
        {
            // Tampilkan detail item di panel kanan
            InventoryManager.Instance.TampilkanDetail(itemData);
        }
    }
}