using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObjStartLvlTxt : MonoBehaviour
{
    public GameObject gameObjectToFollow;
    public int offsetX, offsetY;
    public float disableFollowAfterSeconds = -1f; // Set to >0 to start countdown
    private bool isCountingDown = false;

    
    void Awake()
    {
        GameState.OnPlayState += SetgameObjectToFollowToNull;
    }

    private void OnDestroy() 
    {
        GameState.OnPlayState -= SetgameObjectToFollowToNull;
    }
    
    void Update()
    {
        if (gameObjectToFollow != null)
        {
            transform.position = new Vector3(gameObjectToFollow.transform.position.x + offsetX, gameObjectToFollow.transform.position.y + offsetY, 11);

            if (disableFollowAfterSeconds > 0)
            {
                disableFollowAfterSeconds -= Time.deltaTime;

                if (disableFollowAfterSeconds <= 0)
                {
                    SetgameObjectToFollowToNull();
                }
            }
        }
        
    }

    void SetgameObjectToFollowToNull()
    {
        gameObjectToFollow = null;
        disableFollowAfterSeconds = -1f; // Reset countdown
    }
    
}
