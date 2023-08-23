using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLvlTxtPos : MonoBehaviour
{
    //public delegate void SetPlayerCKPT(Vector2 latestSpawnedLinePosition);
    private LevelEvents.SetPlayerCKPT updatePositionDelegate;

    void OnEnable()
    {
        updatePositionDelegate = UpdatePositionOnce;
        LevelEvents.OnSetPlayerCKPT += updatePositionDelegate;
    }

    void OnDisable()
    {
        if (updatePositionDelegate != null)
        {
            LevelEvents.OnSetPlayerCKPT -= updatePositionDelegate;
        }
    }

    void UpdatePositionOnce(Vector2 latestSpawnedLinePosition)
    {
        Debug.Log("NewLvlTxtPos: UpdatePosition!!!!!!!!!!!!!!");

        Vector2 newPosition = transform.position;
        newPosition.x = latestSpawnedLinePosition.x;
        newPosition.y = latestSpawnedLinePosition.y;

        transform.position = newPosition;

        // Unsubscribe after the event has been handled once
        LevelEvents.OnSetPlayerCKPT -= updatePositionDelegate;
        updatePositionDelegate = null;
    }
}
