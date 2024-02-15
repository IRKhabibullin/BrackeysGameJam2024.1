using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        ShipEventsBus.OxygenHasRunOut += GameLost;
        ShipEventsBus.FuelHasBeenCollected += GameWon;
        StartGameLoop();
    }
    void StartGameLoop()
    {
        ShipEventsBus.GameSessionStart?.Invoke();
    }
    void GameLost()
    {

    }
    void GameWon()
    {

    }
    void OnDisable()
    {
        ShipEventsBus.OxygenHasRunOut -= GameLost;
        ShipEventsBus.FuelHasBeenCollected -= GameWon;
    }

}
