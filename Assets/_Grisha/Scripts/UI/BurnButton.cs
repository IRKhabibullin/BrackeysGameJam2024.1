using UnityEngine;

public class BurnButton : MonoBehaviour
{
    public void Burn()
    {
        ShipEventsBus.BurningShipMember?.Invoke();
    }
}