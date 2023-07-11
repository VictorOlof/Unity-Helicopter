using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles movement between the child objects bottomLine and topLine.
/// Continuously checks if gameObjects goes outside camera and calls Controller script to spawn new line.
/// </summary>
public class Line : MonoBehaviour
{
    private GameObject bottomLine, topLine;
    [SerializeField]
    public int tunnelGapHeight = 20;
    public int tunnelGapHeightRandomness = 0;
    
    public bool lineMovement = true;

    [SerializeField] private int lineTopHeight, lineBottomHeight;
    [SerializeField] private float currentLineTopHeight = 1;
    [SerializeField] private float currentLineBottomHeight = -1;

    GameObject cameraGameObject;
    LineManager lineManagerScript;

    public Vector2 topLineTarget;
    void Awake()
    {
        cameraGameObject = GameObject.Find("Main Camera");
        topLine = gameObject.transform.GetChild(0).gameObject;
        bottomLine = gameObject.transform.GetChild(1).gameObject;

        // todo clean this code
        GameObject lineManagerGameObject = GameObject.Find("Line Manager");
        lineManagerScript = (LineManager)lineManagerGameObject.GetComponent(typeof(LineManager));
    }

    void OnEnable() 
    {
        CalcTopBottomLinesHeight();

        // TODO - check for state in playermovement / event
        if (GameState.PlayerState == PlayerStates.WaitingToStart)
        {
            //tunnelGapHeight = 14;
            //InvokeRepeating("MoveLines", 0.025f, 0.025f);
            //topLine.transform.position    = new Vector2(topLine.transform.position.x,    (float) topLine.transform.position.y + (float) 3.0);
            //bottomLine.transform.position = new Vector2(bottomLine.transform.position.x, (float) bottomLine.transform.position.y - (float) 3.0);

            SetTopBottomLinesHeight();
        }
        else if (GameState.PlayerState == PlayerStates.Playing)
        {
            //SetTopBottomLinesHeight();
            
            InvokeRepeating("MoveTopLine", 0.025f, 0.025f);
            InvokeRepeating("MoveBottomLine", 0.025f, 0.025f);  
        }

        LevelManager.OnLevelParamChanged += UpdateParams;
    }

    void OnDisable() 
    {
        topLine.transform.localPosition    = new Vector2(topLine.transform.localPosition.x,    (float) 1);
        bottomLine.transform.localPosition = new Vector2(bottomLine.transform.localPosition.x, (float) -1);
        currentLineTopHeight = 1;
        currentLineBottomHeight = -1;

        LevelManager.OnLevelParamChanged -= UpdateParams;
    }

    private void UpdateParams(LevelParameters currentLevelParameters)
    {
        tunnelGapHeight = currentLevelParameters.tunnelGapHeight;
        tunnelGapHeightRandomness = currentLevelParameters.tunnelGapHeightRandomness;
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
            //Vector2 targetPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + lineTopHeight + 10);
            ///targetObject = new GameObject("Target");
            //targetObject.transform.position = targetPosition;

            //topLine.transform.localPosition = Vector3.Lerp(topLine.transform.localPosition, topLineTarget, 2 * Time.deltaTime);
            //Vector2.MoveTowards(topLine.transform.localPosition, topLineTarget.localPosition, 2 * Time.deltaTime);
        }
    }


    void CalcTopBottomLinesHeight()
    {
        float heightDiv  = (float)tunnelGapHeight / 2; 
        lineTopHeight    = Mathf.RoundToInt(heightDiv);
        lineBottomHeight = Mathf.RoundToInt(heightDiv) * -1;

        lineTopHeight    += UnityEngine.Random.Range(0, tunnelGapHeightRandomness +1);
        lineBottomHeight += (UnityEngine.Random.Range(0, tunnelGapHeightRandomness +1)) * -1;

        //topLineTarget = new Vector2(transform.localPosition.x, (transform.localPosition.y + lineTopHeight));
    }

    void SetTopBottomLinesHeight()
    {
        topLine.transform.localPosition    = new Vector2(topLine.transform.localPosition.x,    (float) lineTopHeight);
        bottomLine.transform.localPosition = new Vector2(bottomLine.transform.localPosition.x, (float) lineBottomHeight);
    }

    bool IfOutsideCameraLeft()
    {
        // TODO: Check if outside camera to left TODO: instead of -25, subtract half camera size
        return (transform.position.x <= cameraGameObject.transform.position.x - 25);
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
