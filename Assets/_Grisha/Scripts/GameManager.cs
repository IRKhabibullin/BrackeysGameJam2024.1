using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ShipManager ship;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        ClipEventsBus.RunningIntro?.Invoke();
    }

    private void StartMusic()
    {
        audioSource.Play();
    }

    private void StopMusic()
    {
        audioSource.Stop();
    }

    private void FlyOff()
    {
        if (ship.AllCrewOnShip)
        {
            ClipEventsBus.RunningGoodEnding?.Invoke();
        }
        else
        {
            ClipEventsBus.RunningEgoistEnding?.Invoke();
        }
    }

    private void QuitToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void OnEnable()
    {
        GameEventsBus.FlyingOff += FlyOff;
        GameEventsBus.CallingFinishGame += QuitToMenu;
        GameEventsBus.ShipMembersGoingGathering += StartMusic;
        
        ClipEventsBus.RunningGoodEnding += StopMusic;
        ClipEventsBus.RunningEgoistEnding += StopMusic;
        ClipEventsBus.RunningNoOxygenEnding += StopMusic;
        ClipEventsBus.RunningAloneEnding += StopMusic;
    }

    private void OnDisable()
    {
        GameEventsBus.FlyingOff -= FlyOff;
        GameEventsBus.CallingFinishGame -= QuitToMenu;
        GameEventsBus.ShipMembersGoingGathering -= StartMusic;
        
        ClipEventsBus.RunningGoodEnding -= StopMusic;
        ClipEventsBus.RunningEgoistEnding -= StopMusic;
        ClipEventsBus.RunningNoOxygenEnding -= StopMusic;
        ClipEventsBus.RunningAloneEnding -= StopMusic;
    }
}
