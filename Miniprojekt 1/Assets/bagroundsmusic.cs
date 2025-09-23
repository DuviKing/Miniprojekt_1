using UnityEngine;

public class bagroundsmusic : MonoBehaviour
{
    public AudioClip musicTrack1;
    public AudioClip musicTrack2;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.loop = true;
    }

    void Start()
    {
        PlayRandomTrack();
    }

    public void PlayRandomTrack()
    {
        if (musicTrack1 == null && musicTrack2 == null) return;
        AudioClip chosen = (Random.value > 0.5f) ? musicTrack1 : musicTrack2;
        if (chosen != null)
        {
            audioSource.clip = chosen;
            audioSource.Play();
        }
    }

    public void StartMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
