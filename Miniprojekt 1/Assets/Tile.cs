using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor, selectedColor;
    [SerializeField] private SpriteRenderer tileSprite;
    [SerializeField] private GameObject highlight;
    public bool activeHighlight = false;
    public UnitScript OccupiedUnit { get; private set; }
    public DamageIndicatorManager damageIndicatorManager; // reference to the damage indicator manager
    
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

    public void InitiateCombat(UnitScript attackingUnit)
    {
        OccupiedUnit.health = OccupiedUnit.health - attackingUnit.damage;
        Debug.Log($"{OccupiedUnit.transform.position}, {attackingUnit.damage}");
        //damageIndicatorManager.SpawnDamageText(OccupiedUnit.transform.position, attackingUnit.damage.ToString());
        attackingUnit.actionPoints = 0; // sætter action points til 0 efter et angreb
        Debug.Log($"{OccupiedUnit.name} has {OccupiedUnit.health} health left");
        if (OccupiedUnit.health <= 0)
        {
            Destroy(OccupiedUnit.gameObject);
            Debug.Log($"{OccupiedUnit}");
        }
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
