using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header( "Audio Sources" )]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource uiSource;
    [Header( "Audio Clips" )]
    [SerializeField] private AudioClip menuBackground;
    [SerializeField] private AudioClip gameBackground;
    [SerializeField] private AudioClip gameOverMusic;
    [SerializeField] private AudioClip victoryMusic;
    

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
                PlayMenuMusic();
                break;
            case StateManager.GameState.Gameplay:
                if (musicSource.clip == gameBackground && !musicSource.isPlaying)
                {
                    ResumeMusic();
                }
                else
                {
                    PlayGameplayMusic();
                }

                break;
            case StateManager.GameState.Pause:
                PauseMusic();
                break;
            case StateManager.GameState.Gameover:
                PlayGameOverMusic();
                break;
            case StateManager.GameState.Victory:
                PlayVictoryMusic();
                break;
            default:
                Debug.Log("Unknown state");
                break;
        }
    }
    
    private void PlayMenuMusic()
    {
        musicSource.clip = menuBackground;
        musicSource.Play();
    }
    
    private void PlayGameplayMusic(){
        musicSource.clip = gameBackground;
        musicSource.Play();
    }
    
    private void PlayGameOverMusic()
    {
        musicSource.clip = gameOverMusic;
        musicSource.Play();
    }

    private void PlayVictoryMusic()
    {
        musicSource.clip = victoryMusic;
        musicSource.Play();
    }

    private void PauseMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }
    private void ResumeMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }
    
}
