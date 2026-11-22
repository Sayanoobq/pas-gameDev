using UnityEngine;

[CreateAssetMenu(fileName = "NewZombieConfig", menuName = "Config/ZombieConfig")]
public class ZombieConfig : ScriptableObject
{
    public int hp = 100;
    public float kecepatan = 2.5f;
    public float jarakDeteksi = 7f;
    public float jarakSerang = 1.2f;
    public int damage = 25;
}