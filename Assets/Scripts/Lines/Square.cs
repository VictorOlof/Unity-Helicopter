using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    SpriteRenderer renderer;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        Color color;

        if (GameState.PlayerState == PlayerStates.WaitingToStart || 
            LevelEvents.LatestEventType == LevelEvents.EventType.NewLevelEvent)
        {
            LevelParameters levelParameters = LevelManager.Instance.GetCurrentLevelParameters();

            color = levelParameters.squareColor;
            ChangeColor(color);
        }
        else //(LevelEvents.LatestEventType == LevelEvents.EventType.PrepNewLevelEvent)
        {
            LevelParameters levelParameters = LevelManager.Instance.GetNextLevelParameters();

            color = levelParameters.squareColor;
            ChangeColor(color);
        }
    }

    private void UpdateColorFromCurrentLevelParam(LevelParameters levelParameters) //LevelParameters levelParameters)
    {
        LevelParameters levelParameters1 = LevelManager.Instance.GetCurrentLevelParameters();

        Color color = levelParameters1.squareColor;
        FadeToColor(0.5f, color);
    }

    private void UpdateToBlackColor()
    {
        FadeToColor(0.15f, Color.black);
    }

    void FadeToColor(float time, Color color)
    {
        Debug.Log("collr");
        StartCoroutine(FadeToColorCoroutine(time, color));
    }

    IEnumerator FadeToColorCoroutine(float time, Color color)
    {
        float elapsedTime = 0;
        Color startingColor = renderer.color;
        Color endingColor = Color.black;
        /*
        switch (color)
        {
            case "black":
                endingColor = Color.black;
                break;
            case "white":
                endingColor = Color.white;
                break;
            case "red":
                endingColor = Color.red;
                break;
            case "green":
                endingColor = Color.green;
                break;
            case "blue":
                endingColor = Color.blue;
                break;
            case "yellow":
                endingColor = Color.yellow;
                break;
            case "cyan":
                endingColor = Color.cyan;
                break;
            case "magenta":
                endingColor = Color.magenta;
                break;
            case "orange":
                endingColor = new Color(171, 79, 0, 0);
                break;
            case "purple":
                endingColor = new Color(128, 0, 128, 0);
                break;
            case "pink":
                endingColor = new Color(255, 192, 203, 0);
                break;
            case "brown":
                endingColor = new Color(165, 42, 42, 0);
                break;
            case "grey":
                endingColor = new Color(128, 128, 128, 0);
                break;
            case "transparent":
                endingColor = new Color(0, 0, 0, 0);
                break;
            default:
                endingColor = Color.black;
                break;
        }
        */

        while (elapsedTime < time)
        {
            renderer.color = Color.Lerp(startingColor, endingColor, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        renderer.color = endingColor;
    }

    void ChangeColor(Color color)
    {
        renderer.color = color;

        /*
        switch (color)
        {
            case "black":
                renderer.color = Color.black;
                break;
            case "white":
                renderer.color = Color.white;
                break;
            case "red":
                renderer.color = Color.red;
                break;
            case "green":
                renderer.color = Color.green;
                break;
            case "blue":
                renderer.color = Color.blue;
                break;
            case "yellow":
                renderer.color = Color.yellow;
                break;
            case "cyan":
                renderer.color = Color.cyan;
                break;
            case "magenta":
                renderer.color = Color.magenta;
                break;
            case "orange":
                renderer.color = new Color(171, 79, 0, 0);
                break;
            case "purple":
                renderer.color = new Color(128, 0, 128, 0);
                break;
            case "pink":
                renderer.color = new Color(255, 192, 203, 0);
                break;
            case "brown":
                renderer.color = new Color(165, 42, 42, 0);
                break;
            case "grey":
                renderer.color = new Color(128, 128, 128, 0);
                break;
            case "transparent":
                renderer.color = new Color(0, 0, 0, 0);
                break;
            default:
                renderer.color = Color.black;
                break;
        }
        */
    }
    
}
