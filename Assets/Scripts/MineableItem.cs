using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Mineable Item", menuName = "Mineable Item")]
public class MineableItem : ScriptableObject
{
    [Serializable]
    public class CollectibleItemDrop
    {
        public GameObject collectibleItemPrefab;
        public float dropChance;
    }

    public string itemName;
    public CollectibleItemDrop[] collectibleItems;
    public int maxHealth = 3; // Adjust as needed
}
