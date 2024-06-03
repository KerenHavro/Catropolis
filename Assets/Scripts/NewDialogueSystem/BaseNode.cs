using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public abstract class BaseNode : Node
{
    public List<string> prerequisites; // List of prerequisites as strings

    // Checks if all prerequisites are met
    public bool ArePrerequisitesMet(PlayerData playerData)
    {
        foreach (var prerequisite in prerequisites)
        {
            if (!IsPrerequisiteMet(playerData, prerequisite))
            {
                return false;
            }
        }
        return true;
    }

    private bool IsPrerequisiteMet(PlayerData playerData, string prerequisite)
    {
        string[] parts = prerequisite.Split(':');
        if (parts.Length != 2) return false; // Malformed prerequisite string

        string type = parts[0];
        string value = parts[1];

        switch (type)
        {
            case "Level":
                return playerData.level >= int.Parse(value);
            case "Item":
                return playerData.items.Contains(value);
            case "Quest":
                return playerData.completedQuests.Contains(value);
            case "DialogueChoice":
                return playerData.dialogueChoicesMade.Contains(value);
            default:
                return false; // Unknown prerequisite type
        }
    }

    public virtual string GetString()
    {
        return null;
    }

    public virtual Sprite GetSprite()
    {
        return null;
    }

    public virtual float GetFloat()
    {
        return 0;
    }

    public virtual AudioClip[] GetAudioClips()
    {
        return null;
    }

    public virtual Button[] Getbutton()
    {
        return null;
    }
}
