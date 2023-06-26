using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextTypingAnim : MonoBehaviour
{
    public float delayBetweenCharacters = 0.1f;
    public float delayBeforeStartTyping = 0f;

    private TextMeshPro textMeshPro;
    string originalText;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        originalText = textMeshPro.text;
        textMeshPro.text = "";

        Invoke("StartTypeText", delayBeforeStartTyping);;
    }

    private void StartTypeText()
    {
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        for (int i = 0; i < originalText.Length; i++)
        {
            textMeshPro.text += originalText[i];
            yield return new WaitForSeconds(delayBetweenCharacters);
        }
    }
}
