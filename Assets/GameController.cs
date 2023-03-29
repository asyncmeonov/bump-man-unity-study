using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//Game state controller as a singleton
//Doubles down as a UI controller (maybe separate them later?)
public class GameController : MonoBehaviour
{

    public static GameController Instance {get; private set;}
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider tweakSlider;
    [SerializeField] float tweakDecay;
    [SerializeField] float bumpValue;
    [SerializeField] Image fireIcon;
    [SerializeField] Image flashImg;

    private int score = 0;
    private bool _hasEnteredTweak = false;

    private Camera _playerCam;
    
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
        flashImg.gameObject.SetActive(true);
        flashImg.color = new Color(1,1,1,0);
        fireIcon.enabled = false;
        tweakSlider.value = 0;
        _playerCam = PlayerController.Instance.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance.IsTweaking && tweakSlider.value > 0)
        {
            tweakSlider.value -= 2 * tweakDecay;
        }
        else if (!PlayerController.Instance.IsTweaking && tweakSlider.value > 0)
        {
            tweakSlider.value -= tweakDecay;
        }
        scoreText.text = score.ToString().PadLeft(3, '0');
    }

    void FixedUpdate()
    {
        if (tweakSlider.value > 0.7)
        {
            //we are tweaking!
            fireIcon.enabled = true;
            PlayerController.Instance.IsTweaking = true;
            if(!_hasEnteredTweak)
            {
                StartCoroutine(FlashBang());
                SoundController.Instance.PlayZoomWoosh();
            }
        }
        else
        {
            _hasEnteredTweak = false;
            fireIcon.enabled = false;
            PlayerController.Instance.IsTweaking = false;
        }
    }

    public void IncreaseScore(int factor = 1)
    {
        score = score + factor;
    }

    public IEnumerator PickUpBump()
    {
        IncreaseScore();
        float targetSliderValue = Mathf.Clamp(tweakSlider.value + bumpValue, 0f, 0.99f);
        while (tweakSlider.value <= targetSliderValue)
        {
            tweakSlider.value = Mathf.MoveTowards(tweakSlider.value, tweakSlider.value + bumpValue, 0.01f);
            yield return null;
        }
    }

    public IEnumerator FlashBang()
    {
        _hasEnteredTweak = true;
        flashImg.color = new Color(1,1,1,1);
        yield return new WaitForSeconds(0.25f);
        while (flashImg.color.a > 0)
        {
            flashImg.color = new Color(1,1,1,Mathf.MoveTowards(flashImg.color.a, 0f, 0.001f));
        }
    }
}
