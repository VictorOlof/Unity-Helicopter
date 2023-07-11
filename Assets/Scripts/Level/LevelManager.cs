using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private LevelParameters[] levelParameters;
    private LevelParameters currentLevelParameters;
    private int currentLevelIndex = 0; 
    public LevelTimer levelTimer;

    public delegate void NewLevelEvent(LevelParameters levelParameter);
    public static event NewLevelEvent OnLevelParamChanged; 
    


    void Awake()
    {
        currentLevelParameters = levelParameters[currentLevelIndex];

        GameState.OnPlayState += StartNextLevel;
        LevelTimer.OnLevelTimerComplete += StartNextLevel;
    }

    private void OnDestroy() 
    {
        GameState.OnPlayState -= StartNextLevel;
        LevelTimer.OnLevelTimerComplete += StartNextLevel;
    }

    public void StartNextLevel()
    {
        if ((currentLevelIndex + 1) < levelParameters.Length)
        {
            Debug.Log("level: " + currentLevelIndex);

            currentLevelParameters = levelParameters[currentLevelIndex];
            OnLevelParamChanged?.Invoke(currentLevelParameters);
            levelTimer.StartTimer(currentLevelParameters.levelDuration);

            currentLevelIndex++;
        }
        else if ((currentLevelIndex + 1) == levelParameters.Length)
        {
            Debug.Log("End level: " + currentLevelIndex);

            currentLevelParameters = levelParameters[currentLevelIndex];
            OnLevelParamChanged?.Invoke(currentLevelParameters);
            // Game finished, crash into wall of squares? Huge explosion.
        }
        else
        {
            // Invalid
            Debug.Log("Invalid currentLevelIndex");
        }
    }

    public LevelParameters getCurrentLevelParameters()
    {
        return currentLevelParameters;
    }



    /*
    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        if (levelManager == null)
        {
            Debug.LogError("LevelManager not found in the scene.");
        }
        
        StartLevel(currentLevelIndex);
        LevelTimer.OnTimerComplete += OnTimerComplete;

        if (OneMoreLevel)
        {
            OnNewLevel?.Invoke();
        }
    }

    private bool OneMoreLevel()
    {
        return (currentLevelIndex + 1) < levelManager.GetNumOfLevels();
    }

    private void OnDestroy()
    {
        LevelTimer.OnTimerComplete -= OnTimerComplete;
    }

    private void StartLevel(int levelIndex)
    {
        currentLevelParams = levelManager.GetCurrentLevelParameters(levelIndex);
        levelTimer.StartTimer(currentLevelParams.levelDuration);
    }

    private void OnTimerComplete()
    {
        levelTimer.StopTimer();

        if ((currentLevelIndex + 1) < levelManager.GetNumOfLevels())
        {
            currentLevelIndex++;
            StartLevel(currentLevelIndex);
        }
        else
        {
            Debug.Log("All levels completed.");
            // Handle game completion or restart here
        }
    }
    */
}
