using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    [SerializeField] private GameObject dialogue;
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    public void Awake()
    {
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);

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
