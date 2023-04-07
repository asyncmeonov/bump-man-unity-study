using UnityEngine;

//Game state controller as a singleton
public class GameController : MonoBehaviour
{

    public static GameController Instance { get; private set; }

    [SerializeField] private AudioEvent _mainTheme;
    [SerializeField] private AudioEvent _tweakingTheme;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _pointsPrefab;

    private GameObject _points;

    private AudioSource _musicSource;

    private bool _isGameRunning = false;

    private int score = 0;
    public int Score { get => score; set => score = value; }

    public bool IsGameRunning { get => _isGameRunning; private set => _isGameRunning = value; }

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
        _musicSource = _mainTheme.Play(null).GetComponent<AudioSource>();
    }

    void Update()
    {
        if(_isGameRunning && _points.transform.childCount == 0)
        {
            GameWon();
        }
    }

    public void StartGame()
    {
        score = 0;
        MobSpawnerController.Instance.KillAllMobs();
        Instantiate(_playerPrefab);
        _points = Instantiate(_pointsPrefab);
        UIController.Instance.IsCameraAttached = true;
        _isGameRunning = true;
    }

    public void PlayMainTheme()
    {
        _mainTheme.Play(_musicSource);
    }

    public void PlayTweakTheme()
    {
        _tweakingTheme.Play(_musicSource);
    }

    public void GameOver(bool isVictory = false)
    {
        UIController.Instance.ShowEndGameScreen(isVictory);
        Destroy(PlayerController.Instance.gameObject);
        _isGameRunning = false;
    }

    public void GameWon() => GameOver(true);
}
