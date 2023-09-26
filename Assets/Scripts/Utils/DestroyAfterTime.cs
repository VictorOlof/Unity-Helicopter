using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyTime = 5.0f; // The time after which the GameObject will be destroyed

    private float timer; // Timer to keep track of elapsed time

    private void Start()
    {
        // Start the timer when the GameObject is instantiated
        timer = 0.0f;
    }

    private void Update()
    {
        // Increment the timer by the time elapsed since the last frame
        timer += Time.deltaTime;

        // Check if the timer has exceeded the specified destroy time
        if (timer >= destroyTime)
        {
            // Destroy the GameObject
            Destroy(gameObject);
        }
    }
}