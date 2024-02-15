using System;

public static class ShipEventsBus 
{
    // Manipulation with fuel tank
    // For using: ShipsEventsBus.AddFuel?.Invoke(INT_AMOUNT_OF_FUEL)
    public static Action<int> RemoveFuel;
    public static Action<int> AddFuel;

    // Events from ship manager at the end of the game
    public static Action GameLost;
    public static Action GameWon;

}