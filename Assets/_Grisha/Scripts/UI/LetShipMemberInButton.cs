using UnityEngine;

public class LetShipMemberInButton : MonoBehaviour
{
    public void LetShipMemberIn()
    {
        ShipEventsBus.LettingShipMemberIn?.Invoke();
    }
}