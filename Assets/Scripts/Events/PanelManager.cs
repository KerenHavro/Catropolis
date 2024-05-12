using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PanelImage;
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
