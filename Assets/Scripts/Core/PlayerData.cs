using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int level;
    public HashSet<string> items;
    public HashSet<string> completedQuests;
    public HashSet<string> dialogueChoicesMade;

    public PlayerData()
    {
        items = new HashSet<string>();
        completedQuests = new HashSet<string>();
        dialogueChoicesMade = new HashSet<string>();

    }

}
