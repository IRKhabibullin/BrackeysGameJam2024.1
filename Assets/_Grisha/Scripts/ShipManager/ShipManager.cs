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

    void Update()
    {
        if(gameInProgress)
        {
            currentOxygenTimer -= Time.deltaTime;
            if(currentOxygenTimer <= 0)
            {
                gameInProgress = false;
                ShipEventsBus.OxygenHasRunOut?.Invoke();
            }
        }
    }
    void StartTimer()
    {
        gameInProgress = true;
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
        GameEventsBus.GameSessionStart += StartTimer;
    }
    void OnDisable()
    {
        ShipEventsBus.AddFuel -= AddFuel;
        ShipEventsBus.RemoveFuel -= RemoveFuel;
        GameEventsBus.GameSessionStart -= StartTimer;
    }
}