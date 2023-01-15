using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    [SerializeField] float upForce = 14f;
    [SerializeField] float speed = 14f;

    [SerializeField] float tiltSpeed;
    Vector3 currentEulerAngles;

    private Rigidbody2D rb;

    private State state;
    private enum State
    {
        WaitingToStart,
        Playing,
        Dead
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        state = State.WaitingToStart;
    }

    void Update()
    {
        switch (state)
        {
            default:
            case State.Playing:
                tiltSprite();
                smokeParticles();
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            default:
            case State.WaitingToStart:
                if (TestInput())
                {
                    state = State.Playing;
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }
                break;

            case State.Playing:
                if (TestInput())
                {
                    Jump();
                }
                // Move right continuously 
                transform.position += new Vector3(speed * Time.deltaTime, 0);
                break;

            case State.Dead:
                break;
        }
    }

    private void Jump()
    {
        //if (EventManager.PlayerPlaying != null) EventManager.PlayerPlaying();
        rb.AddForce(new Vector2(0, 1 * upForce) * Time.deltaTime);
    }

    private bool TestInput()
    {
        return
            Input.GetKey(KeyCode.Space) ||
            Input.GetMouseButtonUp(0) ||
            Input.touchCount > 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //gameOverScreen.Setup((int)transform.position.x);
    }

    private void tiltSprite()
    {
        if (TestInput() && currentEulerAngles.z <= 8)
        {
            currentEulerAngles += new Vector3(0, 0, 1) * Time.deltaTime * tiltSpeed;
        }
        else if (TestInput())
        {
            currentEulerAngles = new Vector3(0, 0, 8);
        }
        else if (!TestInput() && currentEulerAngles.z >= -6)
        {
            currentEulerAngles += new Vector3(0, 0, -1) * Time.deltaTime * tiltSpeed;
        }
        else
        {
            currentEulerAngles = new Vector3(0, 0, -6);
        }
        transform.eulerAngles = currentEulerAngles;
    }

    private void smokeParticles()
    {
        // TODO: Make less smoke if not isjumping
    }

    private void IncreaseSize()
    {
        transform.localScale = new Vector3(2, 2, 2);
    }

}
