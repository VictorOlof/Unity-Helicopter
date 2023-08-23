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
    [SerializeField] public int maxHeightChange = 2;
    [SerializeField] GameObject lineParent;
    public int newRandomHeight;
    public int spawningHeight;

    public Vector2 latestSpawnedLinePosition;

    private void Awake()
    {
        LevelEvents.OnNewLevel += UpdateMaxHeightChange;
        LevelEvents.OnLevelTimerComplete  += SetPlayerCKPT;
    }
    private void OnDestroy() 
    {
        LevelEvents.OnNewLevel -= UpdateMaxHeightChange;
        LevelEvents.OnLevelTimerComplete  -= SetPlayerCKPT;
    }

    private void SetPlayerCKPT()
    {
        LevelEvents.InvokeOnSetPlayerCKPT(latestSpawnedLinePosition);
    }

    private void UpdateMaxHeightChange(LevelParameters levelParameters)
    {
        
        maxHeightChange = levelParameters.maxHeightChange;
        //ebug.Log("mmmm: ", maxHeightChange.ToString());
    }
    
    void Start()
    {
        SpawnStartLines();
    }

    void SpawnStartLines()
    {
        for (int x = -21; x < (tunnelWidth - 21); x++)
        {
            SpawnNewLine();
        }
    }

    public void SpawnNewLine()
    {
        switch (GameState.PlayerState)
        {
            case PlayerStates.WaitingToStart:
                spawningHeight = UnityEngine.Random.Range(0, maxHeightChange +1);
                break;

            case PlayerStates.Playing:
                newRandomHeight = UnityEngine.Random.Range((maxHeightChange / 2) * -1, (maxHeightChange / 2) + 1);
                spawningHeight += newRandomHeight;
                break;
        }

        latestSpawnedLinePosition = new Vector2(latestSpawnedLinePosition.x + 1, spawningHeight);
        SpawnLineObj(latestSpawnedLinePosition);
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
