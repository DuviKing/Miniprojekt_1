using UnityEngine;
using UnityEngine.Audio;
using Unity.Mathematics;

public class LydMixserManeger : MonoBehaviour
{
    [SerializeField] private AudioMixer lydMixser;
    private void Start()
    {
       // lydMenu.SetActive(false); // Hide it at the start
    }

    public void setmasterVolum(float volum)
    {
        lydMixser.SetFloat("MasterVolume", Mathf.Log10(volum) * 20);
    }
    public void setmusikkVolum(float volum)
    {
        lydMixser.SetFloat("music volume", Mathf.Log10(volum) * 20);
    }
    public void seteffektVolum(float volum)
    {
        lydMixser.SetFloat("LydeffektVolume", Mathf.Log10(volum) * 20);
    }
}
