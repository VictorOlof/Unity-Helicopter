using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         // The player's transform to follow
    public float smoothSpeed = 0.125f; // How smoothly the camera follows the player
    public Vector3 offset;           // The offset from the player's position
    private GameObject targetObj;

    

    void Update()
    {
        if (target != null)
        {
            
            // Calculate the desired position for the cameraa
            Vector3 desiredPosition = target.position + offset;

            // Use Mathf.SmoothDamp to smoothly move the camera
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Set the camera's position to the smoothed position
            transform.position = smoothedPosition;

            // Ensure the camera's Z position remains unchanged
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
            
            // Calculate the desired position for the camera

            /*
            Vector3 desiredPosition = target.position + offset;

            // Smoothly move the camera using Vector3.SmoothDamp
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

            // Set the camera's position to the smoothed position
            transform.position = smoothedPosition;

            // Ensure the camera's Z position remains unchanged
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);

            // Smoothly adjust the camera's orthographic size
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetOrthoSize, smoothSpeed);
            */
        }
        else
        {
            targetObj = GameObject.Find("Player piece body");
            if (targetObj == null)
            {
                Debug.LogError("Could not find object with name ");
            }
            else
            {
                //transform.position = new Vector3(targetObj.transform.position.x + offset.x, targetObj.transform.position.y, transform.position.z);

                // Calculate the desired position for the camera
                Vector3 desiredPosition = new Vector3(transform.position.x, targetObj.transform.position.y + offset.y, -10f);

                // Use Mathf.SmoothDamp to smoothly move the camera
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, (smoothSpeed / 2));

                // Set the camera's position to the smoothed position
                transform.position = smoothedPosition;

                // Ensure the camera's Z position remains unchanged
                transform.position = new Vector3(transform.transform.position.x, transform.transform.position.y, -10f);
            }
            
        }
        
    }
}
