using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventManager : MonoBehaviour
{
    public bool DialoguePanelActive;

    public delegate void DialogueEvent();
    public static event DialogueEvent PanelActivated;
    public static event DialogueEvent PanelDeactivated;

    public void ActivatePanel()
    {
        DialoguePanelActive = true;
        // Trigger the PanelActivated event
        PanelActivated?.Invoke();
    }

    // Method to deactivate the dialogue panel
    public void DeactivatePanel()
    {
        DialoguePanelActive = false;
        // Trigger the PanelDeactivated event
        PanelDeactivated?.Invoke();
    }
}
