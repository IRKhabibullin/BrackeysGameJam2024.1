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

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GameEventsBus.GameStarted += RunIntro;
    }

    private void OnDisable()
    {
        GameEventsBus.GameStarted -= RunIntro;
    }
}
