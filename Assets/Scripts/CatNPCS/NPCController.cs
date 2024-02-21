using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private GameObject dialogue;
    public void Awake()
    {
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
