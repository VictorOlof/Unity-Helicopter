/// <summary>
/// Keeps track of the player's playing time and saves the best playing time to PlayerPrefs.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTime : MonoBehaviour
{
    float currentPlayingTime = 0;
    //public Slider currentPosition;

    // get method for currentPlayingTime
    public float CurrentPlayingTime
    {
        get { return currentPlayingTime; }
    }
    
    public static PlayerTime Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GameState.OnDeadState += SaveBestPlayingTime;
    }

    void OnDestroy()
    {
        GameState.OnDeadState -= SaveBestPlayingTime;
    }

    void Start()
    {
        AddTimeFromPreviousLevels();
    }

    void Update()
    {
        switch (GameState.PlayerState)
        {
            case PlayerStates.Playing:
                currentPlayingTime += Time.deltaTime;
                break;
        }
    }
    
    private void SaveBestPlayingTime()
    {
        float bestPlayingTime = PlayerPrefs.GetFloat("bestPlayingTime");

        if (currentPlayingTime > bestPlayingTime)
        {
            PlayerPrefs.SetFloat("bestPlayingTime", currentPlayingTime);
        }
    }

    private void AddTimeFromPreviousLevels()
    {
        currentPlayingTime += LevelManager.Instance.GetLevelDurationsUntilCurrent();
    }
}
