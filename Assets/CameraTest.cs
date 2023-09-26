using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraTest : MonoBehaviour
{
    public Transform target; // The player's transform to follow
    public float smoothSpeed = 0.125f; // How quickly the camera should follow the player

    public Vector3 offset; // The initial offset between the camera and the player

    void Awake()
    {
        GameState.OnDeadState += ReloadScene;
    }

    private void OnDestroy() 
    {
        GameState.OnDeadState -= ReloadScene;
    }

    void LoadNewGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void ReloadScene()
    {
        Invoke("LoadNewGameScene", (float)2);
	}

    private void Start()
    {
        // Calculate the initial offset between the camera and the player
        // offset = transform.position - target.position;
    }

    private void Update()
    {
        if (target != null)
        {
            // Calculate the desired position for the camera
            Vector3 desiredPosition = target.position + offset;

            // Use Lerp to smoothly interpolate between the current camera position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Set the camera's position to the smoothed position
            transform.position = smoothedPosition;

            // Ensure the camera's Z position remains unchanged
            transform.position = new Vector3(transform.transform.position.x, transform.transform.position.y, -10f);
        }
        else
        {
            target = GameObject.Find("Player piece body").transform;
            if (target == null)
            {
                Debug.LogError("Could not find player piece body");
            }
            else
            {
                // Calculate the desired position for the camera
                Vector3 desiredPosition = target.position + offset;

                // Use Lerp to smoothly interpolate between the current camera position and the desired position
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, (smoothSpeed / 2));

                // Set the camera's position to the smoothed position
                transform.position = smoothedPosition;

                // Ensure the camera's Z position remains unchanged
                transform.position = new Vector3(transform.transform.position.x, transform.transform.position.y, -10f);
            }
            
        }
    }
}

