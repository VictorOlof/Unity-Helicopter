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
    public Color squareColor = new Color(0.76f, 0.76f, 0.76f); //Color.black;

    // Info
    public string levelName = "Level 0";

    //Background colors
    public Color bgColor1 = new Color(0.76f, 0.76f, 0.76f); //Color.black;
    public Color bgColor2 = new Color(0.35f, 0.35f, 0.35f); //Color.white;
    public float bgColorFadeSpeed = 2f;

    // Music
    public AudioClip levelMusic;
}