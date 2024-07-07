using System.Collections;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour
{
    public float fadeOutTime = 0.35f;
    public float fadeInTime = 0.35f; // Duration for the fade-in effect
    public float displayTextDelay = 2.7f;

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
        StartCoroutine(FadeIn()); // Start the fade-in coroutine
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        Color initialColor = textMeshPro.color;
        textMeshPro.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0); // Set initial alpha to 0

        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeInTime);
            Color newColor = new Color(initialColor.r, initialColor.g, initialColor.b, t);
            textMeshPro.color = newColor;
            yield return null;
        }
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
