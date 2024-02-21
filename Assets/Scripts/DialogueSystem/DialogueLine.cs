using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        [Header("Text options")]
        [SerializeField]
        private string input;
        private TMP_Text textHolder;
        [SerializeField]
        private Color textColor;
        [SerializeField]
        private float delay;
        [SerializeField]
        private float delayBetweenLines;

        [Header("Text sound")]
        [SerializeField]
        private AudioClip sound;

        [Header("Char Image")]
        [SerializeField]
        private Sprite charSprite;
        [SerializeField]
        private Image imageHolder;

        private void Awake()
        {
            //textHolder.text = "";
            textHolder = GetComponent<TMP_Text>();
            imageHolder.sprite = charSprite;
            imageHolder.preserveAspect = true;
        }

        private void Start()
        {
            StartCoroutine(WriteText(input, textHolder, delay, textColor, sound, delayBetweenLines));
        }
    }
}
