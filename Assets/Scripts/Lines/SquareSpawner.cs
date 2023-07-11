using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner : MonoBehaviour
{
    [SerializeField] GameObject square;
    [SerializeField] int squareLineHeight = 30;

    private enum SpawningDirection
    {
        Up,
        Down
    }

    [SerializeField] private SpawningDirection spawningDirection;

    void Start()
    {
        SpawnLineOfSquares();
    }

    void SpawnLineOfSquares()
    {
        switch(spawningDirection)
        {
            case SpawningDirection.Up:
                for (int y = 0; y < squareLineHeight; y++)
                {
                    spawnSquareObj(y);
                }
                break;

            case SpawningDirection.Down:
                for (int y = 0; y < squareLineHeight; y++)
                {
                    spawnSquareObj(y * -1);
                }
                break;
        }
    }

    void spawnSquareObj(int height)
    {
        GameObject newObj = Instantiate(square);
        newObj.transform.parent = gameObject.transform;
        newObj.transform.localPosition = new Vector2(0, height);
        newObj.transform.rotation = Quaternion.identity;
    }
}