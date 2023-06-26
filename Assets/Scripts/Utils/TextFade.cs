using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextFade : MonoBehaviour
{
    public float duration = 0.35f;

    private TextMeshPro textMeshPro;

    void Awake()
    {
        GameState.OnPlayState += FadeOutText;
    }

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        
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

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            Color newColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1.0f - t);
            textMeshPro.color = newColor;
            yield return null;
        }

        Destroy(gameObject);
    }
}
