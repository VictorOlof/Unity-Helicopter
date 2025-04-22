using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    public Image fadeImage; // Assign a full-screen black image
    public float fadeDuration = 0.9f;
    public float delayBeforeFade = 1f; // Set in inspector or by code


    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Start scene with a fade-in
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 1f;
            fadeImage.color = c;
            StartCoroutine(Fade(0.9f, 0f, fadeDuration));
        }
    }

    private void OnEnable()
    {
        GameState.OnDeadState += HandleDeadState;
    }

    private void OnDisable()
    {
        GameState.OnDeadState -= HandleDeadState;
    }

    private void HandleDeadState()
    {
        FadeToScene("GameScene");
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeAndSwitchScene(sceneName));
    }

    private IEnumerator FadeAndSwitchScene(string sceneName)
    {
        // Wait before starting the fade
        if (delayBeforeFade > 0f)
            yield return new WaitForSeconds(delayBeforeFade);

        // Fade to black
        yield return Fade(0f, 0.9f, fadeDuration);

        // Load the scene but don't activate it yet
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // Wait until the scene is ready (but not activated)
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        // Optionally wait a frame or two
        yield return new WaitForSeconds(0.1f);

        // Allow Unity to activate and render the scene
        asyncLoad.allowSceneActivation = true;

        // Wait for the scene to activate
        yield return null;

        // Fade from black
        yield return Fade(0.9f, 0f, fadeDuration);
    }



    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;
        Color color = fadeImage.color;

        while (time < duration)
        {
            float t = time / duration;
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            fadeImage.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;
    }
}
