using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventManager : MonoBehaviour
{
    public bool DialoguePanelActive;
    [SerializeField]
    private GameObject PanelImage;
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


    private void OnEnable()
    {
        DialogueEventManager.PanelActivated += OnPanelActivated;
        DialogueEventManager.PanelDeactivated += OnPanelDeactivated;
    }

    private void OnDisable()
    {
        DialogueEventManager.PanelActivated -= OnPanelActivated;
        DialogueEventManager.PanelDeactivated -= OnPanelDeactivated;
    }

    private void OnPanelActivated()
    {
        PanelImage.SetActive(true);
    }

    public void OnPanelDeactivated()
    {
        PanelImage.SetActive(false);
    }
}
