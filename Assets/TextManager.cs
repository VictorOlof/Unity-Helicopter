using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] private GameObject currentLevelText;

    private void SpawnLvlTxt()
    {
        SpawnLvlTxt(Vector2.zero);
    }
    
    private void SpawnLvlTxt(Vector2 latestSpawnedLinePosition)
    {
        // Todo - better solution here for pos11?
        Vector3 newPos = new Vector3(latestSpawnedLinePosition.x, latestSpawnedLinePosition.y, 11);
        GameObject gameObjectLvlTxt = Instantiate(currentLevelText, newPos, Quaternion.identity);
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
