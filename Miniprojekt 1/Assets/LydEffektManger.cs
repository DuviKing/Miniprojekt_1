using UnityEngine;
// LydEffektManager.instance.spilLyd(lydklip, spawnposition, volum)
// husk at lyd skal være angivt som "[serializeField] private AudioClip lydklip_variable_navn;", så kan man drag and droppe lydfilen i inspector.
public class LydEffektManger : MonoBehaviour
{
    public static LydEffektManger instance;

    [SerializeField] private AudioSource lydKildePrefab;
    [Header("Shared Sounds")]
    public AudioClip placementSoundShared;
    public AudioClip[] deathSoundsShared;

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
    // Assign shared placement and death sounds for all units
    UnitScript.sharedPlacementSound = placementSoundShared;
    UnitScript.sharedDeathSounds = deathSoundsShared;
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
        float randomlægte = audioClip.Length;
        AudioSource audioSource = Instantiate(lydKildePrefab, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip[randomIndex];
        audioSource.volume = volum;
        audioSource.Play();
        Debug.Log("Playing sound: " + audioSource.clip.name + "and" + randomlægte);
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
