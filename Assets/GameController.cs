using System.Collections;
using UnityEngine;


//Game state controller as a singleton
public class GameController : MonoBehaviour
{

    public static GameController Instance {get; private set;}

    [SerializeField] private AudioClip music;

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

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

    }
}
