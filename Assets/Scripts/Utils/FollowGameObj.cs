using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObj : MonoBehaviour
{
    //public static PlayerStates PlayerState = PlayerStates.WaitingToStart;
    public GameObject gameObjectToFollow;

    /*
    void Awake()
    {
        GameState.OnPlayState += FreezeText;
    }

    private void OnDestroy() 
    {
        GameState.OnPlayState -= FreezeText;
    }
    */
    
    void Update()
    {
        
        if (gameObjectToFollow != null)
        {
            // todo change Zpos? better solution?
            transform.position = new Vector3(gameObjectToFollow.transform.position.x, gameObjectToFollow.transform.position.y, 11);
            //transform.transform.SetParent(null);
        }
        
    }

    /*
    void FreezeText()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 11);
    }
    */
    
}
