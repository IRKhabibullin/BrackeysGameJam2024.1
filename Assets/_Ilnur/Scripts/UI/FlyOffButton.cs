using UnityEngine;

public class FlyOffButton : MonoBehaviour
{
    public void FlyOff()
    {
        GameEventsBus.FlyingOff?.Invoke();
    }
}