using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    
    public GameState currentState;
    
    public enum GameState
    {
        Menu,
        Gameplay,
        Pause,
        Gameover,
        Victory
    }
    private Dictionary<GameState, List<GameState>> stateTransitions = new()
    {
        {GameState.Menu, new List<GameState>() { GameState.Gameplay }},
        { GameState.Gameplay , new List<GameState>() { GameState.Pause, GameState.Gameover, GameState.Victory}},
        { GameState.Pause, new List<GameState>() { GameState.Gameplay }},
        { GameState.Gameover, new List<GameState>() { GameState.Menu }},
        { GameState.Victory, new List<GameState>() { GameState.Menu }},
    };

    void Start()
    {
        currentState = GameState.Menu;
        Debug.Log($"Initial game state set to: {currentState}");
        GameManager.OnStateChanged?.Invoke(currentState);
        GameManager.OnGameStarted += OnStartedGameplay;
        GameManager.OnStartAgainClicked += OnStartAgain;
        GameManager.OnLostGame += HandleLostGame;
        GameManager.OnWonGame += HandleWonGame;
    }

    void OnDestroy()
    {
        GameManager.OnGameStarted -= OnStartedGameplay;
        GameManager.OnStartAgainClicked -= OnStartAgain;
        GameManager.OnLostGame -= HandleLostGame;
        GameManager.OnWonGame -= HandleWonGame;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Pause)
            {
                ChangeState(GameState.Gameplay);
            }
            else if (currentState == GameState.Gameplay)
            {
                ChangeState(GameState.Pause);
            }
        }
    }

    public void ChangeState(GameState newState)
    {
        if (stateTransitions[currentState].Contains(newState))
        {
            currentState = newState;
            GameManager.OnStateChanged?.Invoke(currentState);
        }
        else
        {
            Debug.Log("Invalid state transition");
        }
    }

    private void OnStartAgain()
    {
        ChangeState(GameState.Gameplay);
        SceneManager.LoadScene(0);
    }
    private void OnStartedGameplay()
    {
        ChangeState(GameState.Gameplay);
    }

    private void HandleLostGame()
    {
        ChangeState(GameState.Gameover);
    }
    
    private void HandleWonGame()
    {
        ChangeState(GameState.Victory);
    }
}
