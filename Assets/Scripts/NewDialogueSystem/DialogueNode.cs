using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueNode : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;
    public string speakerName;
    public string dialogueLine;
    public Sprite sprite;
    public float delayBetweenLines;
    [SerializeField]
    private AudioClip[] sound;

    public List<Prerequisite> prerequisite;

    public override bool GetArePrerequisiteMet(PlayerData playerData)
    {
        foreach (var prerequisite in prerequisites)
        {
            if (!prerequisite.IsMet(playerData))
            {
                return false;
            }
        }
        return true;

    }
    public override string GetString()
    {
        return "DialogueNode/" + speakerName + "/" + dialogueLine;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }
    public override float GetFloat()
    {
        return delayBetweenLines;
    }

    public override AudioClip[] GetAudioClips()
    {
        return sound;
    }
}