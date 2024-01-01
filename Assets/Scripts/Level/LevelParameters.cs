using UnityEngine;

[CreateAssetMenu(fileName = "LevelParameters", menuName = "Game/Level Parameters")]
public class LevelParameters : ScriptableObject
{
    // Player
    public float playerSpeed = 5;

    // Timer
    public float levelDuration = 10;

    // Lines
    public int maxHeightChange = 2;
    public int tunnelGapHeight = 6;
    public int tunnelGapHeightRandomness = 0;

    // Squares
    public string squareColor = "black";

    // Info
    public string levelName = "Level 0";
}