using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Daftar Item di Tas")]
    public List<ItemData> daftarItem = new List<ItemData>();
    public InventorySlot[] semuaSlot; // Kotak-kotak item di kiri

    [Header("UI Inventory")]
    public GameObject inventoryPanel; // Kotak tas utama

    [Header("UI Detail Item (Sebelah Kanan)")]
    public GameObject detailPanel;
    public Image detailIcon;
    public TextMeshProUGUI detailNamaText;
    public TextMeshProUGUI detailDeskripsiText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Tas dan panel detail tertutup di awal game
        if (inventoryPanel != null) inventoryPanel.SetActive(false);
        if (detailPanel != null) detailPanel.SetActive(false);

        UpdateUI();
    }

    void Update()
    {
        // Buka / Tutup Tas dengan tombol "I"
        if (Keyboard.current != null && Keyboard.current.iKey.wasPressedThisFrame)
        {
            BukaTutupInventory();
        }
    }

    public void BukaTutupInventory()
    {
        if (inventoryPanel != null)
        {
            bool status = !inventoryPanel.activeSelf;
            inventoryPanel.SetActive(status);

            if (!status && detailPanel != null)
            {
                detailPanel.SetActive(false);
            }
        }
    }

    // Memasukkan item ke tas saat dipungut dari lantai
    public bool TambahItem(ItemData itemBaru)
{
    // Jika belum ada slot yang di‑assign, lewati pengecekan panjang array
    int capacity = (semuaSlot != null && semuaSlot.Length > 0) ? semuaSlot.Length : 9999;

    if (daftarItem.Count >= capacity)
    {
        Debug.Log("Tas Penuh!");
        return false;
    }

    daftarItem.Add(itemBaru);
    UpdateUI();
    return true;
}

    // Menampilkan gambar item di kotak slot
    public void UpdateUI()
    {
        for (int i = 0; i < semuaSlot.Length; i++)
        {
            if (i < daftarItem.Count)
            {
                semuaSlot[i].IsiSlot(daftarItem[i]);
            }
            else
            {
                semuaSlot[i].KosongkanSlot();
            }
        }
    }

    // KETENTUAN SOAL: Menampilkan detail item ketika kotak diklik!
    public void TampilkanDetail(ItemData item)
    {
        if (detailPanel != null) detailPanel.SetActive(true);
        if (detailIcon != null) detailIcon.sprite = item.iconItem;
        if (detailNamaText != null) detailNamaText.text = item.namaItem;
        if (detailDeskripsiText != null) detailDeskripsiText.text = item.deskripsiItem;
    }
}