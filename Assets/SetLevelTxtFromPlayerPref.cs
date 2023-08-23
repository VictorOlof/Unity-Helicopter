using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SetLevelTxtFromPlayerPref : MonoBehaviour
{
    private TextMeshPro textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        
        LevelParameters levelParameters = LevelManager.Instance.GetCurrentLevelParameters();
        UpdateTxt(levelParameters);
    }


    void UpdateTxt(LevelParameters levelParameters)
    {
        textMeshPro.text = levelParameters.levelName;
    }
}
