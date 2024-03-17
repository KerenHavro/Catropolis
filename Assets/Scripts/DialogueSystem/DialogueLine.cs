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

        private IEnumerator lineAppear;

        private void Awake()
        {
            //textHolder.text = " ";

            imageHolder.sprite = charSprite;
            imageHolder.preserveAspect = true;
        }

        public void PlayLine()
        {
           
            ResetLine();
            lineAppear = WriteText(input, textHolder, delay, textColor, sound, delayBetweenLines);
            StartCoroutine(lineAppear);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (textHolder.text != input)
                {
                    StopCoroutine(lineAppear);
                    textHolder.text = input;
                }
                else
                    finished = true;
            }
        }

        private void ResetLine()
        {
            textHolder = GetComponent<TMP_Text>();
            textHolder.text = "";
            finished = false;
        }
    }
}
