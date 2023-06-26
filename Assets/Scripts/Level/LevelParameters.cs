using UnityEngine;

[CreateAssetMenu(fileName = "LevelParameters", menuName = "Game/Level Parameters")]
public class LevelParameters : ScriptableObject
{
    // Player
    public float playerSpeed;

    // Timer
    public float levelDuration;

    // Lines
    public bool menuMode;
    public int maxHeightChange;
}