using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] private GameObject currentLevelText;

    private void SpawnLvlTxt()
    {
        Debug.Log("TextManager: OnWaitingToStart->SpawnLvlTxt");
        SpawnLvlTxt(Vector2.zero);
    }
    
    private void SpawnLvlTxt(Vector2 latestSpawnedLinePosition)
    {
        Debug.Log("TextManager: SpawnLvlTxt: " + latestSpawnedLinePosition.x);

        GameObject gameObjectLvlTxt = Instantiate(currentLevelText, latestSpawnedLinePosition, Quaternion.identity);
    }

    void OnEnable()
    {
        //GameState.OnWaitingToStartStateEvent       += SpawnLvlTxt;
        LevelEvents.OnSetPlayerCKPT += SpawnLvlTxt;
    }

    void OnDisable()
    {
        //GameState.OnWaitingToStartStateEvent       -= SpawnLvlTxt;
        LevelEvents.OnSetPlayerCKPT -= SpawnLvlTxt;
    }

    /*
    void UpdatePosition(Vector2 latestSpawnedLinePosition)
    {
        float textWidth = rectTransform.rect.width;
        float offsetX = textWidth / 2f;
        Vector2 newPosition = transform.position;
        newPosition.x = latestSpawnedLinePosition.x;
        newPosition.y = latestSpawnedLinePosition.y;

        transform.position = newPosition;
    }
    */
}
