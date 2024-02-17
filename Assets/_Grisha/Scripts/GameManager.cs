using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        ClipEventsBus.RunningIntro?.Invoke();
    }
}
