using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        GameEventsBus.GameStarted?.Invoke();
    }
}
