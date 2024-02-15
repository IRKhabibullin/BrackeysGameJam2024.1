using System;

public static class ShipEventsBus 
{
    // Manipulation with fuel tank
    // For using: ShipsEventsBus.AddFuel?.Invoke(INT_AMOUNT_OF_FUEL)
    public static Action<int> RemoveFuel;
    public static Action<int> AddFuel;

    
    public static Action GameSessionStart;
    // Events from ship manager at the end of the game
    public static Action OxygenHasRunOut;
    public static Action FuelHasBeenCollected;
    // Event, when fuel was stolen from full tank
    public static Action FuelHasBeenStolen;

    // That event must activate button for finishing the game 
    public static Action AskVictoryConfirmation; 

    // That event confirms, that players wanna finish
    public static Action ConfirmTheVictory;


}