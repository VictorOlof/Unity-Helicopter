using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Handles spawning and movements of lines objects. Including calculate heights.
/// Does not handle anything directly related to the squares.
/// </summary>
public class LineManager : MonoBehaviour
{
    [SerializeField] private int tunnelWidth;
    [SerializeField] public int maxHeightChange;
    [SerializeField] GameObject lineParent;
    public int newRandomHeight;
    public int spawningHeight;

    public Vector2 latestSpawnedLinePosition;

    private void Awake()
    {
        LevelManager.OnLevelParamChanged += UpdateParams;
    }
    private void OnDestroy() 
    {
        LevelManager.OnLevelParamChanged -= UpdateParams;
    }

    private void UpdateParams(LevelParameters currentLevelParameters)
    {
        maxHeightChange = currentLevelParameters.maxHeightChange;
    }
    
    void Start()
    {
        //todo UpdateParams(LevelManager.getCurrentLevelParameters());
        maxHeightChange = 2;

        SpawnStartLines();
    }

    void SpawnStartLines()
    {
        for (int x = -21; x < (tunnelWidth - 21); x++)
        {
            /*
            newRandomHeight = UnityEngine.Random.Range((maxHeightChange / 2) * -1, (maxHeightChange / 2) + 1);
            latestSpawnedLinePosition = new Vector2(x, newRandomHeight);
            GameObject newline = SpawnLineObj(latestSpawnedLinePosition);
            */
            SpawnNewLine();
        }
    }

    public void SpawnNewLine()
    {
        
        if (GameState.PlayerState == PlayerStates.WaitingToStart)
        {
            //spawningHeight = UnityEngine.Random.Range((maxHeightChange / 2) * -1, (maxHeightChange / 2) + 1);
            spawningHeight = UnityEngine.Random.Range(0, maxHeightChange +1);

            latestSpawnedLinePosition = new Vector2(latestSpawnedLinePosition.x + 1, spawningHeight);
            SpawnLineObj(latestSpawnedLinePosition);
        }
        else if (GameState.PlayerState == PlayerStates.Playing)
        {
            newRandomHeight = UnityEngine.Random.Range((maxHeightChange / 2) * -1, (maxHeightChange / 2) + 1);
            spawningHeight += newRandomHeight;

            latestSpawnedLinePosition = new Vector2(latestSpawnedLinePosition.x + 1, spawningHeight);
            SpawnLineObj(latestSpawnedLinePosition);
        }
        
        
        
        /* Needs Level Manager obj to be enabled
        if (menuMode)
        {
            newRandomHeight = UnityEngine.Random.Range((maxHeightChange / 2) * -1, (maxHeightChange / 2) + 1);
        }
        else
        {
            newRandomHeight += UnityEngine.Random.Range((maxHeightChange / 2) * -1, (maxHeightChange / 2) + 1);
        }
        */
    }

    void SpawnLineObj(Vector2 spawnPosition)
    {
        GameObject newlineObj = ObjectPool.SharedInstance.GetPooledObject();
        if (newlineObj != null)
        {
            newlineObj.transform.localPosition = spawnPosition;
            newlineObj.transform.parent = gameObject.transform;
            newlineObj.SetActive(true);
        }
    }
}
