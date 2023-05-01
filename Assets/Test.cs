using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MyCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MyCoroutine()
    {
        Debug.Log("0");
        // Code that executes immediately
        yield return new WaitForSeconds(1.0f); // Pause for one frame

        Debug.Log("1");
        // More code that executes after one frame
        yield return new WaitForSeconds(2.0f); // Pause for one second
        // More code that executes after one second

        Debug.Log("3");
    }
}
