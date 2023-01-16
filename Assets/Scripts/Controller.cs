using UnityEngine;
using System;

/// <summary>
/// Handles spawning and movements of lines objects. Including calculate heights.
/// Does not handle anything directly related to the squares.
/// </summary>
public class Controller : MonoBehaviour
{
    [SerializeField] int tunnelWidth, tunnelHeight;
    [SerializeField] int maxHeightChange = 2;
    [SerializeField] GameObject line;
    [SerializeField] GameObject lineParent;
    
    private Vector2 latestSpawnedLinePosition;
    private int newHeight = 0;

    private static Controller instance;
    public static Controller GetInstance() 
    {
        return instance;
    }
    public event EventHandler GameModeEvent;

    enum GameMode
    {
        Normal,
        Enemy,
        LowerTunnel
    }
    GameMode currentGameMode = GameMode.Normal;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        SpawnStartLines();
        InvokeRepeating("ChangeToRandomGameMode", 5.0f, 5.0f);
    }

    void ChangeToRandomGameMode()
    {
        currentGameMode = (GameMode)UnityEngine.Random.Range(0, Enum.GetNames(typeof(GameMode)).Length);
        if (GameModeEvent != null) GameModeEvent(this, EventArgs.Empty);
    }

    void SpawnStartLines()
    {
        // Spawn lines on X-axis
        for (int x = 0; x < tunnelWidth; x++)
        {
            // Get next random floor-height
            newHeight = UnityEngine.Random.Range((maxHeightChange / 2) * -1, (maxHeightChange / 2) + 1);
            // Update latest spawn position
            latestSpawnedLinePosition = new Vector2(x, newHeight);
            // Spawn 1 new line 
            GameObject newline = SpawnLineObj(latestSpawnedLinePosition);
        }
    }

    public void SpawnNewLine()
    {
        // Get next random floor-height
        newHeight += UnityEngine.Random.Range((maxHeightChange / 2) * -1, (maxHeightChange / 2) + 1);
        // Update latest position
        latestSpawnedLinePosition = new Vector2(latestSpawnedLinePosition.x + 1, newHeight);
        // Spawn 1 new line 
        GameObject newline = SpawnLineObj(latestSpawnedLinePosition);
    }

    GameObject SpawnLineObj(Vector2 position)
    {
        GameObject newlineObj = ObjectPool.SharedInstance.GetPooledObject();
        if (newlineObj != null)
        {
            newlineObj.transform.position = position;
            newlineObj.transform.parent = lineParent.transform;
            newlineObj.SetActive(true);
        }
        return newlineObj;
    }
}
