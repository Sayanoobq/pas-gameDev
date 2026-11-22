using UnityEngine;

public enum TipeItem
{
    Potion, // Bisa diminum untuk isi HP
    Coin    // Harta karun
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Detail Item")]
    public string namaItem;
    public Sprite iconItem;
    public TipeItem tipe;

    [TextArea(3, 5)]
    public string deskripsiItem;

    [Header("Nilai Item")]
    public int nilai = 25; // Nilai heal untuk potion, atau nilai emas untuk koin
}