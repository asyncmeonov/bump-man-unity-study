using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }


    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] Slider _tweakSlider;
    [SerializeField] Image _fireIcon;
    [SerializeField] Image _flashImg;
    [SerializeField] AudioClip _zoomWooshSfx;

    //Music



    float _tweakDecay;
    float _bumpValue;
    private bool _hasEnteredTweak = false;

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
        _flashImg.gameObject.SetActive(true);
        _flashImg.color = new Color(1, 1, 1, 0);
        _fireIcon.enabled = false;
        _tweakSlider.value = 0;
        _tweakDecay = 0.001f;
        _bumpValue = 0.1f;
    }

    void Update()
    {
        if (PlayerController.Instance.IsTweaking && _tweakSlider.value > 0)
        {
            _tweakSlider.value -= 2 * _tweakDecay;
        }
        else if (!PlayerController.Instance.IsTweaking && _tweakSlider.value > 0)
        {
            _tweakSlider.value -= _tweakDecay;
        }
        _scoreText.text = GameController.Instance.Score.ToString().PadLeft(3, '0');
    }

    void FixedUpdate()
    {
        //consider moving the tweakertriggering logic to the Player Controller and make the UI only reflect it
        if (_tweakSlider.value > 0.7)
        {
            //we are tweaking!
            _fireIcon.enabled = true;
            PlayerController.Instance.IsTweaking = true;
            if (!_hasEnteredTweak)
            {
                StartCoroutine(FlashBang());
            }
        }
        else
        {
            if (!_hasEnteredTweak)
            {
                // _zoomWooshSfx.name = "woosh_reversed";
                // SoundController.Instance.PlaySound(_zoomWooshSfx,false,-1);
                //TODO figure out how to play this reversed, maybe just use a separate clip
            }
            _hasEnteredTweak = false;
            _fireIcon.enabled = false;
            PlayerController.Instance.IsTweaking = false;
        }
    }


    public IEnumerator PickUpBump()
    {
        GameController.Instance.Score += 1;
        float targetSliderValue = Mathf.Clamp(_tweakSlider.value + _bumpValue, 0f, 0.99f);
        while (_tweakSlider.value <= targetSliderValue)
        {
            _tweakSlider.value = Mathf.MoveTowards(_tweakSlider.value, _tweakSlider.value + _bumpValue, 0.01f);
            yield return null;
        }
    }

    public IEnumerator FlashBang()
    {
        SoundController.Instance.PlaySound(_zoomWooshSfx);
        _hasEnteredTweak = true;
        _flashImg.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.25f);
        while (_flashImg.color.a > 0)
        {
            _flashImg.color = new Color(1, 1, 1, Mathf.MoveTowards(_flashImg.color.a, 0f, 0.001f));
        }
    }
}
