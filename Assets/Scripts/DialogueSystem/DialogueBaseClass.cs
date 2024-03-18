using System.Collections;
using UnityEngine;
using TMPro;

namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool finished { get; protected set; }

        protected IEnumerator WriteText(string input, TMP_Text textHolder, float delay, Color textColor, AudioClip[] sounds, float delayBetweenLines, AudioClip Meow)
        {
            textHolder.color = textColor;

            // Play the initial "Meow" sound
            SoundManager.instance.PlaySound(Meow);

            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];

                // Play a random sound from the array of sounds
                if (sounds.Length > 0)
                {
                    int randomIndex = Random.Range(0, sounds.Length);
                    SoundManager.instance.PlaySound(sounds[randomIndex]);
                }

                yield return new WaitForSeconds(delay);
            }

            yield return new WaitUntil(() => Input.GetMouseButton(0));
            finished = true;
            //this.gameObject.SetActive(false);
        }
    }
}
