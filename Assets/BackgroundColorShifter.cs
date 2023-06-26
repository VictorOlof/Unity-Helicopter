using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorShifter : MonoBehaviour
{
    public float shiftTime = 1f;

    private Camera mainCamera;
    private Color currentColor;
    private Color targetColor;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        currentColor = mainCamera.backgroundColor;
        targetColor = GetRandomColor();
    }

    private void Start()
    {
        StartCoroutine(ShiftBackgroundColor());
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
}