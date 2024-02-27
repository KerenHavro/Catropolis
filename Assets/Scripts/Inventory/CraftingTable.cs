using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    public GameObject craftingPanel; // Reference to the crafting panel

    private bool isCraftingPanelActive = false;

    void Start()
    {
        // Ensure the crafting panel is initially deactivated
        if (craftingPanel != null)
        {
            craftingPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Crafting Panel reference not set!");
        }
    }

    void Update()
    {
        // Check if the player presses the Escape key and if the crafting panel is active
        if (Input.GetKeyDown(KeyCode.Escape) && isCraftingPanelActive)
        {
            CloseCraftingPanel();
        }
    }

    void OnMouseDown()
    {
        ToggleCraftingPanel();
    }

    void ToggleCraftingPanel()
    {
        // Toggle the state of the crafting panel
        isCraftingPanelActive = !isCraftingPanelActive;

        // Activate/deactivate the crafting panel accordingly
        craftingPanel.SetActive(isCraftingPanelActive);
    }

    void CloseCraftingPanel()
    {
        // Deactivate the crafting panel
        craftingPanel.SetActive(false);
        isCraftingPanelActive = false;
    }
}
