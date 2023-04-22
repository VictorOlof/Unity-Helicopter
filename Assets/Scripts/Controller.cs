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
    [SerializeField] GameObject line,lineParent;
    
    private Vector2 latestSpawnedLinePosition;
    private int newHeight = 0;
    public PlayerScriptableObject playerSO;
    
    void Start()
    {
        SpawnStartLines();
        Application.targetFrameRate = 60;
    }

    void SpawnStartLines()
    {
        // Spawn lines on X-axis
        for (int x = -21; x < (tunnelWidth - 21); x++)
        {
            // Get next random floor-height
            newHeight = UnityEngine.Random.Range((maxHeightChange / 2) * -1, (maxHeightChange / 2) + 1);
            // Update latest spawn position
            latestSpawnedLinePosition = new Vector2(x, newHeight);
            // Spawn 1 new line 
            GameObject newline = SpawnLineObj(latestSpawnedLinePosition);
            
            //Line lineScript = (Line)newline.GetComponent(typeof(Line));
            //lineScript.lineMovement = true;
        }
    }

    public void SpawnNewLine()
    {
        if (GameState.PlayerState == PlayerStates.WaitingToStart)
        {
            // Get next random floor-height
            newHeight = UnityEngine.Random.Range((maxHeightChange / 2) * -1, (maxHeightChange / 2) + 1);
        }
        else
        {
            // Get next random floor-height
            newHeight += UnityEngine.Random.Range((maxHeightChange / 2) * -1, (maxHeightChange / 2) + 1);
        }
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
