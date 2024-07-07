using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    private SpriteRenderer renderer;
    private Coroutine randomColorCoroutine;
    private Color originalColor;
    private Color[] colors;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        LevelParameters levelParameters;
        if (GameState.PlayerState == PlayerStates.WaitingToStart || 
            LevelEvents.LatestEventType == LevelEvents.EventType.NewLevelEvent)
        {
            levelParameters = LevelManager.Instance.GetCurrentLevelParameters();
        }
        else //(LevelEvents.LatestEventType == LevelEvents.EventType.PrepNewLevelEvent)
        {
            levelParameters = LevelManager.Instance.GetNextLevelParameters();
        }

        originalColor = renderer.color;
        colors = new Color[] { levelParameters.squareColor1, levelParameters.squareColor2 };

        if (levelParameters.randomColor)
        {
            if (randomColorCoroutine != null)
            {
                StopCoroutine(randomColorCoroutine);
            }
            randomColorCoroutine = StartCoroutine(RandomColorCoroutine());
        }
        else
        {
            ChangeColor(levelParameters.squareColor);
        }
    }

    void ChangeColor(Color color)
    {
        renderer.color = color;
    }

    private void UpdateColorFromCurrentLevelParam(LevelParameters levelParameters)
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
        StartCoroutine(FadeToColorCoroutine(time, color));
    }

    IEnumerator FadeToColorCoroutine(float time, Color color)
    {
        float elapsedTime = 0;
        Color startingColor = renderer.color;
        while (elapsedTime < time)
        {
            renderer.color = Color.Lerp(startingColor, color, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        renderer.color = color;
    }

    IEnumerator RandomColorCoroutine()
    {
        while (true)
        {
            if (Random.value < 1f) // 50% chance
            {
                Color targetColor = colors[Random.Range(0, colors.Length)];
                yield return FadeToColorCoroutine(1.0f, targetColor); // Fade to random color in 1 second
                yield return new WaitForSeconds(1.0f);  // Hold the random color for 1 second
                yield return FadeToColorCoroutine(1.0f, originalColor); // Fade back to the original color in 1 second
                yield return new WaitForSeconds(1.0f);  // Hold the original color for 1 second
            }
            else
            {
                yield return new WaitForSeconds(1.0f); // Wait for 1 second before checking again
            }
        }
    }
}
