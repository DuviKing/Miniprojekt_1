using UnityEngine;
using UnityEngine.Audio;

public class LydMixserManeger : MonoBehaviour
{
    [SerializeField] private AudioMixer lydMixser;
    private void Start()
    {
       // lydMenu.SetActive(false); // Hide it at the start
    }

    public void setmasterVolum(float volum)
    {
        lydMixser.SetFloat("MasterVolume", volum);
    }
    public void setmusikkVolum(float volum)
    {
        lydMixser.SetFloat("music volume", volum);
    }
    public void seteffektVolum(float volum)
    {
        lydMixser.SetFloat("LydeffektVolume", volum);
    }
}
