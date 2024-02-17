using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class ShipManager : MonoBehaviour
{
    // Oxygen timer = time of the round in seconds 
    [SerializeField] int maxOxygenTime = 600;
    [SerializeField] float currentOxygenTime;
    [SerializeField] int requiredAmountOfFuel = 10;
    [SerializeField] float currentAmountOfFuel;
    public List<ShipMember> shipMembers;
    private int aliveCrewNumber;

    private int injectionsNumber = 0;

    private ShipMember _shipMemberAtTheDoors;
    private bool IsTankFull => currentAmountOfFuel >= requiredAmountOfFuel;
    private WaitForSeconds oxygenUpdatePeriod;

    private void CheckShipMember(ShipMember shipMember)
    {
        ShipEventsBus.ResettingPanel?.Invoke();
        _shipMemberAtTheDoors = shipMember;
        _shipMemberAtTheDoors.gameObject.SetActive(true);
        
        ShipEventsBus.ShowShipMemberProfileOnUI?.Invoke(shipMember.shipMemberProfile);
    }

    [ContextMenu("LetShipMemberIn")]
    private void LetShipMemberIn()
    {
        if (IsTankFull)
        {
            // we just stay at ship ready to fly off the planet
            shipMembers.Add(_shipMemberAtTheDoors);
            PlanetEventsBus.ShipMemberComingToShip?.Invoke();
        }
        else
        {
            if (_shipMemberAtTheDoors.IsFinalInfectionStage)
            {
                // we let the fully infected ship member in. He starts to break things and wastes fuel tank
                ShipEventsBus.RemoveFuel?.Invoke();
                ClipEventsBus.LettingInfectedShipMemberIn?.Invoke();
                Debug.Log("Let in infected");
                KillShipMember();
            }
            else
            {
                ShipEventsBus.AddFuel?.Invoke();
                ClipEventsBus.LettingShipMemberIn?.Invoke();
                Debug.Log("Let in normal");
                _shipMemberAtTheDoors.gameObject.SetActive(false);
                PlanetEventsBus.ShipMemberGoingGathering?.Invoke(_shipMemberAtTheDoors);
            }
        }
        
        _shipMemberAtTheDoors = null;
    }

    [ContextMenu("KillShipMember")]
    private void KillShipMember()
    {
        if(_shipMemberAtTheDoors.IsInfected)
        {
            ShipEventsBus.ShowInjectionsNumberOnUI?.Invoke(++injectionsNumber);
        }

        _shipMemberAtTheDoors.gameObject.SetActive(false);
        ShipEventsBus.ShowAliveCrewNumberOnUI?.Invoke(--aliveCrewNumber);
        ClipEventsBus.BurningShipMember?.Invoke();
    }
    private void HealShipMember()
    {
        if(injectionsNumber > 0)
        {
            ShipEventsBus.ShowInjectionsNumberOnUI?.Invoke(--injectionsNumber);
            _shipMemberAtTheDoors.ApplyHeal();
        }
    }
    void SendAllShipMembers()
    {
        StartCoroutine(StartTimerCoroutine());

        foreach (var shipMember in shipMembers)
        {
            shipMember.gameObject.SetActive(false);
            PlanetEventsBus.ShipMemberGoingGathering?.Invoke(shipMember);
        }
        shipMembers.Clear();

        PlanetEventsBus.ShipMemberComingToShip?.Invoke();
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

    void RemoveFuel()
    {
        if (currentAmountOfFuel <= 0)
            return;

        currentAmountOfFuel--;
        ShipEventsBus.FuelAmountUpdated?.Invoke(currentAmountOfFuel / requiredAmountOfFuel);
    }
    void AddFuel()
    {
        if (currentAmountOfFuel >= requiredAmountOfFuel)
            return;

        currentAmountOfFuel++;
        if(IsTankFull)
        {
            Debug.Log("FuelBecameFull");
            ShipEventsBus.FuelBecameFull?.Invoke(shipMembers.Count == aliveCrewNumber);
        }
        ShipEventsBus.FuelAmountUpdated?.Invoke(currentAmountOfFuel / requiredAmountOfFuel);
    }
    void OnEnable()
    {
        ShipEventsBus.AddFuel += AddFuel;
        ShipEventsBus.RemoveFuel += RemoveFuel;
        ShipEventsBus.LettingShipMemberIn += LetShipMemberIn;
        ShipEventsBus.BurningShipMember += KillShipMember;
        ShipEventsBus.HealingShipMember += HealShipMember;
        GameEventsBus.ShipMembersGoingGathering += SendAllShipMembers;
        PlanetEventsBus.ShipMemberSentBack += CheckShipMember;
    }
    void OnDisable()
    {
        ShipEventsBus.AddFuel -= AddFuel;
        ShipEventsBus.RemoveFuel -= RemoveFuel;
        ShipEventsBus.LettingShipMemberIn -= LetShipMemberIn;
        ShipEventsBus.BurningShipMember -= KillShipMember;
        ShipEventsBus.HealingShipMember -= HealShipMember;
        GameEventsBus.ShipMembersGoingGathering -= SendAllShipMembers;
        PlanetEventsBus.ShipMemberSentBack -= CheckShipMember;
    }
}