using UnityEngine;

public class ClipsManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    #region Intro
    private void RunIntro()
    {
        _animator.gameObject.SetActive(true);
        _animator.SetTrigger("RunIntro");
    }

    private void FinishIntro()
    {
        _animator.gameObject.SetActive(false);
        GameEventsBus.ShipMembersGoingGathering?.Invoke();
    }
    #endregion

    #region RunInfectedShipMember
    private void RunLettingInfectedShipMemberIn()
    {
        _animator.gameObject.SetActive(true);
        _animator.SetTrigger("RunInfectedShipMember");
    }

    private void FinishLettingInfectedShipMemberIn()
    {
        _animator.gameObject.SetActive(false);
        PlanetEventsBus.ShipMemberComingToShip?.Invoke();
    }
    #endregion

    #region RunLettingShipMemberIn
    private void RunLettingShipMemberIn()
    {
        _animator.gameObject.SetActive(true);
        _animator.SetTrigger("RunLettingShipMemberIn");
    }

    private void FinishLettingShipMemberIn()
    {
        _animator.gameObject.SetActive(false);
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
