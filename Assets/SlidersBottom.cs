using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlidersBottom : MonoBehaviour
{
    
    public Slider currentPositionSlider;
    public Slider highScoreSlider;

    public GameObject player;
    float bestPlayingTime = 0f;
    public float maxv1, maxv2;

    void Update()
    {
        currentPositionSlider.value = PlayerTime.Instance.CurrentPlayingTime;

        if (PlayerTime.Instance.CurrentPlayingTime > bestPlayingTime)
        {
            bestPlayingTime = PlayerTime.Instance.CurrentPlayingTime;
        }
        highScoreSlider.value = bestPlayingTime;
        
    }



    void Start()
    {
        currentPositionSlider.value    = PlayerTime.Instance.CurrentPlayingTime;
        currentPositionSlider.maxValue = LevelManager.Instance.GetAllLevelDurations();

        bestPlayingTime = PlayerPrefs.GetFloat("bestPlayingTime");

        highScoreSlider.value    = bestPlayingTime;
        highScoreSlider.maxValue = LevelManager.Instance.GetAllLevelDurations();

        maxv1 = LevelManager.Instance.GetAllLevelDurations();
    }
    
}
