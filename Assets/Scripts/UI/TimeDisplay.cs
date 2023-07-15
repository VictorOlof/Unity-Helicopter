using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private float startTime;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {

        
        // Get the starting time
        startTime = Time.time;
    }

    private void Update()
    {
        // Calculate the elapsed time
        float elapsedTime = Time.time - startTime;
        textMeshPro.text = elapsedTime.ToString("F2");
    }
}