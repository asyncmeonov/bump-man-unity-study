using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    [SerializeField] private float _movSpeed = 2f;
    [SerializeField] private AudioEvent _pickupSfx;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Vector2 _movDirection;
    private Animator _anim;
    private float _elapsedTime = 0f;
    private float _defaultMovSpeed = 2f;


    [Header("Tweaking Parameters")]
    [SerializeField] float _tweakDecay = 0.001f;
    [SerializeField] float _bumpValue = 0.1f;
    [SerializeField] float _tweakValue = 0f;
    [SerializeField] float _tweakThreshold;

    private bool _hasEnteredTweak = false;
    private bool _isTweaking;
    public bool IsTweaking { get => _isTweaking; set => _isTweaking = value; }
    public float TweakValue { get => _tweakValue; set => _tweakValue = value; }
    public float TweakThreshold { get => _tweakThreshold; set => _tweakThreshold = value; }

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
        _tweakValue = 0;
        _tweakThreshold = 0.99f;
        _isTweaking = false;
        _movSpeed = _defaultMovSpeed;
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

        if (_isTweaking && _tweakValue > 0)
        {
            _tweakValue -= 2 * _tweakDecay; // decrease twice as fast while tweaking
        }
        else if (!_isTweaking && _tweakValue > 0)
        {
            _tweakValue -= _tweakDecay;
        }

        if (IsTweaking)
        {
            _elapsedTime += Time.deltaTime;
            _movSpeed += _elapsedTime / 100;
        }
        else
        {
            _movSpeed = Mathf.MoveTowards(_movSpeed, _defaultMovSpeed, 0.01f);
            _elapsedTime = 0f;
        }



    }

    void FixedUpdate()
    {
        _rb.velocity = _movDirection * _movSpeed;

        //TODO logic for the tweak bar to fully deplete before you continue. Maybe just move the _isTweaking check and leverage the one-off _hasEnteredTweak
        _isTweaking = _tweakValue > _tweakThreshold;

        if (_isTweaking)
        {
            if (!_hasEnteredTweak)
            {
                GameObject[] mobs = GameObject.FindGameObjectsWithTag("mob");
                foreach (GameObject mob in mobs)
                {
                    mob.SendMessage("SetIsAfraid", true);
                }
                _hasEnteredTweak = true;
            }

        }
        else
        {
            GameObject[] mobs = GameObject.FindGameObjectsWithTag("mob");
            foreach (GameObject mob in mobs)
            {
                mob.SendMessage("SetIsAfraid", false);
            }
            _hasEnteredTweak = false;
        }
        // _isTweaking = _tweakValue > _tweakThreshold;

        // if (_isTweaking)
        // {
        //     if (!_hasEnteredTweak)
        //     {
        //         GameObject[] mobs = GameObject.FindGameObjectsWithTag("mob");
        //         foreach (GameObject mob in mobs)
        //         {
        //             mob.SendMessage("SetIsAfraid", true);
        //         }
        //         _hasEnteredTweak = true;
        //     }

        // }
        // else
        // {
        //     GameObject[] mobs = GameObject.FindGameObjectsWithTag("mob");
        //     foreach (GameObject mob in mobs)
        //     {
        //         mob.SendMessage("SetIsAfraid", false);
        //     }
        //     _hasEnteredTweak = false;
        // }
    }

    public void EnableCollisions(bool value) => gameObject.GetComponent<CircleCollider2D>().enabled = value;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.StartsWith("Mob"))
        {
            if (_isTweaking && other.gameObject.GetComponent<MobController>().GetIsAfraid())
            {
                MobSpawnerController.Instance.KillMob(other.gameObject);
                StartCoroutine(PickUp(_bumpValue * 2, 50, true));
                var source = _pickupSfx.Play(null);
            }
            else
            {
                GameController.Instance.GameOver();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Point")
        {
            StartCoroutine(PickUp(_bumpValue, 1, false));
            var source = _pickupSfx.Play(null); //unused source for now
            _anim.SetTrigger("hasPickedUpBump");
            Destroy(other.gameObject);
        }
    }

    public IEnumerator PickUp(float bumpValue, int scoreInc, bool ignoreTweaking)
    {
        GameController.Instance.Score += scoreInc;
        if (!_isTweaking || ignoreTweaking)
        {
            float targetSliderValue = Mathf.Clamp(_tweakValue + bumpValue, 0f, 0.99f);
            while (_tweakValue <= targetSliderValue)
            {
                _tweakValue = Mathf.MoveTowards(_tweakValue, _tweakValue + bumpValue, 0.01f);
                yield return null;
            }
        }

    }

}
