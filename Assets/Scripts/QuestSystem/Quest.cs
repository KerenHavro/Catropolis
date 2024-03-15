using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public QuestInfoSO info;
    public QuestState state;
    private int CurrentQuestStepIndex;

    public Quest(QuestInfoSO questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.CurrentQuestStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        CurrentQuestStepIndex++;
    }

    public bool CurrentStepExist()
    {
        return (CurrentQuestStepIndex < info.QuestStepPrefabs.Length);
    }
    
    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrenntQuestStepPrefab();
        if (questStepPrefab!= null)
        {
            Object.Instantiate<GameObject>(questStepPrefab, parentTransform);
        }
    }

    public GameObject GetCurrenntQuestStepPrefab()
    {


        GameObject questStepPrefab = null;
        if (CurrentStepExist())
        {
            questStepPrefab = info.QuestStepPrefabs[CurrentQuestStepIndex];
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
}
