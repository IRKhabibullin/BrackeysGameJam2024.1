using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class ShipManager : MonoBehaviour
{
    // Oxygen timer = time of the round in seconds 
    [SerializeField] int maxOxygenTime = 600;
    [SerializeField] float currentOxygenTime = 0;
    [SerializeField] int requiredAmountOfFuel = 10;
    [SerializeField] float currentAmountOfFuel;
    public List<ShipMember> shipMembers;
    private int aliveCrewNumber;

    private ShipMember _shipMemberAtTheDoors;
    private bool IsTankFull => currentAmountOfFuel >= requiredAmountOfFuel;
    private WaitForSeconds oxygenUpdatePeriod;

    private void CheckShipMember(ShipMember shipMember)
    {
        _shipMemberAtTheDoors = shipMember;
        
        ShipEventsBus.ShowShipMemberProfileOnUI?.Invoke(shipMember.shipMemberProfile);

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
        currentOxygenTime = maxOxygenTime;
        oxygenUpdatePeriod = new WaitForSeconds(1);
        ShipEventsBus.OxygenAmountUpdated?.Invoke(currentOxygenTime / maxOxygenTime);
        while (currentOxygenTime > 0)
        {
            yield return oxygenUpdatePeriod;
            ShipEventsBus.OxygenAmountUpdated?.Invoke(currentOxygenTime / maxOxygenTime);
        }

        ShipEventsBus.OxygenHasRunOut?.Invoke();
    }

    void RemoveFuel(int fuelToRemove)
    {
        if (currentAmountOfFuel <= 0)
            return;

        currentAmountOfFuel -= fuelToRemove;
        ShipEventsBus.FuelAmountUpdated?.Invoke(currentAmountOfFuel / requiredAmountOfFuel);
    }
    void AddFuel(int fuelToAdd)
    {
        if (currentAmountOfFuel >= requiredAmountOfFuel)
            return;

        currentAmountOfFuel += fuelToAdd;
        if(IsTankFull)
        {
            ShipEventsBus.FuelBecameFull?.Invoke(shipMembers.Count == aliveCrewNumber);
        }
        ShipEventsBus.FuelAmountUpdated?.Invoke(currentAmountOfFuel / requiredAmountOfFuel);
    }
    void OnEnable()
    {
        ShipEventsBus.AddFuel += AddFuel;
        ShipEventsBus.RemoveFuel += RemoveFuel;
        ShipEventsBus.LettingShipMemberIn += LetShipMemberIn;
        ShipEventsBus.BurningShipMember += BurnShipMember;
        GameEventsBus.ShipMembersGoingGathering += SendAllShipMembers;
        PlanetEventsBus.ShipMemberSentBack += CheckShipMember;
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