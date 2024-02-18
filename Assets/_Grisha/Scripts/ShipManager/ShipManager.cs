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

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip womanBurnSound;
    [SerializeField] private AudioClip manBurnSound;

    public List<ShipMember> shipMembers;
    private int aliveCrewNumber = 7;

    private int injectionsNumber = 0;

    private ShipMember _shipMemberAtTheDoors;
    public bool IsTankFull => currentAmountOfFuel >= requiredAmountOfFuel;
    public bool IsOxygenEmpty => currentOxygenTime > 0;
    public bool AllCrewOnShip => shipMembers.Count == aliveCrewNumber;
    private WaitForSeconds oxygenUpdatePeriod;

    private void CheckShipMember(ShipMember shipMember)
    {
        _shipMemberAtTheDoors = shipMember;
        _shipMemberAtTheDoors.gameObject.SetActive(true);
        
        ShipEventsBus.ShowShipMemberProfileOnUI?.Invoke(shipMember.shipMemberProfile);
    }

    [ContextMenu("LetShipMemberIn")]
    private void LetShipMemberIn()
    {
        if (!_shipMemberAtTheDoors)
            return;
        ShipEventsBus.ResettingPanel?.Invoke();
        
        if (!IsTankFull)
        {
            if (_shipMemberAtTheDoors.IsFinalInfectionStage)
            {
                // we let the fully infected ship member in. He starts to break things and wastes fuel tank
                ShipEventsBus.RemoveFuel?.Invoke();
                ClipEventsBus.LettingInfectedShipMemberIn?.Invoke();
                KillShipMember();
            }
            else
            {
                ShipEventsBus.AddFuel?.Invoke();
                ClipEventsBus.LettingShipMemberIn?.Invoke();
                if (!IsTankFull)
                {
                    _shipMemberAtTheDoors.gameObject.SetActive(false);
                    PlanetEventsBus.ShipMemberGoingGathering?.Invoke(_shipMemberAtTheDoors);
                }
            }
        }
        
        if (IsTankFull)
        {
            // we just stay at ship ready to fly off the planet
            shipMembers.Add(_shipMemberAtTheDoors);
            _shipMemberAtTheDoors.gameObject.SetActive(false);
            _shipMemberAtTheDoors = null;
            ClipEventsBus.LettingInfectedShipMemberIn?.Invoke();
            Debug.Log($"New one {_shipMemberAtTheDoors}");
        }
    }

    [ContextMenu("KillShipMember")]
    private void KillShipMember()
    {
        if (!_shipMemberAtTheDoors)
            return;
        ShipEventsBus.ResettingPanel?.Invoke();

        if(_shipMemberAtTheDoors.IsInfected)
        {
            ShipEventsBus.ShowInjectionsNumberOnUI?.Invoke(++injectionsNumber);
        }

        _shipMemberAtTheDoors.gameObject.SetActive(false);
        ShipEventsBus.ShowAliveCrewNumberOnUI?.Invoke(--aliveCrewNumber);

        if (aliveCrewNumber <= 0 && !IsTankFull)
        { 
            ClipEventsBus.RunningAloneEnding?.Invoke();
        }
        else
        {
            if (_shipMemberAtTheDoors.shipMemberProfile.sex == ShipMemberSex.Male)
                audioSource.clip = manBurnSound;
            else
                audioSource.clip = womanBurnSound;
            audioSource.Play();
            if (_shipMemberAtTheDoors.IsInfected)
                ClipEventsBus.BurnedInfectedMember?.Invoke();
            else
                ClipEventsBus.BurnedNormalMember?.Invoke();
        }
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

        aliveCrewNumber = shipMembers.Count;
        ShipEventsBus.ShowAliveCrewNumberOnUI?.Invoke(aliveCrewNumber);
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
            ShipEventsBus.OxygenAmountUpdated?.Invoke(--currentOxygenTime / maxOxygenTime);
        }

        if (_shipMemberAtTheDoors)
        {
            _shipMemberAtTheDoors.gameObject.SetActive(false);
        }
        ClipEventsBus.RunningNoOxygenEnding?.Invoke();
    }

    private void StopGame()
    {
        StopAllCoroutines();
        if (_shipMemberAtTheDoors)
        {
            _shipMemberAtTheDoors.gameObject.SetActive(false);
        }
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
        GameEventsBus.FlyingOff += StopGame;
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
        GameEventsBus.FlyingOff -= StopGame;
    }
}