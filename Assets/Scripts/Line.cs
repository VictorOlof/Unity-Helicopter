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
    private int tunnelGapHeight = 12;
    private double currentTunnelGapHeight = 0;
    public bool lineMovement = true;

    GameObject cameraGameObject;
    Controller controllerScript;
    [SerializeField] GameObject playerMovement;


    void Awake()
    {
        cameraGameObject = GameObject.Find("Main Camera");
        bottomLine = gameObject.transform.GetChild(0).gameObject;
        topLine = gameObject.transform.GetChild(1).gameObject;

        // TODO init tunnelGapHeight etc. from GameController here

        GameObject controllerGameObject = GameObject.Find("Controller");
        controllerScript = (Controller)controllerGameObject.GetComponent(typeof(Controller));
    }

    void OnEnable() 
    {
        // TODO - check for state in playermonement / event
        

        if (lineMovement)
        {
            InvokeRepeating("MoveLines", 0.025f, 0.025f);  
        }
        else
        {
            topLine.transform.position    = new Vector2(topLine.transform.position.x,       (float) topLine.transform.position.y + (float) 6.0);
            bottomLine.transform.position = new Vector2(bottomLine.transform.position.x,    (float) bottomLine.transform.position.y - (float) 6.0);
        }
    }

    void OnDisable() 
    {
        CancelInvoke("MoveLines");
        bottomLine.transform.position = new Vector2(bottomLine.transform.position.x, 0);
        topLine.transform.position    = new Vector2(topLine.transform.position.x,    0);
        currentTunnelGapHeight = 0;
    }

    void Update()
    {
        if (IfOutsideCameraLeft())
        {
            gameObject.SetActive(false);
            controllerScript.SpawnNewLine();
        }
    }

    bool IfOutsideCameraLeft()
    {
        // TODO: Check if outside camera to left TODO: instead of -25, subtract half camera size
        return (transform.position.x <= cameraGameObject.transform.position.x - 25);
    }

    void MoveLines() {
        if (currentTunnelGapHeight < tunnelGapHeight)
        {
            currentTunnelGapHeight += 0.1;
            
            topLine.transform.position    = new Vector2(topLine.transform.position.x,       (float) topLine.transform.position.y + (float) 0.05);
            bottomLine.transform.position = new Vector2(bottomLine.transform.position.x,    (float) bottomLine.transform.position.y - (float) 0.05);
        }
    }
}
