using UnityEngine;

[CreateAssetMenu(fileName = "LevelParameters", menuName = "Game/Level Parameters")]
public class LevelParameters : ScriptableObject
{
    // Player
    public float playerSpeed = 5;

    // Timer
    public float levelDuration = 10;

    // Lines
    public bool menuMode;
    public int maxHeightChange = 2;
    public int tunnelGapHeight = 6;
    public int tunnelGapHeightRandomness = 0;
}