using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ClickButton);
    }
    void ClickButton()
    {
        MenuEventsBus.StartGame?.Invoke();
    }
}