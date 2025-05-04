using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.OnGameStarted?.Invoke();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
