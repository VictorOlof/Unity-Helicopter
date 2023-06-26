using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBottom : MonoBehaviour
{
    [SerializeField] GameObject square;
    [SerializeField] int lineHeight = 10;

    void Start()
    {
        SpawnLineOfSquares();
    }

    void SpawnLineOfSquares()
    {
        // Spawn top of tunnel
        /*for (int y = 1; y < lineHeight; y++)
        {
            spawnGroundObj(stone, y);
        }
        */
        
        spawnSquareObj(0);
        
    }

    void spawnSquareObj(int height)
    {
        GameObject newObj = Instantiate(square);
        newObj.transform.parent = gameObject.transform;
        newObj.transform.localPosition = new Vector2(0, height);
        newObj.transform.rotation = Quaternion.identity;
    }
}