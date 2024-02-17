using UnityEngine;
using UnityEngine.UI;

public class FuelPanel : MonoBehaviour
{
    [SerializeField] private Image fuelTank;

    private void DrawFuelAmount(float fuelAmount)
    {
        fuelTank.fillAmount = fuelAmount;
    }
    
    private void OnEnable()
    {
        ShipEventsBus.FuelAmountUpdated += DrawFuelAmount;
    }

    private void OnDisable()
    {
        ShipEventsBus.FuelAmountUpdated -= DrawFuelAmount;
    }
}