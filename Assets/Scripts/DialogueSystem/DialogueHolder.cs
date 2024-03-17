using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        public GameObject Board;
        private IEnumerator dialogueSeq;
        private bool dialogueFinished;
       
        private void OnEnable()
        {
           
            dialogueSeq = DialogueSequence();
            StartCoroutine(dialogueSeq);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Deactivate();
                gameObject.SetActive(false); //set active
                StopCoroutine(dialogueSeq);
            }
        }

        private IEnumerator DialogueSequence()
        {

            if (!dialogueFinished)
            {
                for (int i = 0; i < transform.childCount-1; i++)
                {
                    Deactivate();
                    GameObject currentChild = transform.GetChild(i).gameObject;
                    currentChild.SetActive(true);
                    
                    currentChild.GetComponent<DialogueLine>().PlayLine();
                    DialogueLine dialogueLine = currentChild.GetComponent<DialogueLine>();


                    yield return new WaitUntil(() => dialogueLine.finished);

                    yield return new WaitUntil(() => Input.GetMouseButton(0));

                }
                Board.gameObject.SetActive(true); 

            }
            else
            {
                Deactivate();
                int index = transform.childCount-1;
                GameObject currentChild = transform.GetChild(index).gameObject;
                 currentChild.SetActive(true);
                currentChild.GetComponent<DialogueLine>().PlayLine();
                DialogueLine dialogueLine = currentChild.GetComponent<DialogueLine>();


                yield return new WaitUntil(() => dialogueLine.finished);

                yield return new WaitUntil(() => Input.GetMouseButton(0));
           
                

            }
            dialogueFinished = true;
            gameObject.SetActive(false); //set active
            
            

        }

        private void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
                
            }
            //gameObject.SetActive(false);
        }
    }
}
