using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateThisAfterQuest : MonoBehaviour
{

    [Header("Icons")]
    [SerializeField] public GameObject canBeFinished;

    [SerializeField] public GameObject finished;


    public void SetState(QuestState newState, bool startPoint, bool finishPoint)
    {


        // set the appropriate one to active based on the new state
        switch (newState)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
                
                break;
            case QuestState.CAN_START:
             
                break;
            case QuestState.IN_PROGRESS:
            
                break;
            case QuestState.CAN_FINISH:
                finished.SetActive(true);
                Debug.Log("finished");

                break;
            case QuestState.FINISHED:
                finished.SetActive(true);
                this.gameObject.SetActive(false);
                break;
            default:
                Debug.LogWarning("Quest State not recognized by switch statement for quest icon: " + newState);
                break;
        }
    }
}

