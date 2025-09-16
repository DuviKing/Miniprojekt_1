using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor, selectedColor;
    [SerializeField] private SpriteRenderer tileSprite;
    [SerializeField] private GameObject highlight;
    public bool activeHighlight = false;
    public UnitScript OccupiedUnit { get; private set; }
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

    public void PlaceUnit(UnitScript unit)
    {
        OccupiedUnit = unit;
        unit.PlaceOnTile(this);
    }

    public void ClearUnit()
    {
        OccupiedUnit = null;
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
