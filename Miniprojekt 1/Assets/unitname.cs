using UnityEngine;
using TMPro;

public class HoverText : MonoBehaviour
{
    private TextMeshProUGUI hoverText;
    private string originalText;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        hoverText = GameObject.Find("unit name")?.GetComponent<TextMeshProUGUI>();

        if (hoverText != null)
            originalText = hoverText.text;
    }

    void Update()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        if (hit != null && hit.gameObject == gameObject)
        {
            hoverText.text = gameObject.name;
        }
        else if (hoverText != null && hoverText.text == gameObject.name)
        {
            hoverText.text = originalText;
        }
    }
}