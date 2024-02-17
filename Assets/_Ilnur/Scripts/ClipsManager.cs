using UnityEngine;

public class ClipsManager : MonoBehaviour
{
    private Animator _animator;

    #region Intro
    private void RunIntro()
    {
        _animator.SetTrigger("RunIntro");
    }

    private void FinishIntro()
    {
        GameEventsBus.ShipMembersGoingGathering?.Invoke();
    }
    #endregion

    #region RunInfectedShipMember
    private void RunLettingInfectedShipMemberIn()
    {
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
        _animator.SetTrigger("RunLettingShipMemberIn");
    }

    private void FinishLettingShipMemberIn()
    {
        PlanetEventsBus.ShipMemberComingToShip?.Invoke();
    }
    #endregion

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

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
