using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private GameObject targetObj;

    private void LateUpdate()
    {
        // Follow player
        if (player != null)
        {
            transform.position = new Vector3(player.position.x + 13, player.position.y, transform.position.z);
        }
        else
        {
            targetObj = GameObject.Find("Player piece body");
            if (targetObj == null)
            {
                Debug.LogError("Could not find object with name ");
            }
            else
            {
                transform.position = new Vector3(targetObj.transform.position.x + 13, targetObj.transform.position.y, transform.position.z);
            }
            
        }
        
        // Camera.main.transform.LookAt(target.transform); TODO?
    }

    void Start()
    {
        //Controller.GetInstance().GameModeEvent += ChangeColor;
        //InvokeRepeating("ChangeToRandomColor", 0.5f, 0.25f);
        //InvokeRepeating("ChangeToRandomColor2", 0.5f, 3);
    }

    void Awake()
    {
        GameState.OnDeadState += ReloadScene;
    }

    private void OnDestroy() 
    {
        GameState.OnDeadState -= ReloadScene;
    }

    void LoadNewGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void ReloadScene()
    {
        Invoke("LoadNewGameScene", (float)2);
	}

	

    void ChangeToRandomColor()
    {
        Camera.main.backgroundColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    /*
    void ChangeToRandomColor2()
    {
        //Camera.main.backgroundColor = 
        Camera.main.backgroundColor.DOColor
    }
    */
}
