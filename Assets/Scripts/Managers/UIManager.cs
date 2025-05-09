using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] screens; //[ Menu, Gameplay, Pause, GameOver, Victory]
    
    void Start()
    {
        GameManager.OnStateChanged += OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnStateChanged -= OnGameStateChanged;
    }
    
    private void OnGameStateChanged(StateManager.GameState state)
    {
        switch (state)
        {
            case StateManager.GameState.Menu:
                Time.timeScale = 0;
                ShowMenu();
                break;
            case StateManager.GameState.Gameplay:
                Time.timeScale = 1;
                StartGameplay();
                break;
            case StateManager.GameState.Pause:
                Time.timeScale = 0;
                PauseGameplay();
                return;
            case StateManager.GameState.Gameover:
                Time.timeScale = 0;
                ShowGameOverScreen();
                break;
            case StateManager.GameState.Victory:
                Time.timeScale = 0;
                ShowVictoryScreen();
                break;
            default:
                Debug.Log("Unknown state");
                break;
        }
    }

    private void ShowScreen(int index)
    {
        for (int i = 0; i < screens.Length; i++)
        {
            screens[i].SetActive(i == index);
        }
    }
    
    private void ShowMenu()
    {
        ShowScreen(0);
    }

    private void StartGameplay()
    {
        ShowScreen(1);
    }

    private void PauseGameplay()
    {
        ShowScreen(2);
    }

    private void ShowGameOverScreen()
    {
        ShowScreen(3);
    }
    
    private void ShowVictoryScreen()
    {
        ShowScreen(4);
    }
}

