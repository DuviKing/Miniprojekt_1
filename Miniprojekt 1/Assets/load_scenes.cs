using UnityEngine;
using UnityEngine.SceneManagement;

public class load_scenes : MonoBehaviour
{
    // Loads scene 1 (End Screen)
    public void LoadEndScreen()
    {
        SceneManager.LoadScene(1);
    }

    // Loads scene 2 (Title Screen)
    public void LoadTitleScreen()
    {
        SceneManager.LoadScene(2);
    }

    // Loads scene 0 (Battle Scene)
    public void LoadBattleScene()
    {
        SceneManager.LoadScene(0);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
