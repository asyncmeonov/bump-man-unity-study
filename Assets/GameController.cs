using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider tweakSlider;
    [SerializeField] float tweakDecay;
    [SerializeField] float bumpValue;

    private int score = 0;

    void Start()
    {
        tweakSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (tweakSlider.value > 0) tweakSlider.value  -= tweakDecay; //decay tweak timer
    }

    public void PickUpBump() 
    {
        score++;
        tweakSlider.value += bumpValue;
        scoreText.text = score.ToString().PadLeft(3,'0');
        Debug.Log("Score: "+score);
    } 
}
