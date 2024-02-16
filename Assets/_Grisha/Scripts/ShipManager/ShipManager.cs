using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class ShipManager : MonoBehaviour
{
    // Oxygen timer = time of the round in seconds 
    [SerializeField] float startingOxygenTimer = 60.0f; 
    [SerializeField] float currentOxygenTimer;
    [SerializeField] int requiredAmountOfFuel = 100;
    [SerializeField] int currentAmountOfFuel;
    bool gameInProgress = false;
    bool isTankFull = false;
    public List<ShipMember> shipMembers;

    private ShipMember _shipMemberAtTheDoors;

    private void CheckShipMember(ShipMember shipMember)
    {
        Debug.Log($"CheckShipMember {shipMember.name}");
        _shipMemberAtTheDoors = shipMember;
        
        LetInShipMember();
    }

    private void LetInShipMember()
    {
        // if fuel is full and we dont need to send anyone
        shipMembers.Add(_shipMemberAtTheDoors);
        
        PlanetEventsBus.ShipMemberComingBack?.Invoke();
    }

    void SendAllShipMembers()
    {
        gameInProgress = true;
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
        yield return new WaitForSeconds(startingOxygenTimer);
        
        ShipEventsBus.OxygenHasRunOut?.Invoke();
    }

    void RemoveFuel(int fuelToRemove)
    {
        currentAmountOfFuel -= fuelToRemove;
        if(true)
        {
            ShipEventsBus.FuelStoppedBeingFull?.Invoke();
        }
    }
    void AddFuel(int fuelToAdd)
    {
        currentAmountOfFuel += fuelToAdd;
        if(currentAmountOfFuel >= requiredAmountOfFuel)
        {
            isTankFull = true;
            ShipEventsBus.FuelBecameFull?.Invoke(shipMembers.Count == 7);
        }
    }
    void OnEnable()
    {
        ShipEventsBus.AddFuel += AddFuel;
        ShipEventsBus.RemoveFuel += RemoveFuel;
        GameEventsBus.ShipMembersGoingGathering += SendAllShipMembers;
        PlanetEventsBus.ShipMemberSentBack += CheckShipMember;
    }
    void OnDisable()
    {
        ShipEventsBus.AddFuel -= AddFuel;
        ShipEventsBus.RemoveFuel -= RemoveFuel;
        GameEventsBus.ShipMembersGoingGathering -= SendAllShipMembers;
        PlanetEventsBus.ShipMemberSentBack -= CheckShipMember;
    }
}