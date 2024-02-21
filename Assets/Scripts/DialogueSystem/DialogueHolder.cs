using System.Collections;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(DialogueSequence());
        }

        private IEnumerator DialogueSequence()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Deactivate();
                GameObject currentChild = transform.GetChild(i).gameObject;
                currentChild.SetActive(true);

                DialogueLine dialogueLine = currentChild.GetComponent<DialogueLine>();
                
                if (dialogueLine != null)
                {
                    yield return new WaitUntil(() => dialogueLine.finished);
                }
                yield return new WaitUntil(() => Input.GetMouseButton(0));
            }
            

            gameObject.SetActive(false);

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
