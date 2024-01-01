using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

/*public class Levels : MonoBehaviour {
	[SerializeField] public Level[] levels;
}*/

[System.Serializable]
public class Level {
    [SerializeField] public LevelParameters[] levelParameters;
}

[RequireComponent(typeof(LevelTimer))]
public class LevelManager : MonoBehaviour
{
    [SerializeField] public Level[] levels;
    [SerializeField] private int currentLevelIndex = 0, currentLevelParamIndex = 0; 
    [SerializeField] private LevelTimer levelTimer; // todo, find this in awake/start by accessing child

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy any additional instances
        }

        //GameState.OnWaitingToStartStateEvent += SetLevelZero;
        GameState.OnPlayState       += SetLevelStart;

        LevelEvents.OnSetPlayerCKPT += InvokeOnPrepNewLevelEvent;
        LevelEvents.OnPlayerCKPT    += StartNextLevel;
    }

    private void OnDestroy() 
    {
        GameState.OnPlayState       -= SetLevelStart;

        LevelEvents.OnSetPlayerCKPT -= InvokeOnPrepNewLevelEvent;
        LevelEvents.OnPlayerCKPT    -= StartNextLevel;
    }

    /// <summary>
    /// Returns the LevelParameters object for the current level.
    /// </summary>
    /// <returns>The LevelParameters object for the current level.</returns>
    public LevelParameters GetCurrentLevelParameters()
    {
        return this.GetLevelParameters(currentLevelIndex, currentLevelParamIndex);
    }

    public LevelParameters GetNextLevelParameters()
    {
        (int levelIndex, int levelParamIndex) = this.FindNextLevelParametersPos(currentLevelIndex, currentLevelParamIndex);
        return this.GetLevelParameters(levelIndex, levelParamIndex);
    }

    /// <summary>
    /// Calculates the total duration of all levels.
    /// </summary>
    /// <returns>The total duration of all levels.</returns>
    public float GetAllLevelDurations()
    {
        float totalDuration = 0f;

        foreach (Level level in levels)
        {
            foreach (LevelParameters levelParameters in level.levelParameters)
            {
                totalDuration += levelParameters.levelDuration;
            }

            //totalDuration += 4f; // Add 4 seconds for the checkpoint
        }

        //totalDuration += 4f; // Add 4 seconds for the checkpoint


        return totalDuration;
    }

    /// <summary>
    /// Calculates the total duration of all levels up to the current level.
    /// </summary>
    /// <returns>The total duration of all levels up to the current level.</returns>
    public float GetLevelDurationsUntilCurrent()
    {
        float totalDuration = 0f;

        for (int i = 0; i < currentLevelIndex; i++)
        {
            foreach (LevelParameters levelParameters in levels[i].levelParameters)
            {
                totalDuration += levelParameters.levelDuration;
            }
        }

        return totalDuration;
    }

    private void OnEnable()
    {
        // Load current saved level index
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 0);
    }

    private void SetLevelZero()
    {
        LevelParameters levelParameters = this.GetLevelParameters(currentLevelIndex, currentLevelParamIndex);
        LevelEvents.InvokeOnNewLevel(levelParameters);
    }
    
    /// <summary>
    /// Sets the start of the current level by getting the level parameters, invoking the OnNewLevel event, and starting the level timer.
    /// </summary>
    private void SetLevelStart()
    {
        LevelParameters levelParameters = this.GetLevelParameters(currentLevelIndex, currentLevelParamIndex);
        LevelEvents.InvokeOnNewLevel(levelParameters);
        levelTimer.StartTimer(levelParameters.levelDuration);
    }

    private void InvokeOnPrepNewLevelEvent(Vector2 _)
    {
        (int levelIndex, int levelParamIndex) = this.FindNextLevelParametersPos(currentLevelIndex, currentLevelParamIndex);
        LevelParameters levelParameters = this.GetLevelParameters(levelIndex, levelParamIndex);

        LevelEvents.InvokeOnPrepNewLevelEvent(levelParameters);
    }

    /// <summary>
    /// Starts the next level by updating the level indexes, setting the current level index in PlayerPrefs,
    /// getting the level parameters, invoking the OnNewLevel event, and starting the level timer.
    /// </summary>
    public void StartNextLevel()
    {
        (currentLevelIndex, currentLevelParamIndex) = this.FindNextLevelParametersPos(currentLevelIndex, currentLevelParamIndex);
        PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex);

        LevelParameters levelParameters = this.GetLevelParameters(currentLevelIndex, currentLevelParamIndex);

        LevelEvents.InvokeOnNewLevel(levelParameters);
        levelTimer.StartTimer(levelParameters.levelDuration);
    }

    /// <summary>
    /// Finds and return LevelParameter for given index.
    /// </summary>
    private LevelParameters GetLevelParameters(int levelIndex, int levelParamIndex)
    {
        if (levels == null || levels.Length == 0)
        {
            Debug.LogError("Error Levels array empty.");
        }

        Level level = levels[levelIndex];

        if (level.levelParameters == null && level.levelParameters.Length < levelParamIndex)
        {
            Debug.LogError("First level missing LevelParameters.");
        }

        return level.levelParameters[levelParamIndex];
    }

    /// <summary>
    /// Finds position for next level parameter by given index.
    /// </summary>
    /// <returns>Position for next level parameter, or current values if no found.</returns>
    private (int, int) FindNextLevelParametersPos(int levelIndex, int levelParamIndex)
    {
        // Check if more LevelParameters on current level
        if ((levelIndex + 1) <= levels.Length)
        {
            Level level = levels[levelIndex];

            if ((levelParamIndex + 2) <= level.levelParameters.Length)
            {
                return (levelIndex, levelParamIndex + 1);
            }
        }

        // Else refer to next level
        if ((levelIndex + 2) <= levels.Length)
        {
            return (levelIndex + 1, 0);
        }

        // Else return current values
        return (levelIndex, levelParamIndex);
    }

}
