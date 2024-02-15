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
}
