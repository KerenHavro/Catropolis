using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using XNode;

public class NodeParser : MonoBehaviour
{
    public Prerequisite prerequisite;
    public PlayerData playerData;
    public DialogueGraph graph;
    private Coroutine _parser;

    // Use TextMeshPro instead of Unity's built-in UI Text
    public GameObject DialogueHolder;
    public TMP_Text speaker;
    public TMP_Text dialogue;
    public Image speakerImage;

    public GameObject choiceButtonPrefab; // Button prefab for choices
    public Transform choiceButtonParent;  // Parent for choice buttons

    public float delayBetweenLines;
    public AudioClip[] sounds;

    private BaseNode lastNode; // Store the last node accessed
    private void OnEnable()
    {
        DialogueEventManager.PanelActivated += StartingPoint;
        DialogueEventManager.PanelDeactivated += CloseDialogue;
    }

    private void OnDisable()
    {
        DialogueEventManager.PanelActivated -= StartingPoint;
        DialogueEventManager.PanelDeactivated -= CloseDialogue;
    }
    private void StartingPoint()
    {
        if (lastNode != null)
        {
            graph.current = lastNode;
        }
        else
        {
            // Find the "Start" node and set it as the current node
            foreach (BaseNode b in graph.nodes)
            {
                if (b.GetString() == "Start")
                {
                    graph.current = b;
                    break;
                }

            }
        }
        // Start the node parsing coroutine
        _parser = StartCoroutine(ParseNode());
    }


    private IEnumerator ParseNode()
    {


        BaseNode b = graph.current;
        if (b == null || !b.ArePrerequisitesMet(playerData))
        {
            yield break; // Exit if there's no current node to parse
        }

        string data = b.GetString();
        string[] dataParts = data.Split('/');

        if (dataParts.Length == 0)
        {
            yield break; // Avoid processing empty strings
        }

        // Start node logic
        if (dataParts[0] == "Start")
        {
            //yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            NextNode("exit");
        }
        // Dialogue node logic
        else if (dataParts[0] == "DialogueNode" && dataParts.Length >= 3)
        {
            // Set speaker and dialogue texts
            speaker.text = dataParts[1];
            dialogue.text = "";

            // Set speaker image if available
            Sprite speakerSprite = b.GetSprite();
            if (speakerSprite != null)
            {
                speakerImage.sprite = speakerSprite;
            }

            float delayBetweenLines = b.GetFloat();
            if (delayBetweenLines != 0)
            {
                this.delayBetweenLines = delayBetweenLines;
            }

            AudioClip[] sound = b.GetAudioClips();
            if (sound != null)
            {
                sounds = sound;
            }

            for (int i = 0; i < dataParts[2].Length; i++)
            {
                dialogue.text += dataParts[2][i];

                // Play a random sound from the array of sounds
                if (sounds.Length > 0)
                {
                    int randomIndex = Random.Range(0, sounds.Length);
                    SoundManager.instance.PlaySound(sounds[randomIndex]);
                }

                yield return new WaitForSeconds(delayBetweenLines);
            }

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            dialogue.text = null;
            lastNode = graph.current;
            NextNode("exit");
        }
        // Choice node logic
        else if (b is ChoiceNode choiceNode)
        {
            // Set speaker and dialogue texts
            speaker.text = dataParts[1];
            dialogue.text = "";

            // Set speaker image if available
            Sprite speakerSprite = b.GetSprite();
            if (speakerSprite != null)
            {
                speakerImage.sprite = speakerSprite;
            }

            float delayBetweenLines = b.GetFloat();
            if (delayBetweenLines != 0)
            {
                this.delayBetweenLines = delayBetweenLines;
            }

            AudioClip[] sound = b.GetAudioClips();
            if (sound != null)
            {
                sounds = sound;
            }

            for (int i = 0; i < choiceNode.GetString().Split('/')[1].Length; i++)
            {
                dialogue.text += choiceNode.GetString().Split('/')[1][i];

                // Play a random sound from the array of sounds
                if (sounds.Length > 0)
                {
                    int randomIndex = Random.Range(0, sounds.Length);
                    SoundManager.instance.PlaySound(sounds[randomIndex]);
                }

                yield return new WaitForSeconds(delayBetweenLines);
            }



            // Clear any previous choice buttons
            foreach (Transform child in choiceButtonParent)
            {
                Destroy(child.gameObject);
            }

            // Create choice buttons
            for (int i = 0; i < choiceNode.choices.Count; i++)
            {
                GameObject button = Instantiate(choiceButtonPrefab, choiceButtonParent);
                button.GetComponentInChildren<TMP_Text>().text = choiceNode.choices[i];
                int choiceIndex = i; // Capture index for the button click event
                button.GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(choiceNode, choiceIndex));
                button.GetComponent<Button>().onClick.AddListener(() =>
                {
                    dialogue.text = null;
                    foreach (Transform child in choiceButtonParent)
                    {
                        Destroy(child.gameObject);
                    }
                });
            }

            //yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        }
        else if (dataParts[0] == "End")
        {

            CloseDialogue();

            //---------CLOSE DIALOGUE----------//
        }
    }
    public void CloseDialogue()
    {
        DialogueHolder.SetActive(false);
        
        foreach (Transform child in choiceButtonParent)
        {
            Destroy(child.gameObject);
        }
    }
    public void NextNode(string fieldName)
    {
        // Stop the currently running parsing coroutine
        if (_parser != null)
        {
            StopCoroutine(_parser);
            _parser = null;
        }

        // Look for the port that matches the specified field name
        foreach (NodePort p in graph.current.Ports)
        {
            if (p.fieldName == fieldName && p.Connection != null && p.Connection.node != null)
            {
                graph.current = p.Connection.node as BaseNode;
                break;
            }
        }

        // Restart the parsing coroutine
        _parser = StartCoroutine(ParseNode());
    }

    // Handle choice selection
    void OnChoiceSelected(ChoiceNode node, int index)
    {
        NodePort port = node.GetOutputPort("choices " + index);
        graph.current = port.Connection?.node as BaseNode;
        // Restart the node parser
        _parser = StartCoroutine(ParseNode());
    }
}
