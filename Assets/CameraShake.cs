using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;
    public float shakeDuration = 0.15f;
    public float shakeIntensity = 0.4f;

    private Vector3 originalPosition;
    private float shakeTimer = 0f;
    private bool isShaking = false;

    void Start()
    {
        cameraTransform ??= transform;

        originalPosition = cameraTransform.localPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !isShaking)
        {
            Debug.Log("M key pressed."); // Add this line for testing
            StartCoroutine(ShakeCamera());
        }
    }

    IEnumerator ShakeCamera()
    {
        isShaking = true;
        shakeTimer = shakeDuration;

        while (shakeTimer > 0)
        {
            cameraTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeIntensity;

            shakeTimer -= Time.deltaTime;

            yield return null;
        }

        cameraTransform.localPosition = originalPosition;
        isShaking = false;
    }
}
