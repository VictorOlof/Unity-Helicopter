using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorFader : MonoBehaviour
{
    public Color startColor, endColor, oldStartColor;
    public float fadeSpeed = 1.0f;
    public float fadeTransitionTime = 5.0f;

    private float t = 0.0f;
    private bool onGoingTransition;

    private bool isReversing = false;

    private void OnEnable()
    {
        LevelEvents.OnNewLevel += UpdateColorFromCurrentLevelParam;
    }

    void Start()
    {
        LevelParameters levelParameters = LevelManager.Instance.GetCurrentLevelParameters();
        startColor = levelParameters.bgColor1;
        endColor = levelParameters.bgColor2;
    }

    private void Destroy()
    {
        LevelEvents.OnNewLevel -= UpdateColorFromCurrentLevelParam;
    }

    private void UpdateColorFromCurrentLevelParam(LevelParameters levelParameters)
    {
        startColor = levelParameters.bgColor1;
        endColor = levelParameters.bgColor2;
        fadeSpeed = levelParameters.bgColorFadeSpeed;
        //fadeTransitionTime = fadeSpeed;

        onGoingTransition = true;
        oldStartColor = Camera.main.backgroundColor;
        t = 0f;  // Reset the time variable
    }

    void Update()
    {
        if (onGoingTransition)
        {
            // Increment the time variable
            t += Time.deltaTime / fadeTransitionTime;

            // Use Mathf.Sin to create a smooth oscillation between -1 and 1
            float lerpFactor = Mathf.Sin(t * Mathf.PI * 0.5f);

            // Use Mathf.Lerp to interpolate between oldStartColor and endColor based on lerpFactor
            Camera.main.backgroundColor = Color.Lerp(oldStartColor, endColor, lerpFactor);

            if (t >= fadeTransitionTime)
            {
                onGoingTransition = false;
                Debug.Log("--");
            }
        }
        else
        {
            /*
            // Increment the time variable
            t += Time.deltaTime * fadeSpeed;

            // Use Mathf.Sin to create a smooth oscillation between -1 and 1
            float lerpFactor = Mathf.Sin(t * Mathf.PI * 0.5f);

            // Use Mathf.Lerp to interpolate between startColor and endColor based on lerpFactor
            Camera.main.backgroundColor = Color.Lerp(startColor, endColor, lerpFactor);
            */



            // Determine the direction of the lerp based on isReversing flag
            t += (isReversing ? -1 : 1) * Time.deltaTime / fadeSpeed;
            
            // When t reaches 1 or 0, reverse the direction
            if (t > 1.0f)
            {
                t = 1.0f;
                isReversing = !isReversing;
            }
            else if (t < 0.0f)
            {
                t = 0.0f;
                isReversing = !isReversing;
            }

            // Interpolate the background color between startColor and endColor
            Camera.main.backgroundColor = Color.Lerp(startColor, endColor, t);
        }
    }
}
