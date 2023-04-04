using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//Game state controller as a singleton
public class GameController : MonoBehaviour
{

    public static GameController Instance {get; private set;}

    [SerializeField] private AudioClip music;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _pointsPrefab;

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
        SoundController.Instance.PlaySound(music, true);
    }

    public void StartGame()
    {
        score = 0;
        Instantiate(_playerPrefab);
        Instantiate(_pointsPrefab);
        UIController.Instance.IsCameraAttached = true;
    }

    public void GameOver()
    {   
        UIController.Instance.ShowEndGameScreen();
        Destroy(PlayerController.Instance.gameObject);
    }
}
