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

    // Parameters set by Levelmanager
    [SerializeField] private int tunnelGapHeight = 20;
    [SerializeField] private int tunnelGapHeightRandomness = 0;
    
    // Movement
    [SerializeField] private int lineTopHeight, lineBottomHeight;
    private Vector2 initialTopLinePos, initialBottomLinePos;
    [SerializeField] private float currentLineTopHeight = 1;
    [SerializeField] private float currentLineBottomHeight = -1;
    [SerializeField] private bool linesMovementStarted;
    [SerializeField] private Vector3 topLineTargetPosition;
    public float moveDuration = 1.0f;

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
            UpdateParams(levelParameters);
            runningFirstTime = false;
        }
        
        CalcHeightParamsBasedOnTunnelGapHeight();

        switch (GameState.PlayerState)
        {
            case PlayerStates.WaitingToStart:
                SetTopBottomLinesHeight();

                linesMovementStarted = true;
                break;
        }

        // Events
        LevelEvents.OnPrepNewLevelEvent += UpdateParams;
    }

    void OnDisable() 
    {
        topLine.transform.localPosition    = new Vector2(topLine.transform.localPosition.x,    (float) 1);
        bottomLine.transform.localPosition = new Vector2(bottomLine.transform.localPosition.x, (float) -1);
        currentLineTopHeight    = 1;
        currentLineBottomHeight = -1;

        LevelEvents.OnPrepNewLevelEvent -= UpdateParams;

        linesMovementStarted = false;
    }
    
    void Start()
    {
        player = GameObject.Find("Player");

        if (topLine != null)
        {
            topLineTargetPosition = topLine.transform.position + new Vector3(0, lineTopHeight, 0);
        }
    }
    private void UpdateParams(LevelParameters levelParameters)
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

        switch (GameState.PlayerState)
        {
            case PlayerStates.WaitingToStart:
                /*if (IfPosLeftPlayer())
                {
                    StartCoroutine(MoveObjectCoroutine(topLine, (lineTopHeight * -1), 0.5f));
                    StartCoroutine(MoveObjectCoroutine(bottomLine, (lineBottomHeight * -1), 0.5f));
                }
                */
                break;

            case PlayerStates.Playing:
                if (!linesMovementStarted && !IfPosOutsideCameraRight())
                {
                    StartCoroutine(MoveObjectCoroutine(topLine, lineTopHeight, moveDuration));
                    StartCoroutine(MoveObjectCoroutine(bottomLine, lineBottomHeight, moveDuration));
                    linesMovementStarted = true;
                }

                /*if (IfPosLeftPlayer())
                {
                    StartCoroutine(MoveObjectCoroutine(topLine, 3, 0.5f));
                    StartCoroutine(MoveObjectCoroutine(bottomLine, -3, 0.5f));
                }*/

                break;
        }

        
    }

    void CalcHeightParamsBasedOnTunnelGapHeight()
    {
        float heightDiv  = (float)tunnelGapHeight / 2; 
        lineTopHeight    = Mathf.RoundToInt(heightDiv);
        lineBottomHeight = Mathf.RoundToInt(heightDiv) * -1;

        lineTopHeight    += UnityEngine.Random.Range(0, tunnelGapHeightRandomness +1);
        lineBottomHeight += (UnityEngine.Random.Range(0, tunnelGapHeightRandomness +1)) * -1;
    }

    void SetTopBottomLinesHeight()
    {
        initialTopLinePos    = new Vector2(topLine.transform.localPosition.x,    (float) lineTopHeight);
        initialBottomLinePos = new Vector2(bottomLine.transform.localPosition.x, (float) lineBottomHeight);

        topLine.transform.localPosition = initialTopLinePos;
        bottomLine.transform.localPosition = initialBottomLinePos;
    }

    bool IfPosLeftPlayer()
    {
        if (player != null)
        {
            return transform.position.x +2 < player.transform.position.x;
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

    private IEnumerator MoveObjectCoroutine(GameObject objectToMove, int yPosToMove, float movingTime = 2f)
    {
        Vector3 startPos = objectToMove.transform.position;
        Vector3 endPos = startPos + new Vector3(0.0f, yPosToMove, 0.0f);
        float elapsedTime = 0.0f;

        while (elapsedTime < movingTime)
        {
            objectToMove.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / movingTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        objectToMove.transform.position = endPos;
    }

}
