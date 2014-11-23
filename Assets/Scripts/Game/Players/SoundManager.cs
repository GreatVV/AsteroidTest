using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
            }

            return _instance ??
                   (_instance = new GameObject("SoundManager", typeof (SoundManager)).GetComponent<SoundManager>());
        }
    }

    public static void PlaySound(AudioClip clip)
    {
        if (Instance.audio && clip)
        {
            Instance.audio.clip = clip;
            Instance.audio.Play();
        }
    }
}