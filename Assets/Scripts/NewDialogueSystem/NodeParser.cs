using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using XNode;

public class NodeParser : MonoBehaviour
{
    public DialogueGraph graph;
    private Coroutine _parser;
 

    // Use TextMeshPro instead of Unity's built-in UI Text
    public TMP_Text speaker;
    public TMP_Text dialogue;
    public Image speakerImage;

    public float delaybetweenLines;
    public AudioClip[] sounds;

    private void Start()
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

        // Start the node parsing coroutine
        _parser = StartCoroutine(ParseNode());
    }

    private IEnumerator ParseNode()
    {
        BaseNode b = graph.current;
        if (b == null)
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
            NextNode("exit");
        }
        // Dialogue node logic
        else if (dataParts[0] == "DialogueNode" && dataParts.Length >= 3)
        {
            // Set speaker and dialogue texts
            speaker.text = dataParts[1];
          

            // Set speaker image if available
            Sprite speakerSprite = b.GetSprite();
            if (speakerSprite != null)
            {
                speakerImage.sprite = speakerSprite;
            }

            float delayBetweenLines = b.GetFloat();
            if (delayBetweenLines != 0)
            {
                delaybetweenLines = delayBetweenLines;
            }

            AudioClip[] sound = b.GetAudioClips();
            if (sound != null)
            {
                sounds = sound;
            }

      

            print("enter");
            for (int i = 0; i < dataParts[2].Length; i++)
            {
                dialogue.text += dataParts[2][i];

                // Play a random sound from the array of sounds
                if (sounds.Length > 0)
                {
                    int randomIndex = Random.Range(0, sounds.Length);
                    SoundManager.instance.PlaySound(sounds[randomIndex]);
                }

                yield return new WaitForSeconds(delaybetweenLines);
                
             
            }
        }
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        NextNode("exit");
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

}