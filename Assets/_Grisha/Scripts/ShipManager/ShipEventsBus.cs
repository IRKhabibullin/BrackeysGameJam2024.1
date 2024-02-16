using System;

public static class ShipEventsBus 
{
    // Manipulation with fuel tank
    // For using: ShipsEventsBus.AddFuel?.Invoke(INT_AMOUNT_OF_FUEL)
    public static Action<int> RemoveFuel;
    public static Action<int> AddFuel;

    public static Action OxygenHasRunOut;

    /// bool = isAllShipMembers on the ship
    public static Action<bool> FuelBecameFull;
    public static Action FuelStoppedBeingFull;

    public static Action LettingShipMemberIn;
    public static Action BurningShipMember;
}