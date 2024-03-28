using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSlimesQuestStep : QuestStep
{

    private int slimesKilled = 0;
    private int slimesToKill = 5;
    [SerializeField]
    private GameObject Dialogue;


    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onSlimeKilled += SlimesKilled;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onSlimeKilled -= SlimesKilled;
    }

    private void SlimesKilled()
    {
        if (slimesKilled < slimesToKill)
        {
            slimesKilled++;
            UpdateState();

        }

        if (slimesKilled >= slimesToKill)
        {
            FinishedQuestStep();
            //Dialogue.SetActive(true);

}
    }

    private void UpdateState()
    {
        string state = slimesKilled.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this.slimesKilled = System.Int32.Parse(state);
        UpdateState();
    }
}


