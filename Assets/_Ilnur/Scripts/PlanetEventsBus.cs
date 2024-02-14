using System;

public class PlanetEventsBus
{
    public static Action<ShipMember> ShipMemberGoingGathering;
    public static Action ShipMemberComingBack;
    public static Action<ShipMember> ShipMemberSentBack;
}