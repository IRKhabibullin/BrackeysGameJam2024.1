using UnityEngine;
using UnityEngine.UI;

public class OxygenPanel : MonoBehaviour
{
    [SerializeField] private Image oxygenTank;

    private void DrawOxygenAmount(float oxygenAmount)
    {
        oxygenTank.fillAmount = oxygenAmount;
    }
    
    private void OnEnable()
    {
        ShipEventsBus.OxygenAmountUpdated += DrawOxygenAmount;
    }

    private void OnDisable()
    {
        ShipEventsBus.OxygenAmountUpdated -= DrawOxygenAmount;
    }
}