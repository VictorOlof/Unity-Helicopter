using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    SpriteRenderer renderer;

    [Range(0f, 1f)]
    public float fadeToGreenAmount = 0f;
    public float fadingSpeed = 0.05f;




    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();

        GameState.OnDeadState += HandleDeadState2;
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
        //InvokeRepeating("ChangeToRandomColor", 0.1f, 0.25f);

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
        renderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); 
        /*
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
        */
        
    }
    
}
