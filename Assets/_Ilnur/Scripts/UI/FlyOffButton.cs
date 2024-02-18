using System.Collections;
using UnityEngine;

public class FlyOffButton : MonoBehaviour
{
    [SerializeField] private ShipManager ship;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip flyOffClip;
    
    
    public void FlyOff()
    {
        if (ship.IsTankFull)
            StartCoroutine(FlyOffEnumerator());
    }

    private IEnumerator FlyOffEnumerator()
    {
        audioSource.clip = flyOffClip;
        audioSource.Play();
        yield return new WaitForSeconds(6);
        audioSource.Stop();
        GameEventsBus.FlyingOff?.Invoke();
    }
}