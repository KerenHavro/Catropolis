using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mineable Item", menuName = "Mineable Item")]
public class MineableItem: ScriptableObject
{
    public Sprite sprite;
    public int health;
    public ItemSO[] dropItems;
}
