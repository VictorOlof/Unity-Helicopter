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

        Controller.GetInstance().GameModeEvent += ChangeColor;
    }

    void ChangeColor(object sender, System.EventArgs e)
    {
        Renderer.color = new Color(0f, 0f, 0f, 1f);
    }
}
