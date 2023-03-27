using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider tweakSlider;
    [SerializeField] float tweakDecay;
    [SerializeField] float bumpValue;
    [SerializeField] GameObject player;
    [SerializeField] Image fireIcon;

    private int score = 0;

    private Camera playerCam;

    void Start()
    {
        fireIcon.enabled = false;
        tweakSlider.value = 0;
        playerCam = player.GetComponentInChildren<Camera>();
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

    }

    void FixedUpdate()
    {
        if (tweakSlider.value > 0.7)
        {
            //we are tweaking!
            fireIcon.enabled = true;
            player.SendMessage("SetIstweaking", true);
            //camera logic
            playerCam.orthographicSize = Mathf.MoveTowards(playerCam.orthographicSize, 11f, 0.2f);
            float tempXZoomOut = Mathf.MoveTowards(playerCam.transform.position.x, 0f, 0.2f);
            playerCam.transform.position = new Vector3(tempXZoomOut, playerCam.transform.position.y, playerCam.transform.position.z);
        }
        else
        {
            fireIcon.enabled = false;
            player.SendMessage("SetIstweaking", false);
            //camera logic
            playerCam.orthographicSize = Mathf.MoveTowards(playerCam.orthographicSize, 2f, 0.1f);
            float tempXZoomIn = Mathf.MoveTowards(playerCam.transform.position.x, player.transform.position.x, 0.1f);
            float tempYZoomIn = Mathf.MoveTowards(playerCam.transform.position.y, player.transform.position.y, 0.2f);
            playerCam.transform.position = new Vector3(tempXZoomIn, tempYZoomIn, -3);
        }
    }

    public IEnumerator PickUpBump()
    {
        score++;
        scoreText.text = score.ToString().PadLeft(3, '0');
        float targetSliderValue = Mathf.Clamp(tweakSlider.value + bumpValue, 0f, 0.99f);
        while (tweakSlider.value <= targetSliderValue)
        {
            tweakSlider.value = Mathf.MoveTowards(tweakSlider.value, tweakSlider.value + bumpValue, 0.01f);
            yield return null;
        }
    }
}
