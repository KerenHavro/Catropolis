using UnityEngine;

[CreateAssetMenu(fileName = "New Mineable Item", menuName = "Mineable Item")]
public class MineableItem : ScriptableObject
{
    public string itemName;
    public GameObject collectibleItemPrefab;
    public float dropChance;
    public int maxHealth = 3; // Adjust as needed
}
