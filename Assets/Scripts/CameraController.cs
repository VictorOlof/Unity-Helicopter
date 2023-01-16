using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        // Follow player
        transform.position = new Vector3(player.position.x + 13, player.position.y, transform.position.z);
    }

    void Start()
    {
        //Controller.GetInstance().GameModeEvent += ChangeColor;
        //InvokeRepeating("ChangeToRandomColor", 0.5f, 0.55f);
    }

    void ChangeToRandomColor()
    {
        Camera.main.backgroundColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
