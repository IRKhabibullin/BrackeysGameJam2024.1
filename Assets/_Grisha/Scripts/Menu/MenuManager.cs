using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        MenuEventsBus.StartGame += StartGame;
    }
    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    void OnDisable()
    {
        MenuEventsBus.StartGame -= StartGame;
    }
}