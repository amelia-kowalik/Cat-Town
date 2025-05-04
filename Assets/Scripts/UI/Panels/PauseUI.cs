using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public void ResumeGame()
    {
        GameManager.OnStateChanged(StateManager.GameState.Gameplay);
    }

    public void Restart()
    {
        GameManager.OnStartAgainClicked?.Invoke();
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
