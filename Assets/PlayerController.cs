using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _movSpeed = 2f;
    [SerializeField] private Sprite[] sprites;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Vector2 _movDirection;
    private Animator _anim;

    private bool _isTweaking;


    public void SetIstweaking(bool value)
    {
        _isTweaking = value;
    }
    public bool GetIsTweaking() => _isTweaking;
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
        _movDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (_movDirection.x < 0)
        {
            _sr.flipX = true;

        }
        else if (_movDirection.x > 0)
        {
            _sr.flipX = false;
        }

        //TODO this does not work because the Animator is overriding it
        //I might need to put the Animator in a separate object
        //TODO research animation controllers
        if (_isTweaking)
        {
            _sr.sprite = sprites[0];
        }
        else
        {
            _sr.sprite = sprites[1];
        }

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
                GameController.Instance.IncreaseScore(50);
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
            StartCoroutine(GameController.Instance.PickUpBump());
            SoundController.Instance.PlayPickupSound();
            _anim.SetTrigger("hasPickedUpBump");
            Destroy(other.gameObject);
        }
    }
}
