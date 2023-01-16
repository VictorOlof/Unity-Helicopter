using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles heights and movements between the child objects bottomLine and topLine.
/// Continuously checks if gameObjects goes outside camera and calls Controller scripts.
/// </summary>
public class Line : MonoBehaviour
{
    private GameObject bottomLine, topLine;
    private int maxTunnelHeight = 12;
    private double currentTunnelHeight = 0;
    
    GameObject cameraGameObject;
    Controller controllerScript;

 
    void Awake()
    {
        cameraGameObject = GameObject.Find("Main Camera");
        bottomLine = gameObject.transform.GetChild(0).gameObject;
        topLine = gameObject.transform.GetChild(1).gameObject;

        // TODO init maxTunnelHeight etc. from GameController here
    }

    void OnEnable() 
    {
        Invoke("StartLineMovement", 1);
    }

    void OnDisable() 
    {
        CancelInvoke("MoveLines");
        bottomLine.transform.position = new Vector2(bottomLine.transform.position.x, 0);
        topLine.transform.position    = new Vector2(topLine.transform.position.x,    0);
        currentTunnelHeight = 0;
    }

    void Start()
    {
        GameObject controllerGameObject = GameObject.Find("Controller");
        controllerScript = (Controller)controllerGameObject.GetComponent(typeof(Controller));
    }

    void StartLineMovement()
    {
        InvokeRepeating("MoveLines", 0.025f, 0.025f);
    }

    void Update()
    {
        if (CheckIfOutside())
        {
            gameObject.SetActive(false);
            controllerScript.SpawnNewLine();
        }
    }

    bool CheckIfOutside()
    {
        // TODO: Check if outside camera to left TODO: instead of -25, subtract half camera size
        return (transform.position.x <= cameraGameObject.transform.position.x - 25);
    }

    void MoveLines() {
        if (currentTunnelHeight < maxTunnelHeight)
        {
            currentTunnelHeight += 0.1;
            
            topLine.transform.position    = new Vector2(topLine.transform.position.x,       (float) topLine.transform.position.y + (float) 0.05);
            bottomLine.transform.position = new Vector2(bottomLine.transform.position.x,    (float) bottomLine.transform.position.y - (float) 0.05);
        }
    }

    public void SetTunnelHeight(int height)
    {
        // Set tunnel height
        //topLine.transform.position = new Vector2(topLine.transform.position.x, bottomLine.transform.position.y + height);
        
        currentTunnelHeight = 0;
        maxTunnelHeight = height;
    }
}
