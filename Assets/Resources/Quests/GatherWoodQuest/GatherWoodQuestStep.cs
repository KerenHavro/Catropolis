using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherWoodQuestStep : QuestStep
{
    [SerializeField]
    private InventoryChecker inventoryChecker;
    private int woodCollected = 0;
    private int woodToComplete = 5;
    private bool isNear;

    private void Start()
    {
        // Find the InventoryCanvas GameObject in the scene
        GameObject inventoryCanvas = GameObject.Find("InventoryCanvas");

        if (inventoryCanvas != null)
        {
            // Get the InventoryChecker component from the InventoryCanvas GameObject
            inventoryChecker = inventoryCanvas.GetComponent<InventoryChecker>();
        }
        else
        {
            Debug.LogError("Could not find InventoryCanvas GameObject in the scene.");
        }
    }

    public void Update()
    {
        if (inventoryChecker != null)
        {
            int woodCollected = inventoryChecker.CalculateWoodCount();
            WoodCollected(woodCollected);
        }
        else
        {
            Debug.LogError("InventoryChecker is not assigned.");
        }
    }



    public void WoodCollected(int collectedAmount)
    {
        woodCollected = collectedAmount;

        if ((woodCollected >= woodToComplete))
        {
            inventoryChecker.RemoveWoodCount();
            FinishedQuestStep();
           
            UpdateState();
        }
        else
        {
            UpdateState();
        }
    }

    private void UpdateState()
    {
        string state = woodCollected.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        if (int.TryParse(state, out int parsedWoodCollected))
        {
            woodCollected = parsedWoodCollected;
        }
        else
        {
            Debug.LogError("Failed to parse wood collected state!");
        }

        UpdateState();
    }
}
