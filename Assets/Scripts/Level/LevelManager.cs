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

    public LevelParameters GetCurrentLevelParameters()
    {
        return this.GetLevelParameters(currentLevelIndex, currentLevelParamIndex);
    }

    private void OnDestroy() 
    {
        GameState.OnPlayState       -= SetLevelStart;

        LevelEvents.OnSetPlayerCKPT -= InvokeOnPrepNewLevelEvent;
        LevelEvents.OnPlayerCKPT    -= StartNextLevel;
    }

    private void OnEnable()
    {
        // Load current saved level
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 0);
        Debug.Log("------");
        Debug.Log(currentLevelIndex);
        LevelParameters levelParameters = this.GetLevelParameters(currentLevelIndex, currentLevelParamIndex);
        //LevelEvents.InvokeOnNewLevel(levelParameters);
    }

    private void SetLevelZero()
    {
        LevelParameters levelParameters = this.GetLevelParameters(currentLevelIndex, currentLevelParamIndex);
        LevelEvents.InvokeOnNewLevel(levelParameters);
    }
    
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

    public void StartNextLevel()
    {
        // Update levelIndexes
        (currentLevelIndex, currentLevelParamIndex) = this.FindNextLevelParametersPos(currentLevelIndex, currentLevelParamIndex);
        PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex);

        LevelParameters levelParameters = this.GetLevelParameters(currentLevelIndex, currentLevelParamIndex);

        LevelEvents.InvokeOnNewLevel(levelParameters);
        levelTimer.StartTimer(levelParameters.levelDuration);
    }

    /// <summary>
    ///  Finds and return LevelParameter for given index.
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
