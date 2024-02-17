using System;

public static class GameEventsBus 
{
    public static Action GameStarted;
    public static Action GameWon;
    public static Action GameLost;
    
    // Gameplay events
    public static Action ShipMembersGoingGathering;
    public static Action FlyingOff;
}