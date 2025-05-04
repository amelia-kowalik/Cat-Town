using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public void StartAgain()
    {
        GameManager.OnStartAgainClicked?.Invoke();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
