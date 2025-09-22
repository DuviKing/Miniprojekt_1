using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class StatWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI unitNameText;
    [SerializeField] private TextMeshProUGUI unitHealthText;
    [SerializeField] private TextMeshProUGUI unitDamageText;
    [SerializeField] private TextMeshProUGUI unitActionPointsText;
    [SerializeField] private TextMeshProUGUI unitRangeText;

    private void Start()
    {

    }

    void Update()
    {

    }

    public void StatWindowText(UnitScript unit)
    {
        unitNameText.text = unit.name;
        unitHealthText.text = unit.health.ToString();
        unitDamageText.text = unit.damage.ToString();
        unitActionPointsText.text = $"{unit.actionPoints}/{unit.actionPointsMax}";
        unitRangeText.text = unit.attackRange.ToString();
    }
}