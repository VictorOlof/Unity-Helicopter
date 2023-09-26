using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles movement between the child objects bottomLine and topLine.
/// Continuously checks if gameObjects goes outside camera and calls Controller script to spawn new line.
/// </summary>
public class Line : MonoBehaviour
{
    // Object references
    private GameObject bottomLine, topLine;
    private GameObject player;
    private Camera cameraGameObject;
    LineManager lineManagerScript;

    // Parameters
    [SerializeField] private int tunnelGapHeight = 20;
    [SerializeField] private int tunnelGapHeightRandomness = 0;
    
    // Movement
    [SerializeField] private int lineTopHeight, lineBottomHeight;
    [SerializeField] private float currentLineTopHeight = 1;
    [SerializeField] private float currentLineBottomHeight = -1;
    [SerializeField] private bool hasEnteredCameraView;
    [SerializeField] private bool movementStarted;

    private bool runningFirstTime = true;

    void Awake()
    {
        cameraGameObject = Camera.main;
        topLine = gameObject.transform.GetChild(0).gameObject;
        bottomLine = gameObject.transform.GetChild(1).gameObject;

        // todo clean this code
        GameObject lineManagerGameObject = GameObject.Find("Line Manager");
        lineManagerScript = (LineManager)lineManagerGameObject.GetComponent(typeof(LineManager));
    }

    void OnEnable() 
    {
        if (runningFirstTime)
        {
            LevelParameters levelParameters = LevelManager.Instance.GetCurrentLevelParameters();
            UpdateTunnelGap(levelParameters);
            runningFirstTime = false;
        }
        
        CalcTopBottomLinesHeight();

        if (GameState.PlayerState == PlayerStates.WaitingToStart)
        {
            SetTopBottomLinesHeight();

            movementStarted = true;
        }

        // Events
        LevelEvents.OnPrepNewLevelEvent += UpdateTunnelGap;
    }

    void OnDisable() 
    {
        topLine.transform.localPosition    = new Vector2(topLine.transform.localPosition.x,    (float) 1);
        bottomLine.transform.localPosition = new Vector2(bottomLine.transform.localPosition.x, (float) -1);
        currentLineTopHeight    = 1;
        currentLineBottomHeight = -1;

        LevelEvents.OnPrepNewLevelEvent -= UpdateTunnelGap;

        CancelInvoke("MoveTopLine");
        CancelInvoke("MoveBottomLine");

        movementStarted = false;
        hasEnteredCameraView = false;
    }
    
    void Start()
    {
        player = GameObject.Find("Player");
    }
    private void UpdateTunnelGap(LevelParameters levelParameters)
    {
        tunnelGapHeight = levelParameters.tunnelGapHeight;
        tunnelGapHeightRandomness = levelParameters.tunnelGapHeightRandomness;
    }

    void Update() 
    {
        if (IfPosOutsideCameraLeft())
        {
            gameObject.SetActive(false);
            lineManagerScript.SpawnNewLine();
        }

        if (!IfPosOutsideCameraRight())
        {
            hasEnteredCameraView = true;
        }

        if (!movementStarted && hasEnteredCameraView && (GameState.PlayerState == PlayerStates.Playing))
        {
            InvokeRepeating("MoveTopLine", 0.025f, 0.025f);
            InvokeRepeating("MoveBottomLine", 0.025f, 0.025f);
            movementStarted = true;
        }
        
        if (IfPosLeftPlayer() && movementStarted)
        {
            InvokeRepeating("MoveTopLineReverse", 0.025f, 0.025f);
            InvokeRepeating("MoveBottomLineReverse", 0.025f, 0.025f);
            movementStarted = false;
        }
    }

    void CalcTopBottomLinesHeight()
    {
        float heightDiv  = (float)tunnelGapHeight / 2; 
        lineTopHeight    = Mathf.RoundToInt(heightDiv);
        lineBottomHeight = Mathf.RoundToInt(heightDiv) * -1;

        lineTopHeight    += UnityEngine.Random.Range(0, tunnelGapHeightRandomness +1);
        lineBottomHeight += (UnityEngine.Random.Range(0, tunnelGapHeightRandomness +1)) * -1;
    }

    void SetTopBottomLinesHeight()
    {
        topLine.transform.localPosition    = new Vector2(topLine.transform.localPosition.x,    (float) lineTopHeight);
        bottomLine.transform.localPosition = new Vector2(bottomLine.transform.localPosition.x, (float) lineBottomHeight);
    }

    bool IfPosLeftPlayer()
    {
        if (player != null)
        {
            return transform.position.x -1 < player.transform.position.x;
        }
        return false;
    }

    bool IfPosOutsideCameraRight()
    {
        Vector3 objectScreenPosition = cameraGameObject.WorldToScreenPoint(transform.position);
        return objectScreenPosition.x > Screen.width;
    }

    bool IfPosOutsideCameraLeft()
    {
        Vector3 linePosition = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        Vector3 objectScreenPosition = cameraGameObject.WorldToScreenPoint(linePosition);
        return objectScreenPosition.x < 0;
    }
    
    void MoveTopLine() {
        // Todo change movement method
        if (currentLineTopHeight < lineTopHeight)
        {
            currentLineTopHeight += (float) 0.05;
            topLine.transform.localPosition    = new Vector2(topLine.transform.localPosition.x,       (float) topLine.transform.localPosition.y + (float) 0.05);
        }
        else
        {
            CancelInvoke("MoveTopLine");
        }
    }

    void MoveBottomLine() {
        // Todo change movement method
        if (currentLineBottomHeight > lineBottomHeight)
        {
            currentLineBottomHeight -= (float) 0.05;
            bottomLine.transform.localPosition = new Vector2(bottomLine.transform.localPosition.x,    (float) bottomLine.transform.localPosition.y - (float) 0.05);
        }
        else
        {
            CancelInvoke("MoveBottomLine");
        }
    }

    void MoveTopLineReverse() {
        // Todo change movement method
        if (currentLineTopHeight > lineTopHeight)
        {
            currentLineTopHeight -= 0.05f; // Subtraction instead of addition
            topLine.transform.localPosition = new Vector2(topLine.transform.localPosition.x, topLine.transform.localPosition.y - 0.05f); // Subtract 0.05f
        }
        else
        {
            CancelInvoke("MoveTopLineReverse");
        }
    }

    void MoveBottomLineReverse() {
        // Todo change movement method
        if (currentLineBottomHeight < lineBottomHeight)
        {
            currentLineBottomHeight -= (float) 0.05;
            bottomLine.transform.localPosition = new Vector2(bottomLine.transform.localPosition.x,    (float) bottomLine.transform.localPosition.y + (float) 0.05);
        }
        else
        {
            CancelInvoke("MoveBottomLineReverse");
        }
    }
}
