using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    SpriteRenderer Renderer;
    
    void Start()
    {
        //Fetch the SpriteRenderer from the GameObject
        Renderer = GetComponent<SpriteRenderer>();

        //Controller.GetInstance().GameModeEvent += ChangeColor;
        //InvokeRepeating("ChangeToRandomColor", 0.1f, 0.1f);
    }

    void OnEnable()
    {
        //Controller.OnClicked += ChangeToRandomColor;
    }


    void OnDisable()
    {
        //Controller.OnClicked -= ChangeToRandomColor;
    }

    
    void ChangeToRandomColor()
    {
        bool colorBlack = true;
        if (colorBlack)
        {
            Renderer.color = new Color(171, 79, 0, 0); //Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            colorBlack = false;
        }
        else
        {
            Renderer.color = Color.black;
            colorBlack = true;
        }
        
    }
    
}
