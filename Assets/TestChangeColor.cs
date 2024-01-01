using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChangeColor : MonoBehaviour
{
    SpriteRenderer renderer;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (renderer != null)
        {
            renderer.material.color = Color.blue;
            Debug.Log("-----Start method called. Color set to white.");
        }
        else
        {
            Debug.LogError("----SpriteRenderer component not found!");
        }
    }
}
