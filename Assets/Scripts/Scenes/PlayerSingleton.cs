using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    private static PlayerSingleton _instance;

    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy this GameObject
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set this instance as the singleton instance
        _instance = this;

        // Ensure that this GameObject persists across scene changes
        DontDestroyOnLoad(gameObject);
    }
}
