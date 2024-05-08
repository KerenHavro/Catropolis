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
            dialogue.text = dataParts[2];

            // Set speaker image if available
            Sprite speakerSprite = b.GetSprite();
            if (speakerSprite != null)
            {
                speakerImage.sprite = speakerSprite;
            }

            // Wait for the user to click before proceeding
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            // Proceed to the next node
            NextNode("exit");
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
}
