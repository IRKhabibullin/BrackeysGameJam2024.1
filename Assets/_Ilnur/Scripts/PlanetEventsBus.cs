using System;

public class PlanetEventsBus
{
    public static Action<ShipMember> ShipMemberGoingGathering;  // Called when ship member goes out of the ship
    public static Action ShipMemberComingToShip;  // Called when ship is ready to accept another ship member
    public static Action<ShipMember> ShipMemberSentBack;  // Called when ship member came back to the ship
}