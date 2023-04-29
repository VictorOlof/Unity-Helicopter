using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBottom : MonoBehaviour
{
    [SerializeField] GameObject grass, dirt, stone;
    private int lineHeight = 27;

    void Start()
    {
        SpawnSquares();
    }

    void SpawnSquares()
    {
        // Spawn bottom of tunnel
        spawnGroundObj(grass, 0);
        int dirtHeight = Random.Range(3, 7);

        for (int y = 1; y < lineHeight; y++)
        {
            if (y < dirtHeight)
            {
                spawnGroundObj(dirt, y * -1);
            }
            else
            {
                spawnGroundObj(stone, y * -1);
            }
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