using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Level")]
public class LevelSO : ScriptableObject
{
    public float playerLineGoalXPos;
    public bool playerLineGoal = false;

    public void SetNewLineGoal(float xPosition)
    {
        playerLineGoalXPos = xPosition;
        playerLineGoal = true;
    }

    public void StopLineGoal()
    {
        playerLineGoal = false;
    }
}