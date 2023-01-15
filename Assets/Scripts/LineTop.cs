using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTop : MonoBehaviour
{
    [SerializeField] GameObject stone;
    [SerializeField] int lineHeight = 27;

    void Start()
    {
        SpawnSquares();
    }

    void SpawnSquares()
    {
        // Spawn top of tunnel
        for (int y = 1; y < lineHeight; y++)
        {
            spawnGroundObj(stone, y);
        }
    }

    void spawnGroundObj(GameObject obj, int height)
    {
        GameObject newObj = Instantiate(obj);
        newObj.transform.parent = gameObject.transform;
        newObj.transform.localPosition = new Vector2(0, height);
        newObj.transform.rotation = Quaternion.identity;
    }
}