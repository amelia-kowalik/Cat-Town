using System;
using UnityEngine;

public class 
    GameManager : MonoBehaviour
{
    public static Action<float, float> OnHealthChanged;
    public static Action OnCatKidnapped;
    public static Action OnCoyoteDeath;
    public static Action OnBossDeath;
    public static Action OnLostGame;
    public static Action OnWonGame;
    public static Action<int> OnGoldChanged;
    public static Action<int> OnNextWave;
    public static Action<int> OnFoodBought;
    public static Action<int,int> OnCatKidnappedChanged;
    public static Action OnGameStarted;
    public static Action OnGameOver;
    public static Action OnStartAgainClicked;
    public static Action<StateManager.GameState> OnStateChanged;
    
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private StateManager stateManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private WaveManager waveManager;
    
    public ScoreManager ScoreManager => scoreManager;
    public HealthManager HealthManager => healthManager;
    public SpawnManager SpawnManager => spawnManager;
    public StateManager StateManager => stateManager;
    public UIManager UIManager => uiManager;
    public UpgradeManager UpgradeManager => upgradeManager;
    public WaveManager WaveManager => waveManager;

    private bool bossDefeated = false;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        OnNextWave += CheckWin;
        OnBossDeath += () => bossDefeated = true;
    }

    private void OnDestroy()
    {
        OnNextWave -= CheckWin;
        OnBossDeath -= () => bossDefeated = true;
    }

    private void CheckWin(int currentWave)
    {
        bool allCoyotesDefeated = FindObjectsOfType<Coyote>().Length == 0;
        if (currentWave == 10 && bossDefeated && allCoyotesDefeated )
        {
            HandleWin();
        }
    }

    private void HandleWin()
    {
        OnWonGame?.Invoke();
    }
}
