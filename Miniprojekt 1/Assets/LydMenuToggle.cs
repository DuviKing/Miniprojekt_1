using UnityEngine;

public class LydMenuToggle : MonoBehaviour
{
    [SerializeField] private GameObject lydMenu; // Drag your LydMenu panel here in Inspector

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            lydMenu.SetActive(!lydMenu.activeSelf);
        }
    }
}
