using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueEventManager dialogueManager = FindObjectOfType<DialogueEventManager>();
            if (dialogueManager != null)
            {
                dialogueManager.ActivatePanel();
            }
            else
            {
                Debug.LogError("No DialogueEventManager found in the scene.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueEventManager dialogueManager = FindObjectOfType<DialogueEventManager>();
            if (dialogueManager != null)
            {
                Debug.Log("no dialogue.");
                dialogueManager.DeactivatePanel();
            }
            else
            {
                Debug.Log("No DialogueEventManager found in the scene.");
            }
        }
    }
}
