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
    [SerializeField] GameObject player;
    [SerializeField] Image fireIcon;

    private int score = 0;

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
        fireIcon.enabled = false;
        tweakSlider.value = 0;
        _playerCam = player.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().GetIsTweaking() && tweakSlider.value > 0)
        {
            tweakSlider.value -= 2 * tweakDecay;
        }
        else if (!player.GetComponent<PlayerController>().GetIsTweaking() && tweakSlider.value > 0)
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
            player.SendMessage("SetIstweaking", true);
            //camera logic
            _playerCam.orthographicSize = Mathf.MoveTowards(_playerCam.orthographicSize, 11f, 0.2f);
            float tempXZoomOut = Mathf.MoveTowards(_playerCam.transform.position.x, 0f, 0.2f);
            _playerCam.transform.position = new Vector3(tempXZoomOut, _playerCam.transform.position.y, _playerCam.transform.position.z);
        }
        else
        {
            fireIcon.enabled = false;
            player.SendMessage("SetIstweaking", false);
            //camera logic
            _playerCam.orthographicSize = Mathf.MoveTowards(_playerCam.orthographicSize, 2f, 0.1f);
            float tempXZoomIn = Mathf.MoveTowards(_playerCam.transform.position.x, player.transform.position.x, 0.1f);
            float tempYZoomIn = Mathf.MoveTowards(_playerCam.transform.position.y, player.transform.position.y, 0.2f);
            _playerCam.transform.position = new Vector3(tempXZoomIn, tempYZoomIn, -3);
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
}
