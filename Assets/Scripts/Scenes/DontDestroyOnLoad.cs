using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        // Ensure that this GameObject persists across scene changes
        DontDestroyOnLoad(this.gameObject);
    }
}
