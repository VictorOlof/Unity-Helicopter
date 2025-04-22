using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeFromBlack : MonoBehaviour
{
    public Image fadeImage;           // Assign the black image in the inspector
    public float fadeDelay = 0f;      // Optional delay before fade starts
    public float fadeDuration = 1f;   // Duration of the fade

    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        if (fadeDelay > 0f)
            yield return new WaitForSeconds(fadeDelay);

        float time = 0f;
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            color.a = Mathf.Lerp(1f, 0f, t);
            fadeImage.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        // Fully transparent at the end
        color.a = 0f;
        fadeImage.color = color;

        // Optionally disable the image to allow clicks through
        fadeImage.gameObject.SetActive(false);
    }
}
