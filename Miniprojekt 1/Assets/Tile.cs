using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor, selectedColor;
    [SerializeField] private SpriteRenderer tileSprite;
    [SerializeField] private GameObject highlight;
    private bool isSelected;
    private bool activeHighlight = false;
    public bool unitOnTile = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Init(bool isOffset)
    {
        tileSprite.color = isOffset ? offsetColor : baseColor;
    }

    public void Select()
    {
        activeHighlight = true;
        highlight.SetActive(activeHighlight);
    }
    public void Deselect()
    {
        activeHighlight = false;
        highlight.SetActive(activeHighlight);
    }
}
