using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgtest : MonoBehaviour
{
    public float transitionDuration = 0.3f;

    private Camera mainCamera;
    private Color startColor, newStartColor, targetColor;
    private float t = 0f;

    void Start()
    {
        mainCamera = Camera.main;
        startColor = new Color(0.35f, 0.35f, 0.35f); //Color.white;
        targetColor = new Color(0.76f, 0.76f, 0.76f); //Color.black;
    }

    private void OnEnable()
    {
        LevelEvents.OnNewLevel += UpdateColorFromCurrentLevelParam;
    }

    private void Destroy()
    {
        LevelEvents.OnNewLevel -= UpdateColorFromCurrentLevelParam;
    }

    private void UpdateColorFromCurrentLevelParam(LevelParameters levelParameters)
    {
        t = 0f;
        newStartColor = levelParameters.bgColor1;
        targetColor = levelParameters.bgColor2;
    }

    void Update()
    {
        // Update the transition progress
        t += Time.deltaTime / transitionDuration;

        // Lerp between start and target colors
        mainCamera.backgroundColor = Color.Lerp(startColor, targetColor, Mathf.PingPong(t, transitionDuration));

        // Check if the transition is complete and swap start and target colors
        if (t >= transitionDuration)
        {
            t = 0f;
            SwapColors();
        }
    }

    void SwapColors()
    {
        Color temp = startColor;
        startColor = targetColor;
        targetColor = newStartColor;


        if (startColor == newStartColor)
        {
            startColor = targetColor;
            targetColor = newStartColor;
        }
        else
        {
            targetColor = newStartColor;
            targetColor = targetColor;
        }

        //temp = startColor;
        //startColor = targetColor;
        //targetColor = temp;
    }
}
