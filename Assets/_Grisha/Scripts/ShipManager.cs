using UnityEngine;
using System;

public class ShipManager : MonoBehaviour
{
    // Oxygen timer = time of the round in seconds 
    [SerializeField] float startingOxygenTimer = 60.0f; 
    [SerializeField] float currentOxygenTimer;
    [SerializeField] int requiredAmountOfFuel = 100;
    [SerializeField] int currentAmountOfFuel;
    bool gameFinished;

    void Start()
    {
        GameSessionStart();
    }
    void Update()
    {
        if(!gameFinished)
        {
            if (currentOxygenTimer > 0 & currentAmountOfFuel < requiredAmountOfFuel)
                currentOxygenTimer -= Time.deltaTime;
            else if(currentAmountOfFuel >= requiredAmountOfFuel)
                GameSessionEnd(true);
            else
                GameSessionEnd(false);
        }
    }

    void GameSessionStart()
    {
        currentOxygenTimer = startingOxygenTimer;
        ShipEventsBus.AddFuel += AddFuel;
        ShipEventsBus.RemoveFuel += RemoveFuel;
    }
    void GameSessionEnd(bool isVictory)
    {
        gameFinished = true;
        currentOxygenTimer = 0;
        if(isVictory)
            ShipEventsBus.GameWon?.Invoke();
        else
            ShipEventsBus.GameLosed?.Invoke();
        
    }

    void RemoveFuel(int fuelToRemove)
    {
        currentAmountOfFuel -= fuelToRemove;
    }
    void AddFuel(int fuelToAdd)
    {
        currentAmountOfFuel += fuelToAdd;
    }
    void OnDisable()
    {
        ShipEventsBus.AddFuel -= AddFuel;
        ShipEventsBus.RemoveFuel -= RemoveFuel;
    }
}