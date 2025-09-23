using UnityEngine;
// LydEffektManager.instance.spilLyd(lydklip, spawnposition, volum)
public class LydEffektManger : MonoBehaviour
{
    public static LydEffektManger instance;

    [SerializeField] private AudioSource lydKildePrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void spilLyd(AudioClip audioClip, Transform spawnTransform, float volum)
    {
        AudioSource audioSource = Instantiate(lydKildePrefab, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volum;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
    public void spilrandomLyd(AudioClip[] audioClip, Transform spawnTransform, float volum)
    {
        int randomIndex = Random.Range(0, audioClip.Length); 
        AudioSource audioSource = Instantiate(lydKildePrefab, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip[randomIndex];
        audioSource.volume = volum;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }    
}
