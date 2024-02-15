using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        StartGameLoop();
    }
    void StartGameLoop()
    {
        GameEventsBus.GameSessionStart?.Invoke();
    }
    void GameLost()
    {

    }
    void GameWon(bool isAllMembersBack)
    {

    }
    void OnEnable()
    {
        ShipEventsBus.OxygenHasRunOut += GameLost;
        ShipEventsBus.FuelBecameFull += GameWon;
    }
    void OnDisable()
    {
        ShipEventsBus.OxygenHasRunOut -= GameLost;
        ShipEventsBus.FuelBecameFull -= GameWon;
    }

}
