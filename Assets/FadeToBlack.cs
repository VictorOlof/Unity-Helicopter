using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeToBlack : MonoBehaviour
{
    public Image fadeImage; // Assign the black UI image in the inspector
    public float fadeDuration = 0.5f;
    public float waitForFadeTime = 1.5f;

    private void OnEnable()
    {
        GameState.OnDeadState += HandleDeadState;
    }

    private void OnDisable()
    {
        GameState.OnDeadState -= HandleDeadState;
    }

    void Awake()
    {
        
    }

    private void OnDestroy() 
    {
        
    }

    void LoadNewGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void HandleDeadState()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(waitForFadeTime); // Wait before starting fade

        float time = 0f;
        Color color = fadeImage.color;
        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            color.a = Mathf.Lerp(0f, 1f, t);
            fadeImage.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        // Ensure it's fully black at the end
        color.a = 1f;
        fadeImage.color = color;

        LoadNewGameScene();
    }

}
