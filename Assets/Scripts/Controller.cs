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

    enum GameMode
    {
        Start,
        LowTunnel,
        HighTunnel
    }
    GameMode currentGameMode;

    //public delegate void ClickAction();
    //public static event ClickAction OnClicked;

    void Awake()
    {
        currentGameMode = GameMode.Start;
    }
    
    void Start()
    {
        SpawnStartLines();
        InvokeRepeating("ChangeToRandomGameMode", 1.0f, 1.0f);
    }

    void ChangeToRandomGameMode()
    {
        currentGameMode = (GameMode)UnityEngine.Random.Range(0, Enum.GetNames(typeof(GameMode)).Length);
        //if(OnClicked != null)
        //    OnClicked();
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
