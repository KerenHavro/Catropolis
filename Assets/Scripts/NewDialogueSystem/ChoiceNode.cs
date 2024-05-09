using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class ChoiceNode : BaseNode
{
    [Input] public int entry;
    [Output(dynamicPortList = true)] public List<string> choices = new List<string>();
    public string question;
    public Sprite sprite;
    public float delayBetweenLines;
    [SerializeField]
    private AudioClip[] sound;

    public override string GetString()
    {
        return "ChoiceNode/" + question;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }

    // This method will return the list of buttons for the choices
    public override Button[] Getbutton()
    {
        Button[] buttons = new Button[choices.Count];
        for (int i = 0; i < choices.Count; i++)
        {
            // Here, you can instantiate or assign UI buttons representing each choice
        }
        return buttons;
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
