using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished;

    protected void FinishedQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            Destroy(this.gameObject);
        }
    }
   
}
