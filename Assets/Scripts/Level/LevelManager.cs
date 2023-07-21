using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelParameters[] levelParameters;
    private LevelParameters currentLevelParameters;
    private int currentLevelIndex = 0; 
    [SerializeField] private LevelTimer levelTimer;

    
    
     


    void Awake()
    {
        currentLevelParameters = levelParameters[currentLevelIndex];

        GameState.OnPlayState += StartNextLevel;
        LevelEvents.OnPlayerCKPT += StartNextLevel;
    }

    private void OnDestroy() 
    {
        GameState.OnPlayState -= StartNextLevel;
        LevelEvents.OnPlayerCKPT -= StartNextLevel;
    }

    public void StartNextLevel()
    {
        if ((currentLevelIndex + 1) < levelParameters.Length)
        {
            Debug.Log("level: " + currentLevelIndex);

            currentLevelParameters = levelParameters[currentLevelIndex];
            LevelEvents.InvokeLevelParamChanged(currentLevelParameters);
            levelTimer.StartTimer(currentLevelParameters.levelDuration);

            currentLevelIndex++;
        }
        else if ((currentLevelIndex + 1) == levelParameters.Length)
        {
            Debug.Log("End level: " + currentLevelIndex);

            currentLevelParameters = levelParameters[currentLevelIndex];
            LevelEvents.InvokeLevelParamChanged(currentLevelParameters);
            // Game finished, crash into wall of squares? Huge explosion.
        }
        else
        {
            Debug.Log("Invalid currentLevelIndex");
        }
    }

    public LevelParameters getCurrentLevelParameters()
    {
        return currentLevelParameters;
    }
}
