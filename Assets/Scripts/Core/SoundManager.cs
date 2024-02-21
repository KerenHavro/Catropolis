using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; protected set; }

    private AudioSource sorce;

    private void Awake()
    {
        instance = this;
        sorce = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        sorce.PlayOneShot(sound);
    }
}