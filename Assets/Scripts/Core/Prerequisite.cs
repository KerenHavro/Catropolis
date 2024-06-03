using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prerequisite : MonoBehaviour
{
    public enum Type { Level, Item, Quest, DialogueChoice }
    public Type type;
    public string key;
    public int value;

    public bool IsMet(PlayerData playerData)
    {
        switch (type)
        {
            case Type.Level:
                return playerData.level >= value;

            case Type.Item:
                return playerData.items.Contains(key);
            case Type.Quest:
                return playerData.completedQuests.Contains(key);
            default:
                return false;
        }
    }

}
