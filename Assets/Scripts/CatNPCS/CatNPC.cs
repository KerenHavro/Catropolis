using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CatNPC : MonoBehaviour
{
    // Common properties for all cat NPCs
    public string catName;
    public string[] dialogueLines;

    // State enum for NPC behavior
    public enum NPCState
    {
        Idle,
        Walking,
        Talking,
        Selling
        // Add more states as needed
    }
    protected NPCState currentState;

    // Constructor method
    public CatNPC(string name, string[] dialogues)
    {
        catName = name;
        dialogueLines = dialogues;
        currentState = NPCState.Idle;
    }

    // Common methods for all cat NPCs
    public virtual void Move()
    {
        // Implement movement behavior for cat NPCs
    }

    public virtual void StartDialogue()
    {
        currentState = NPCState.Talking;
        // Display dialogue UI and text
    }

    public virtual void EndDialogue()
    {
        currentState = NPCState.Idle;
        // Hide dialogue UI
    }

    public virtual void SellItems()
    {
        currentState = NPCState.Selling;
        // Display shop UI with items available for purchase
    }

    // Abstract method for interacting with NPC
    public abstract void Interact();
}
