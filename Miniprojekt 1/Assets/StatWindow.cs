using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class StatWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI unitNameText;
    [SerializeField] private TextMeshProUGUI unitHealthText;
    [SerializeField] private TextMeshProUGUI unitDamageText;
    [SerializeField] private TextMeshProUGUI unitActionPointsText;
    [SerializeField] private TextMeshProUGUI unitRangeText;
    [SerializeField] private Slider healthBarSlider;

    private void Start()
    {

    }

    void Update()
    {

    }

    public void StatWindowText(UnitScript unit)
    {
        // Update Stat Window UI elements with unit info
        unitNameText.text = unit.name;
        unitHealthText.text = $"{unit.health}/{unit.maxHealth}";
        unitDamageText.text = unit.damage.ToString();
        unitActionPointsText.text = $"{unit.actionPoints}/{unit.actionPointsMax}";
        unitRangeText.text = unit.attackRange.ToString();
        healthBarSlider.value = (float)unit.health / (float)unit.maxHealth;
    }
}