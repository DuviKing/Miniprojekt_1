using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor, selectedColor;
    [SerializeField] private SpriteRenderer tileSprite;
    [SerializeField] private GameObject highlight;
    [SerializeField] private Color highlightColor, attackHighlightColor;
    [SerializeField] private SpriteRenderer highlightSpriteRenderer;
    public bool activeMoveHighlight = false;
    public bool activeAttackHighlight = false;
    public UnitScript OccupiedUnit { get; private set; }
    public DamageIndicatorManager damageIndicatorManager; // reference to the damage indicator manager
    public bool tileIsMountain = false;

    private Slider hpBar;

    void Start()
    {
        // Look for the GameObject named "hp_bar"
        GameObject hpBarObject = GameObject.Find("hp_bar");
        if (hpBarObject != null)
        {
            hpBar = hpBarObject.GetComponent<Slider>();
            hpBar.value = 0.5f;
        }
        else
        {
            Debug.LogError("Could not find 'hp_bar'!");
        }
    }

    [System.Obsolete]
    void Awake()
    {
        if (damageIndicatorManager == null)
        {
            damageIndicatorManager = FindObjectOfType<DamageIndicatorManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hpBar != null && OccupiedUnit != null) // assuming OccupiedUnit is accessible
        {
            // Calculate ratio: currentHealth / maxHealth
            float ratio = OccupiedUnit.health / 100f;
            hpBar.value = Mathf.Clamp01(ratio); // keeps it between 0 and 1
        }
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

        damageIndicatorManager.SpawnDamageText(OccupiedUnit.transform.position, attackingUnit.damage.ToString());

        
        // Set action points to 0 after attack
        attackingUnit.actionPoints = 0;

        Debug.Log($"{OccupiedUnit.name} has {OccupiedUnit.health} health left");


        if (OccupiedUnit.health <= 0)
        {
            OccupiedUnit.PlayDeathSound();
            Destroy(OccupiedUnit.gameObject);
            Debug.Log($"{OccupiedUnit}");
            
        }
        else
        {
            attackingUnit.PlayAttackSound();
        }
    }

    public void Select()
    {
        highlightSpriteRenderer.color = highlightColor;
        activeMoveHighlight = true;
        highlight.SetActive(activeMoveHighlight);
    }
    public void Deselect()
    {
        activeMoveHighlight = false;
        highlight.SetActive(activeMoveHighlight);
    }
    public void AttackSelect()
    {
        highlightSpriteRenderer.color = attackHighlightColor;
        activeAttackHighlight = true;
        highlight.SetActive(activeAttackHighlight);
    }
    public void SetAsMountain()
    {
        tileIsMountain = true;
    }
    
}


