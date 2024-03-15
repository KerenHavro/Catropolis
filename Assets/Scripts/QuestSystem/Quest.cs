using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public QuestInfoSO info;
    public QuestState state;
    private int currentQuestStepIndex;
    private QuestStepState[] questStepStates;

    public Quest(QuestInfoSO questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
        this.questStepStates = new QuestStepState[info.questStepPrefabs.Length];
        for (int i = 0; i < questStepStates.Length; i++)
        {
            questStepStates[i] = new QuestStepState();
        }
    }

    public Quest(QuestInfoSO questInfo, QuestState questState, int currentQuestStepIndex, QuestStepState[] questStepStates)
    {
        this.info = questInfo;
        this.state = questState;
        this.currentQuestStepIndex = currentQuestStepIndex;
        this.questStepStates = questStepStates;

        if(this.questStepStates.Length!= this.info.questStepPrefabs.Length)
        {

            Debug.LogWarning("Quest Step Prefabs and Quest Step States are "
                + "of different lengths. This indicates something changed "
                + "with the QuestInfo and the saved data is now out of sync. "
                + "Reset your data - as this might cause issues. QuestId: " + this.info.id);
        }
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExist()
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length);
    }
    
    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrenntQuestStepPrefab();
        if (questStepPrefab!= null)
        {
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>();
            questStep.InitializeQuestStep(info.id, currentQuestStepIndex, questStepStates[currentQuestStepIndex].state);
        }
    }

    public GameObject GetCurrenntQuestStepPrefab()
    {


        GameObject questStepPrefab = null;
        if (CurrentStepExist())
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else
        {
        
                Debug.LogWarning("Quest Step Prefabs and Quest Step States are "
                    + "of different lengths. This indicates something changed "
                    + "with the QuestInfo and the saved data is now out of sync. "
                    + "Reset your data - as this might cause issues. QuestId: " + this.info.id);
            
        }
        return questStepPrefab;
    }

    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
    {
        if(stepIndex< questStepStates.Length)
        {
            questStepStates[stepIndex].state = questStepState.state;
        }
        else
        {
            Debug.LogWarning("tried");
        }
    }
    public QuestData GetQuestData()
    {
        return new QuestData(state, currentQuestStepIndex, questStepStates);
    }
}
