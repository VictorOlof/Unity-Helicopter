using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void LateUpdate()
    {
        // Follow player
        transform.position = new Vector3(player.position.x + 13, player.position.y, transform.position.z);
        // Camera.main.transform.LookAt(target.transform); TODO?
    }

    void Start()
    {
        //Controller.GetInstance().GameModeEvent += ChangeColor;
        InvokeRepeating("ChangeToRandomColor", 0.5f, 0.25f);
        //InvokeRepeating("ChangeToRandomColor2", 0.5f, 3);
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
