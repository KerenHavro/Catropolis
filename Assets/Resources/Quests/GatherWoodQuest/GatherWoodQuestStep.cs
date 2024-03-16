using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherWoodQuestStep : QuestStep
{
 
    private int woodCollected = 0;
    private int woodToComplete = 5;

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onWoodCollected += WoodCollected;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onWoodCollected -= WoodCollected;
    }

    private void WoodCollected(int collectedAmount)
    {
        woodCollected = collectedAmount;

        if (woodCollected >= woodToComplete)
        {
            FinishedQuestStep();
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
