using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Level")]
public class LevelSO : ScriptableObject
{
    /*
    // Levelparameters
    [SerializeField] private LevelParameters[] levelParameters;
    [SerializeField] private int currentLevelIndex = 0; 

    public LevelParameters GetCurrentLevelParameters()
    {
        return levelParameters[currentLevelIndex];
    }

    public LevelParameters GetUpcomingLevelParameters()
    {
        if ((currentLevelIndex + 1) < levelParameters.Length)
        {
            return levelParameters[currentLevelIndex + 1];
        }
        Debug.Log("LevelSO: GetUpcomingLevelParameters fail");
        return levelParameters[currentLevelIndex];
    }

    public void IncreaseCurrentLevelIndex()
    {
        if ((currentLevelIndex + 1) < levelParameters.Length)
        {
            currentLevelIndex += 1;
            Debug.Log("LevelSO: IncreaseCurrentLevelIndex: " + currentLevelIndex);
        }
        else
        {
            Debug.Log("LevelSO: IncreaseCurrentLevelIndex: OUTOFBOUNDS");
        }

        
    }
    */
    /*

    // PlayerCKPT
    public float playerLineGoalXPos;
    public bool playerLineGoal = false;

    public void SetPlayerCKPT(float xPosition)
    {
        playerLineGoalXPos = xPosition;
        playerLineGoal = true;

        LevelEvents.InvokeOnSetPlayerCKPT();
    }

    public void RemovePlayerCKPT()
    {
        playerLineGoal = false;
    }
    */
}