using UnityEngine;

public class LydEffektManger
{
    public static LydEffektManger instance;

    [SerializeField] private AudioSource lydKildePrefab;

    private void awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void spilLyd(AudioClip audioClip, transform spawntransfrom, float volum )
    {
        AudioSource audioSource = instance(lydKildePrefab, spawntransfrom.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volum;

        audioSource.Play();

        float cliplength = audioSource.clip.length;

        Object.Destroy(audioSource.gameObject, cliplength);
    }
}
