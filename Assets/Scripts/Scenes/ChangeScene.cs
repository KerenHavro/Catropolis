using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string NPCHouse; // Name of the scene to load
    public bool loadAsync = false; // Whether to load the scene asynchronously
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // If loadAsync is false, load the scene synchronously
            if (!loadAsync)
            {
                SceneManager.LoadScene(NPCHouse);
            }
            // If loadAsync is true, load the scene asynchronously
            else
            {
                StartCoroutine(LoadSceneAsync());
            }
        }
    }

    // Coroutine to load the scene asynchronously
    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(NPCHouse);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}