using UnityEngine;

//Game state controller as a singleton
public class GameController : MonoBehaviour
{

    public static GameController Instance {get; private set;}

    [SerializeField] private AudioEvent _mainTheme;
    [SerializeField] private AudioEvent _tweakingTheme;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _pointsPrefab;

    private AudioSource _musicSource;

    private int score = 0;
    public int Score { get => score; set => score = value; }

    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        _musicSource = _mainTheme.Play(null);
   }

    public void StartGame()
    {
        score = 0;
        MobSpawnerController.Instance.KillAllMobs();
        Instantiate(_playerPrefab);
        Instantiate(_pointsPrefab);
        UIController.Instance.IsCameraAttached = true;
    }

    public void PlayMainTheme()
    {
        _mainTheme.Play(_musicSource);
    }

    public void PlayTweakTheme()
    {
        _tweakingTheme.Play(_musicSource);
    }

    public void GameOver()
    {   
        UIController.Instance.ShowEndGameScreen();
        Destroy(PlayerController.Instance.gameObject);
    }
}
