using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjInWaitingToStart : MonoBehaviour
{
    public static PlayerStates PlayerState = PlayerStates.WaitingToStart;
    public GameObject obj;

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
        
        if (GameState.PlayerState == PlayerStates.WaitingToStart && obj != null)
        {
            transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, 11);
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
