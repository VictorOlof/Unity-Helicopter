using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    private float timeLimit;
    private float currentTime = 0f;
    private bool isTimerRunning = false;

    public delegate void LevelTimerEvent();
    public static event LevelTimerEvent OnLevelTimerComplete; 

    void Awake()
    {
        GameState.OnDeadState += StopTimer;
    }

    private void OnDestroy() 
    {
        GameState.OnDeadState -= StopTimer;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= timeLimit)
            {
                isTimerRunning = false;
                currentTime = 0f;
                OnLevelTimerComplete?.Invoke();
            }
        }
    }

    public void StartTimer(float time)
    {
        timeLimit = time;
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        currentTime = 0f;
    }
}
