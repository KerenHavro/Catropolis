using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    [SerializeField] private GameObject dialogue;
    [SerializeField] private GameObject Board;
    [SerializeField] private GameObject patrolDestination;
    [SerializeField] private int patrolSpeed = 10;
    public bool walkable= false;
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

    public void Update()
    {
        if(walkable)
        transform.position = Vector2.MoveTowards(transform.position, patrolDestination.transform.position, patrolSpeed * Time.deltaTime);
    }
        public void canwalk()
        {
        walkable = true;
        }

    
}

