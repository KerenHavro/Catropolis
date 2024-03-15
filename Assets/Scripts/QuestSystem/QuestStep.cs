using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished;
    private string questId;
    private int stepIndex;

    public void InitializeQuestStep(string questId, int stepIndex, string questStepState)
    {
        this.questId = questId;
        this.stepIndex = stepIndex;
        if(questStepState!= null && questStepState != "")
        {
            SetQuestStepState(questStepState);
        }
    }


    protected void FinishedQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            GameEventsManager.instance.questEvents.AdvanceQuest(questId);
            Destroy(this.gameObject);
        }
    }

    protected void ChangeState(string newState)
    {
        GameEventsManager.instance.questEvents.QuestStepStateChange(questId, stepIndex, new QuestStepState(newState));
    }

    protected abstract void SetQuestStepState(string state);
}
