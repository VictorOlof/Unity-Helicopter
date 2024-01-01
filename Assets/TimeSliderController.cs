using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LevelManager))]
public class TimerSliderController : MonoBehaviour
{
    /*
    public Slider timerSlider;
    public float totalTime = 60f; // Total time in seconds

    private float currentTime;

    void Start()
    {
        currentTime = LevelManager.Instance.GetAllLevelDurations();
    }

    void Update()
    {
        if (currentTime < totalTime)
        {
            currentTime += Time.deltaTime;
            UpdateSliderValue();
        }
        else
        {
            // Timer has reached one minute, you can perform any actions here
            Debug.Log("One minute has passed!");
        }
    }

    void UpdateSliderValue()
    {
        float sliderValue = currentTime / totalTime;
        timerSlider.value = sliderValue;
    }
    */
}
