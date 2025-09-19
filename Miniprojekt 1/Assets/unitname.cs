using UnityEngine;
using UnityEngine.EventSystems;
using TMPro; // needed for TMP_Text

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text textComponent;

    void Awake()
    {
        textComponent = GetComponent<TMP_Text>(); // get the text component on this object
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textComponent.text = "Hovering: " + gameObject.name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textComponent.text = "Not hovering";
    }
}