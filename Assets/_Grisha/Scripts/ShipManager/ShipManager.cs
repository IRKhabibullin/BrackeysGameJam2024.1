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

    void Start()
    {
        ShipEventsBus.GameSessionStart += GameSessionStart;

        ShipEventsBus.AddFuel += AddFuel;
        ShipEventsBus.RemoveFuel += RemoveFuel;
        
        ShipEventsBus.ConfirmTheVictory += ConfirmTheVictory;
    }
    void Update()
    {
        if(gameInProgress)
        {
            currentOxygenTimer -= Time.deltaTime;
            if(currentOxygenTimer <= 0)
            {
                GameSessionEnd(false);
            }
            else if(currentAmountOfFuel >= requiredAmountOfFuel & !isTankFull)
            {
                PendingVictory();
            }
            else if(currentAmountOfFuel < requiredAmountOfFuel & isTankFull)
            {
                ShipEventsBus.FuelHasBeenStolen?.Invoke();
            }
            else if(currentAmountOfFuel >= requiredAmountOfFuel & shipMembers.Count == 7) // need to check, that every ship member is back
            {
                GameSessionEnd(true);
            }
        }
    }

    void GameSessionStart()
    {
        currentOxygenTimer = startingOxygenTimer;
        ShipEventsBus.AddFuel += AddFuel;
        ShipEventsBus.RemoveFuel += RemoveFuel;

        gameInProgress = true;
    }
    void GameSessionEnd(bool isVictory)
    {
        if(!isVictory)
        {
            gameInProgress = false;
            currentOxygenTimer = 0;
            ShipEventsBus.OxygenHasRunOut?.Invoke();
        }
        else
        {
            ShipEventsBus.FuelHasBeenCollected?.Invoke();
        }
    }
    void PendingVictory()
    {
        isTankFull = true;
        ShipEventsBus.AskVictoryConfirmation?.Invoke();
    }
    void ConfirmTheVictory()
    {
        gameInProgress = false;
        GameSessionEnd(true);
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
        ShipEventsBus.ConfirmTheVictory -= ConfirmTheVictory;
    }
}