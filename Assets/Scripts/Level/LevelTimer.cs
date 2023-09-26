using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    private float timeLimit;
    private float currentTime = 0f;
    private bool isTimerRunning = false;

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
                LevelEvents.InvokeLevelTimerComplete();
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
