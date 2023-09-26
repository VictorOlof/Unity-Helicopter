using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnLvlTxt : MonoBehaviour
{
    private TextMeshPro textMeshPro;

    void Awake()
    {
        textMeshPro   = GetComponent<TextMeshPro>();
    }
    void OnEnable()
    {
        LevelEvents.OnPrepNewLevelEvent += UpdateTxt;
    }

    void OnDisable()
    {
        LevelEvents.OnPrepNewLevelEvent -= UpdateTxt;
    }

    void UpdateTxt(LevelParameters levelParameters)
    {
        textMeshPro.text = levelParameters.levelName;
        LevelEvents.OnPrepNewLevelEvent -= UpdateTxt;
    }
}
