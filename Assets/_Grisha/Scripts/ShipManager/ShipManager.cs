using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class ShipManager : MonoBehaviour
{
    // Oxygen timer = time of the round in seconds 
    [SerializeField] float oxygenTimer = 600.0f;
    [SerializeField] int requiredAmountOfFuel = 10;
    [SerializeField] int currentAmountOfFuel;
    public List<ShipMember> shipMembers;
    private int aliveCrewNumber;

    private ShipMember _shipMemberAtTheDoors;
    private bool IsTankFull => currentAmountOfFuel >= requiredAmountOfFuel;

    private void CheckShipMember(ShipMember shipMember)
    {
        _shipMemberAtTheDoors = shipMember;
        
        ShipEventsBus.ShowShipMemberProfileOnUI(shipMember.shipMemberProfile);

    }

    private void LetShipMemberIn()
    {
        if (IsTankFull)
        {
            // we just stay at ship ready to fly off the planet
            shipMembers.Add(_shipMemberAtTheDoors);
        }
        else
        {
            
            PlanetEventsBus.ShipMemberComingBack?.Invoke();
        }
    }

    private void BurnShipMember()
    {
        aliveCrewNumber -= 1;
        ShipEventsBus.ShowAliveCrewNumberOnUI?.Invoke(aliveCrewNumber);
    }

    void SendAllShipMembers()
    {
        StartCoroutine(StartTimerCoroutine());

        foreach (var shipMember in shipMembers)
        {
            PlanetEventsBus.ShipMemberGoingGathering(shipMember);
        }
        shipMembers.Clear();

        PlanetEventsBus.ShipMemberComingBack?.Invoke();
    }

    private IEnumerator StartTimerCoroutine()
    {
        yield return new WaitForSeconds(oxygenTimer);
        
        ShipEventsBus.OxygenHasRunOut?.Invoke();
    }

    void RemoveFuel(int fuelToRemove)
    {
        currentAmountOfFuel -= fuelToRemove;
    }
    void AddFuel(int fuelToAdd)
    {
        currentAmountOfFuel += fuelToAdd;
        if(IsTankFull)
        {
            ShipEventsBus.FuelBecameFull?.Invoke(shipMembers.Count == aliveCrewNumber);
        }
    }
    void OnEnable()
    {
        ShipEventsBus.AddFuel += AddFuel;
        ShipEventsBus.RemoveFuel += RemoveFuel;
        ShipEventsBus.LettingShipMemberIn += LetShipMemberIn;
        ShipEventsBus.BurningShipMember += BurnShipMember;
        GameEventsBus.ShipMembersGoingGathering += SendAllShipMembers;
        PlanetEventsBus.ShipMemberSentBack += CheckShipMember;
        //
    }
    void OnDisable()
    {
        ShipEventsBus.AddFuel -= AddFuel;
        ShipEventsBus.RemoveFuel -= RemoveFuel;
        ShipEventsBus.LettingShipMemberIn -= LetShipMemberIn;
        ShipEventsBus.BurningShipMember -= BurnShipMember;
        GameEventsBus.ShipMembersGoingGathering -= SendAllShipMembers;
        PlanetEventsBus.ShipMemberSentBack -= CheckShipMember;
    }
}