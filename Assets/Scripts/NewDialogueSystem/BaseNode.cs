using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class BaseNode : Node
{
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

}