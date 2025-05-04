using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class StateManager : MonoBehaviour
{
    
    public GameState currentState;
    
    public enum GameState
    {
        Menu,
        Gameplay,
        Pause,
        Gameover
    }
    private Dictionary<GameState, List<GameState>> stateTransitions = new()
    {
        {GameState.Menu, new List<GameState>() { GameState.Gameplay }},
        { GameState.Gameplay , new List<GameState>() { GameState.Pause, GameState.Gameover}},
        { GameState.Pause, new List<GameState>() { GameState.Gameplay }},
        { GameState.Gameover, new List<GameState>() { GameState.Menu }}
    };

    void Start()
    {
        currentState = GameState.Menu;
        Debug.Log($"Initial game state set to: {currentState}");
        GameManager.OnStateChanged?.Invoke(currentState);

        GameManager.OnGameStarted += OnStartedGameplay;
    }

    void OnDestroy()
    {
        GameManager.OnGameStarted -= OnStartedGameplay;
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

    private void OnStartedGameplay()
    {
        ChangeState(GameState.Gameplay);
    }
}
