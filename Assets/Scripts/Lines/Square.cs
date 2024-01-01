using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    SpriteRenderer renderer;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();

        LevelEvents.OnPlayerCKPT += UpdateColorFromCurrentLevelParam;
        GameState.OnDeadState += UpdateToBlackColor;
    }

    void OnEnable()
    {
        /*LevelParameters levelParameters = LevelManager.Instance.GetCurrentLevelParameters();

        string color = levelParameters.squareColor;
        ChangeColor(color);*/
    }

    private void OnDestroy() 
    {
        LevelEvents.OnPlayerCKPT -= UpdateColorFromCurrentLevelParam;
        GameState.OnDeadState -= UpdateToBlackColor;
    }

    private void UpdateColorFromCurrentLevelParam() //LevelParameters levelParameters)
    {
        LevelParameters levelParameters = LevelManager.Instance.GetCurrentLevelParameters();

        string color = levelParameters.squareColor;
        FadeToColor(0.5f, color);
    }

    private void UpdateToBlackColor()
    {
        FadeToColor(0.15f, "black");
    }

    void FadeToColor(float time, string color)
    {
        StartCoroutine(FadeToColorCoroutine(time, color));
    }

    //

    IEnumerator FadeToColorCoroutine(float time, string color)
    {
        float elapsedTime = 0;
        Color startingColor = renderer.color;
        Color endingColor = Color.black;

        switch (color)
        {
            case "black":
                endingColor = Color.black;
                break;
            case "white":
                endingColor = Color.white;
                break;
            case "red":
                endingColor = Color.red;
                break;
            case "green":
                endingColor = Color.green;
                break;
            case "blue":
                endingColor = Color.blue;
                break;
            case "yellow":
                endingColor = Color.yellow;
                break;
            case "cyan":
                endingColor = Color.cyan;
                break;
            case "magenta":
                endingColor = Color.magenta;
                break;
            case "orange":
                endingColor = new Color(171, 79, 0, 0);
                break;
            case "purple":
                endingColor = new Color(128, 0, 128, 0);
                break;
            case "pink":
                endingColor = new Color(255, 192, 203, 0);
                break;
            case "brown":
                endingColor = new Color(165, 42, 42, 0);
                break;
            case "grey":
                endingColor = new Color(128, 128, 128, 0);
                break;
            case "transparent":
                endingColor = new Color(0, 0, 0, 0);
                break;
            default:
                endingColor = Color.black;
                break;
        }

        while (elapsedTime < time)
        {
            renderer.color = Color.Lerp(startingColor, endingColor, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        renderer.color = endingColor;
    }

    void ChangeColor(string color)
    {
        switch (color)
        {
            case "black":
                renderer.color = Color.black;
                break;
            case "white":
                renderer.color = Color.white;
                break;
            case "red":
                renderer.color = Color.red;
                break;
            case "green":
                renderer.color = Color.green;
                break;
            case "blue":
                renderer.color = Color.blue;
                break;
            case "yellow":
                renderer.color = Color.yellow;
                break;
            case "cyan":
                renderer.color = Color.cyan;
                break;
            case "magenta":
                renderer.color = Color.magenta;
                break;
            case "orange":
                renderer.color = new Color(171, 79, 0, 0);
                break;
            case "purple":
                renderer.color = new Color(128, 0, 128, 0);
                break;
            case "pink":
                renderer.color = new Color(255, 192, 203, 0);
                break;
            case "brown":
                renderer.color = new Color(165, 42, 42, 0);
                break;
            case "grey":
                renderer.color = new Color(128, 128, 128, 0);
                break;
            case "transparent":
                renderer.color = new Color(0, 0, 0, 0);
                break;
            default:
                renderer.color = Color.black;
                break;
        }
    }


    /*
    [Range(0f, 1f)]
    public float fadeToGreenAmount = 0f;
    public float fadingSpeed = 0.05f;




    void Awake()
    {
        //renderer = GetComponent<SpriteRenderer>();

        //GameState.OnDeadState += HandleDeadState2;
    }

    
    private void OnDestroy() 
    {
        GameState.OnDeadState -= HandleDeadState2;
    }

    private void HandleDeadState2() 
    {
        renderer.color = Color.red;
    }
    


    void Start()
    {

        //Fetch the SpriteRenderer from the GameObject
        
        Color c = renderer.material.color;

        c.r = 1f;
        c.b = 1f;

        //renderer.material.color = c;

        //Controller.GetInstance().GameModeEvent += ChangeColor;
        InvokeRepeating("ChangeToRandomColor", 0.1f, 0.25f);
        
        //colorDisco = ChangeToRandomColor;

        //StartCoroutine("FadeToGreen");

    }
    
    IEnumerator FadeToGreen()
    {
        Debug.Log("1");
        for (float i = 1f; i >= fadeToGreenAmount; i -= 0.05f)
        {
            Debug.Log("2");

            Color c = renderer.material.color;
            c.r = i;
            c.b = i;

            renderer.material.color = c;

            yield return new WaitForSeconds(fadingSpeed);
        }
        Debug.Log("3");
    }
    

    void OnEnable()
    {
        //onGameOver.AddListener(ChangeToRandomColor);
        //Controller.OnClicked += ChangeToRandomColor;
        //PlayerMovement.OnGameOver += ChangeToRandomColor;
    }


    void OnDisable()
    {
        //PlayerMovement.OnGameOver -= ChangeToRandomColor;
    }
    
    
    void ChangeToRandomColor()
    {
        //renderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); 
        
        bool colorBlack = true;
        if (colorBlack)
        {
            renderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); 
            // orange or transparent new Color(171, 79, 0, 0); 
            colorBlack = false;
        }
        else
        {
            renderer.color = Color.black;
            colorBlack = true;
        }
        
        
    }
    */
    
    
}
