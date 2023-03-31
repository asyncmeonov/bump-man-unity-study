using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    [SerializeField] private float _movSpeed = 2f;
    [SerializeField] private AudioClip[] _pickupSfx;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Vector2 _movDirection;
    private Animator _anim;
    private bool _isTweaking;

    public bool IsTweaking { get => _isTweaking; set => _isTweaking = value; }

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

        _isTweaking = false;
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame - good for gathering information, but never for updating sprites
    void Update()
    {
        _anim.SetBool("isTweaking", _isTweaking);
        _movDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        switch (_movDirection.x)
        {
            case < 0:
                _sr.flipX = true;

                break;
            case > 0:
                _sr.flipX = false;
                break;
        }


        _movSpeed = IsTweaking ? 3f : 2f;



    }

    void FixedUpdate()
    {
        _rb.velocity = _movDirection * _movSpeed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.StartsWith("Mob"))
        {
            if (_isTweaking)
            {
                MobSpawnerController.Instance.KillMob(other.gameObject);
                GameController.Instance.Score += 50;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Point")
        {
            StartCoroutine(UIController.Instance.PickUpBump());
            SoundController.Instance.PlayRandomSound(_pickupSfx);
            _anim.SetTrigger("hasPickedUpBump");
            Destroy(other.gameObject);
        }
    }
}
