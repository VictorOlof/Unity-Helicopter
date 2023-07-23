using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
   // [SerializeField] private LevelParameters[] levelParameters;
    //private LevelParameters currentLevelParameters;
    
    [SerializeField] private LevelTimer levelTimer; 
    // todo, find this in awake/start by accessing child
    public LevelSO LevelSO;

    void Awake()
    {
        //LevelParameters currentLevelParameters = levelParameters[currentLevelIndex];

        GameState.OnPlayState += SetLevelStart;
        LevelEvents.OnPlayerCKPT += StartNextLevel;
    }

    private void OnDestroy() 
    {
        GameState.OnPlayState -= SetLevelStart;
        LevelEvents.OnPlayerCKPT -= StartNextLevel;
    }

    private void SetLevelStart()
    {
        LevelParameters levelParameters = LevelSO.GetCurrentLevelParameters();
        LevelEvents.InvokeOnNewLevel();
        levelTimer.StartTimer(levelParameters.levelDuration);
    }

    public void StartNextLevel()
    {
        LevelSO.IncreaseCurrentLevelIndex(); // if return false, dont start timer?
        LevelParameters levelParameters = LevelSO.GetCurrentLevelParameters();

        LevelEvents.InvokeOnNewLevel();
        levelTimer.StartTimer(levelParameters.levelDuration);
    }

}
