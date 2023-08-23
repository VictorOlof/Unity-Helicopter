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
    private Camera cameraGameObject;
    LineManager lineManagerScript;

    // Parameters
    [SerializeField] private int tunnelGapHeight = 20;
    [SerializeField] private int tunnelGapHeightRandomness = 0;
    
    // Movement ...
    public bool lineMovement = true;
    [SerializeField] private int lineTopHeight, lineBottomHeight;
    [SerializeField] private float currentLineTopHeight = 1;
    [SerializeField] private float currentLineBottomHeight = -1;
    // new movements
    public float duration = 1f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float startTime;

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

    void Start()
    {
        
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
        }
        else if (GameState.PlayerState == PlayerStates.Playing)
        {
            //SetTopBottomLinesHeight();
            
            InvokeRepeating("MoveTopLine", 0.025f, 0.025f);
            InvokeRepeating("MoveBottomLine", 0.025f, 0.025f);  
        }

        LevelEvents.OnPrepNewLevelEvent += UpdateTunnelGap;
    }

    void OnDisable() 
    {
        topLine.transform.localPosition    = new Vector2(topLine.transform.localPosition.x,    (float) 1);
        bottomLine.transform.localPosition = new Vector2(bottomLine.transform.localPosition.x, (float) -1);
        currentLineTopHeight = 1;
        currentLineBottomHeight = -1;

        LevelEvents.OnPrepNewLevelEvent -= UpdateTunnelGap;
    }

    private void UpdateTunnelGap(LevelParameters levelParameters)
    {
        //denna metod kallas ej av LevelEvents.OnNewLevel 
        // Eftersom den skapas under körning !!!

        tunnelGapHeight = levelParameters.tunnelGapHeight;
        tunnelGapHeightRandomness = levelParameters.tunnelGapHeightRandomness;

        Debug.Log("Line: OnPrepNewLevelEvent, updating height: " + levelParameters.tunnelGapHeight);
    }

    void Update() 
    {
        if (IfOutsideCameraLeft())
        {
            gameObject.SetActive(false);
            lineManagerScript.SpawnNewLine();
        }

        if (GameState.PlayerState == PlayerStates.Playing)
        {
            //...
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

    bool IfOutsideCameraLeft()
    {
        // TODO: Check if outside camera to left TODO: instead of -25, subtract half camera size
        //return (transform.position.x <= cameraGameObject.transform.position.x - 25);
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
    }

    void MoveBottomLine() {
        // Todo change movement method
        if (currentLineBottomHeight > lineBottomHeight)
        {
            currentLineBottomHeight -= (float) 0.05;
            bottomLine.transform.localPosition = new Vector2(bottomLine.transform.localPosition.x,    (float) bottomLine.transform.localPosition.y - (float) 0.05);
        }
    }
    
}
