using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    [SerializeField] private GameObject dialogue;
    [SerializeField] private GameObject Board;

    public void Awake()
    {
        Board.gameObject.SetActive(false);
  

        dialogue.SetActive(false);
    }
    public void ActivateDialogue()
    {
        dialogue.SetActive(true);
    }

    public bool DialogueActive()
    {
        return dialogue.activeInHierarchy;
    }
}
