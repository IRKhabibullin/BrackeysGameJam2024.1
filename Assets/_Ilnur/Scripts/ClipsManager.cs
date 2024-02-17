using UnityEngine;

public class ClipsManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _container;

    #region Intro
    private void RunIntro()
    {
        _container.SetActive(true);
        _animator.SetTrigger("RunIntro");
    }

    private void FinishIntro()
    {
        _container.SetActive(false);
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
        _container.SetActive(false);
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
        _container.SetActive(false);
        PlanetEventsBus.ShipMemberComingToShip?.Invoke();
    }
    #endregion

    private void OnEnable()
    {
        ClipEventsBus.RunningIntro += RunIntro;
        ClipEventsBus.LettingInfectedShipMemberIn += RunLettingInfectedShipMemberIn;
        ClipEventsBus.LettingShipMemberIn += RunLettingShipMemberIn;
    }

    private void OnDisable()
    {
        ClipEventsBus.RunningIntro -= RunIntro;
        ClipEventsBus.LettingInfectedShipMemberIn -= RunLettingInfectedShipMemberIn;
        ClipEventsBus.LettingShipMemberIn -= RunLettingShipMemberIn;
    }
}
