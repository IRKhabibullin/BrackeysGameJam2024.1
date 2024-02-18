using System;
using UnityEngine;

public static class ShipEventsBus 
{
    // Manipulation with fuel tank
    // For using: ShipsEventsBus.AddFuel?.Invoke(INT_AMOUNT_OF_FUEL)
    public static Action RemoveFuel;
    public static Action AddFuel;

    public static Action<float> OxygenAmountUpdated;  // in %
    public static Action OxygenHasRunOut;
    
    public static Action<float> FuelAmountUpdated;  // in %
    public static Action<bool> FuelBecameFull;  // bool = isAllShipMembers on the ship

    // Events from UI
    public static Action LettingShipMemberIn;
    public static Action BurningShipMember;
    public static Action ResettingPanel;
    public static Action HealingShipMember;
    public static Action<Vector3> MoveXRay;

    // Data refreshers for UI
    public static Action<SO_ShipMemberProfile> ShowShipMemberProfileOnUI;
    public static Action<int> ShowAliveCrewNumberOnUI;
    public static Action<int> ShowInjectionsNumberOnUI;
    public static Action<int> RefreshAliveCrewOnUI;
}