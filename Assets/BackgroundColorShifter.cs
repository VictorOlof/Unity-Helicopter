using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorShifter : MonoBehaviour
{
    public float shiftTime = 1f;

    private Camera mainCamera;
    private Color currentColor;
    private Color targetColor;
    public float fadeDuration = 2f; // Duration of the fade in seconds
    private string bgColor;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();

        //LevelEvents.OnNewLevel += UpdateColorFromCurrentLevelParam;
    }

    private void Destroy()
    {
        //LevelEvents.OnNewLevel -= UpdateColorFromCurrentLevelParam;
    }


    void UpdateColorFromCurrentLevelParam(LevelParameters levelParameters)
    {
        //todo string color = levelParameters.bgColor;
        //ChangeColor(color);
    }

   

    private void Start()
    {
        bgColor = "black";
        SetBackgroundColor(GetColorFromString(bgColor));
    }

    public void SetBackgroundColor(Color targetBgColor)
    {
        // Start the fading coroutine
        StartCoroutine(FadeBackgroundColor(targetBgColor));
    }

    private IEnumerator FadeBackgroundColor(Color targetColor)
{
    while (true)
    {
        Color initialColor = mainCamera.backgroundColor;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            mainCamera.backgroundColor = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Wait for a short duration before starting the next fade
        yield return new WaitForSeconds(0.5f);

        // Alternate between the original color, a brighter version, and a darker version
        targetColor = BrightenColor(targetColor);
        yield return new WaitForSeconds(0.5f); // Wait before starting the next fade

        targetColor = DarkenColor(targetColor);
        yield return new WaitForSeconds(0.5f); // Wait before starting the next fade
    }
}


    private Color GetColorFromString(string colorName)
    {
        switch (colorName.ToLower())
        {
            case "black":
                return Color.black;
            case "blue":
                return Color.blue;
            // Add more cases for other colors as needed
            default:
                return Color.black; // Default to black if the input is not recognized
        }
    }

    private Color BrightenColor(Color color)
    {
        // Darken towards black
        return Color.Lerp(Color.white, Color.black, 0.8f);
    }


    private Color DarkenColor(Color color)
    {
        // Darken towards black
        return Color.Lerp(Color.white, Color.black, 0.95f);
    }




    private IEnumerator ShiftBackgroundColor()
    {
        float t = 0f;

        while (true)
        {
            mainCamera.backgroundColor = Color.Lerp(currentColor, targetColor, t);

            t += Time.deltaTime / shiftTime;

            if (t >= 1f)
            {
                t = 0f;
                currentColor = targetColor;
                targetColor = GetRandomColor();
            }

            yield return null;
        }
    }

    private Color GetRandomColor()
    {
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    /*
    void ChangeColor(string color)
    {
        switch (color)
        {
            case "black":
                GetComponent<Renderer>().color = Color.black;
                break;
            case "white":
                GetComponent<Renderer>().color = Color.white;
                break;
            case "red":
                GetComponent<Renderer>().color = Color.red;
                break;
            case "green":
                GetComponent<Renderer>().color = Color.green;
                break;
            case "blue":
                GetComponent<Renderer>().color = Color.blue;
                break;
            case "yellow":
                GetComponent<Renderer>().color = Color.yellow;
                break;
            case "cyan":
                GetComponent<Renderer>().color = Color.cyan;
                break;
            case "magenta":
                GetComponent<Renderer>().color = Color.magenta;
                break;
            case "orange":
                GetComponent<Renderer>().color = new Color(171, 79, 0, 0);
                break;
            case "purple":
                GetComponent<Renderer>().color = new Color(128, 0, 128, 0);
                break;
            case "pink":
                GetComponent<Renderer>().color = new Color(255, 192, 203, 0);
                break;
            case "brown":
                GetComponent<Renderer>().color = new Color(165, 42, 42, 0);
                break;
            case "grey":
                GetComponent<Renderer>().color = new Color(128, 128, 128, 0);
                break;
            case "transparent":
                GetComponent<Renderer>().color = new Color(0, 0, 0, 0);
                break;
            default:
                GetComponent<Renderer>().color = Color.black;
                break;
        }
    }
    */
}