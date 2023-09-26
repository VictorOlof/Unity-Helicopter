using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextFade : MonoBehaviour
{
    public float fadeOutTime = 0.35f;
    public float displayTextDelay = 0;

    private TextMeshPro textMeshPro;
    private string text;

    void Awake()
    {
        GameState.OnPlayState += FadeOutText;
    }

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshPro>();

        text = textMeshPro.text;
        textMeshPro.text = "";
        Invoke("SetText", displayTextDelay);
    }

    private void SetText()
    {
        textMeshPro.text = text;
    }

    private void OnDestroy() 
    {
        GameState.OnPlayState -= FadeOutText;
    }

    
    private void FadeOutText() 
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f;
        Color initialColor = textMeshPro.color;

        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeOutTime);
            Color newColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1.0f - t);
            textMeshPro.color = newColor;
            yield return null;
        }

        Destroy(gameObject);
    }
}
