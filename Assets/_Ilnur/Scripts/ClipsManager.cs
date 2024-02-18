using UnityEngine;

public class ClipsManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _container;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip goodEndingClip;
    [SerializeField] private AudioClip egoistEndingClip;
    [SerializeField] private AudioClip alienEndingClip;
    [SerializeField] private AudioClip noOxygenEndingClip;
    [SerializeField] private AudioClip aloneEndingClip;

    #region Intro
    private void RunIntro()
    {
        _container.SetActive(true);
        _animator.SetTrigger("RunIntro");
    }

    private void FinishIntro()
    {
        DisableClip();
        GameEventsBus.ShipMembersGoingGathering?.Invoke();
    }
    #endregion

    #region RunInfectedShipMember
    private void RunLettingInfectedShipMemberIn()
    {
        _container.SetActive(true);
        _animator.SetTrigger("RunInfectedShipMember");
    }

    private void FinishLettingInfectedShipMemberIn()
    {
        PlanetEventsBus.ShipMemberComingToShip?.Invoke();
    }
    #endregion

    #region RunLettingShipMemberIn
    private void RunLettingShipMemberIn()
    {
        _container.SetActive(true);
        _animator.SetTrigger("RunLettingShipMemberIn");
    }

    private void FinishLettingShipMemberIn()
    {
        PlanetEventsBus.ShipMemberComingToShip?.Invoke();
    }
    #endregion

    #region RunBurningShipMember
    private void RunBurnedNormalMember()
    {
        _container.SetActive(true);
        _animator.SetTrigger("RunBurnedNormalMember");
    }

    private void RunBurnedInfectedMember()
    {
        _container.SetActive(true);
        _animator.SetTrigger("RunBurnedInfectedMember");
    }

    private void FinishBurningShipMember()
    {
        PlanetEventsBus.ShipMemberComingToShip?.Invoke();
    }

    private void RunGoodEnding()
    {
        audioSource.clip = goodEndingClip;
        _container.SetActive(true);
        _animator.SetTrigger("RunGoodEnding");
    }

    private void RunEgoistEnding()
    {
        audioSource.clip = egoistEndingClip;
        _container.SetActive(true);
        _animator.SetTrigger("RunEgoistEnding");
    }

    private void RunAlienEnding()
    {
        audioSource.clip = alienEndingClip;
        _container.SetActive(true);
        _animator.SetTrigger("RunAlienEnding");
    }

    private void RunNoOxygenEnding()
    {
        audioSource.clip = noOxygenEndingClip;
        _container.SetActive(true);
        _animator.SetTrigger("RunNoOxygenEnding");
    }

    private void RunAloneEnding()
    {
        audioSource.clip = aloneEndingClip;
        _container.SetActive(true);
        _animator.SetTrigger("RunAloneEnding");
    }

    private void CallFinishGame()
    {
        GameEventsBus.CallingFinishGame?.Invoke();
    }

    private void DisableClip()
    {
        _container.SetActive(false);
    }
    #endregion

    private void OnEnable()
    {
        ClipEventsBus.RunningIntro += RunIntro;
        ClipEventsBus.LettingInfectedShipMemberIn += RunLettingInfectedShipMemberIn;
        ClipEventsBus.LettingShipMemberIn += RunLettingShipMemberIn;
        ClipEventsBus.BurnedNormalMember += RunBurnedNormalMember;
        ClipEventsBus.BurnedInfectedMember += RunBurnedInfectedMember;

        ClipEventsBus.RunningGoodEnding += RunGoodEnding;
        ClipEventsBus.RunningEgoistEnding += RunEgoistEnding;
        ClipEventsBus.RunningNoOxygenEnding += RunNoOxygenEnding;
        ClipEventsBus.RunningAloneEnding += RunAloneEnding;
    }

    private void OnDisable()
    {
        ClipEventsBus.RunningIntro -= RunIntro;
        ClipEventsBus.LettingInfectedShipMemberIn -= RunLettingInfectedShipMemberIn;
        ClipEventsBus.LettingShipMemberIn -= RunLettingShipMemberIn;
        ClipEventsBus.BurnedNormalMember -= RunBurnedNormalMember;
        ClipEventsBus.BurnedInfectedMember -= RunBurnedInfectedMember;
        
        ClipEventsBus.RunningGoodEnding -= RunGoodEnding;
        ClipEventsBus.RunningEgoistEnding -= RunEgoistEnding;
        ClipEventsBus.RunningNoOxygenEnding -= RunNoOxygenEnding;
        ClipEventsBus.RunningAloneEnding -= RunAloneEnding;
    }
}
