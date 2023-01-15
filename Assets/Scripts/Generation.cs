using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Handles spawning and movements of lineParts objects. Including calculate heights.
/// Does not handle anything directly related to the squares.
/// </summary>
public class Generation : MonoBehaviour
{
    [SerializeField] int tunnelWidth, tunnelHeight;
    [SerializeField] int maxHeightChange = 2;
    [SerializeField] GameObject linePart;
    [SerializeField] GameObject linePartsParent;
    
    private Vector2 latestSpawnedlinePartPosition;
    private int newHeight = 0;

    enum GameMode
    {
        Normal,
        Enemy,
        LowerTunnel
    }
    GameMode currentGameMode = GameMode.Normal;
    
    void Start()
    {
        SpawnStartLines();
        
        //InvokeRepeating("ChangeToRandomGameMode", 5.0f, 5.0f);
    }

    void ChangeToRandomGameMode()
    {
        currentGameMode = (GameMode)UnityEngine.Random.Range(0, Enum.GetNames(typeof(GameMode)).Length);
    }

    void SpawnStartLines()
    {
        // Spawn lineParts on X-axis
        for (int x = 0; x < tunnelWidth; x++)
        {
            // Get next random floor-height
            newHeight = UnityEngine.Random.Range((maxHeightChange / 2) * -1, (maxHeightChange / 2) + 1);

            // Update latest spawn position
            latestSpawnedlinePartPosition = new Vector2(x, newHeight);

            // Spawn 1 new linepart 
            GameObject newlinePart = SpawnLinePartObj(latestSpawnedlinePartPosition);
        }
    }

    public void SpawnNewLine()
    {
        // Get next random floor-height
        newHeight += UnityEngine.Random.Range((maxHeightChange / 2) * -1, (maxHeightChange / 2) + 1);
        // Update latest position
        latestSpawnedlinePartPosition = new Vector2(latestSpawnedlinePartPosition.x + 1, newHeight);
        // Spawn 1 new linepart 
        GameObject newlinePart = SpawnLinePartObj(latestSpawnedlinePartPosition);
    }

    GameObject SpawnLinePartObj(Vector2 position)
    {
        GameObject newlinePartObj = ObjectPool.SharedInstance.GetPooledObject();
        if (newlinePartObj != null)
        {
            newlinePartObj.transform.position = position;
            newlinePartObj.transform.parent = linePartsParent.transform;
            newlinePartObj.SetActive(true);
        }
        return newlinePartObj;
    }
}
