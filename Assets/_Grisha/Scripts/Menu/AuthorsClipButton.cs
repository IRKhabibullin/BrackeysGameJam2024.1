using UnityEngine;

public class AuthorsClipButton : MonoBehaviour
{
    [SerializeField] private GameObject authorsClip;

    public void StartClip()
    {
        authorsClip.SetActive(true);
    }

    public void StopClip()
    {
        authorsClip.SetActive(false);
    }
}