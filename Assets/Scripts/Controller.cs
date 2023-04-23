using UnityEngine;
using System;

/// <summary>
/// Handles spawning and movements of lines objects. Including calculate heights.
/// Does not handle anything directly related to the squares.
/// </summary>
public class Controller : MonoBehaviour
{
    [SerializeField] int tunnelWidth;
    [SerializeField] int maxHeightChange = 2;
    [SerializeField] GameObject lineParent;
    
    private Vector2 latestSpawnedLinePosition;
    private int newRandomHeight = 0;
    
    void Start()
    {
        SpawnStartLines();
    }

    void SpawnStartLines()
    {
        for (int x = -21; x < (tunnelWidth - 21); x++)
        {
            newRandomHeight = UnityEngine.Random.Range((maxHeightChange / 2) * -1, (maxHeightChange / 2) + 1);
            latestSpawnedLinePosition = new Vector2(x, newRandomHeight);
            GameObject newline = SpawnLineObj(latestSpawnedLinePosition);
        }
    }

    public void SpawnNewLine()
    {
        if (GameState.PlayerState == PlayerStates.WaitingToStart)
        {
            newRandomHeight = UnityEngine.Random.Range((maxHeightChange / 2) * -1, (maxHeightChange / 2) + 1);
        }
        else
        {
            newRandomHeight += UnityEngine.Random.Range((maxHeightChange / 2) * -1, (maxHeightChange / 2) + 1);
        }
        latestSpawnedLinePosition = new Vector2(latestSpawnedLinePosition.x + 1, newRandomHeight);
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
