using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float upForce = 14f;
    [SerializeField] float speed = 14f;
    [SerializeField] float tiltSpeed;

    // Explosions
    Collider2D[] inExplosionRadius = null;
    [SerializeField] private float ExplosionForceMulti = 5;
    [SerializeField] private float ExplosionRadius = 5;

    [SerializeField] private Slider speedSlider;
    [SerializeField] private TextMeshProUGUI speedTxt;

    [SerializeField] private Slider upForceSlider;
    [SerializeField] private TextMeshProUGUI upForceTxt;

    Vector3 currentEulerAngles;

    private Rigidbody2D rb;

    public State state;
    public enum State
    {
        WaitingToStart,
        Playing,
        Crashed,
        Dead
    }

    private bool movingUpwards = false;

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
            case State.Playing:
                TiltSprite();
                SmokeParticles();
                break;
        }
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case State.WaitingToStart:
                if (TouchInput())
                {
                    state = State.Playing;
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }
                break;

            case State.Playing:
                if (TouchInput())
                {
                    Jump();
                }
                PlayHelicopterSound();
                // Move right continuously 
                transform.position += new Vector3(speed * Time.deltaTime, 0);
                break;

            case State.Crashed:
                Explode();
                state = State.Dead;
                break;

            case State.Dead:
                Invoke("LoadNewGame", (float)2);
                break;
        }
    }

    void Start() {
        /*
        speedSlider.onValueChanged.AddListener((v) => {
            speedTxt.text = v.ToString("0");
            speed = v;
        });

        upForceSlider.onValueChanged.AddListener((v) => {
            upForceTxt.text = v.ToString("0");
            upForce = v;
        });
        */
    }

    void PlayHelicopterSound()
    {
        // Play sound
        if (TouchInput())
        {
            if (!movingUpwards) 
            {
                SoundManager.Instance.PlaySoundUp();
            }
            movingUpwards = true;
        }
        else 
        {
            if (movingUpwards) 
            {
                SoundManager.Instance.PlaySoundDown();
            }
            movingUpwards = false;
        }
    }

    private void Jump()
    {
        //if (EventManager.PlayerPlaying != null) EventManager.PlayerPlaying();
        rb.AddForce(new Vector2(0, 1 * upForce) * Time.deltaTime);
    }

    private bool TouchInput()
    {
        return
            Input.GetKey(KeyCode.Space) ||
            Input.GetMouseButtonUp(0) ||
            Input.touchCount > 0;
    }


    private void TiltSprite()
    {
        if (TouchInput() && currentEulerAngles.z <= 8)
        {
            currentEulerAngles += new Vector3(0, 0, 1) * Time.deltaTime * tiltSpeed;
        }
        else if (TouchInput())
        {
            currentEulerAngles = new Vector3(0, 0, 8);
        }
        else if (!TouchInput() && currentEulerAngles.z >= -6)
        {
            currentEulerAngles += new Vector3(0, 0, -1) * Time.deltaTime * tiltSpeed;
        }
        else
        {
            currentEulerAngles = new Vector3(0, 0, -6);
        }
        transform.eulerAngles = currentEulerAngles;
    }

    private void SmokeParticles()
    {
        // TODO: Make less smoke if not isjumping
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (state == State.Playing)
        {
            state = State.Crashed;
        }
    }

    void LoadNewGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    void Explode()
    {
        inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

        foreach (Collider2D o in inExplosionRadius)
        {
            Rigidbody2D o_rigidbody = o.GetComponent<Rigidbody2D>();
            if (o_rigidbody != null)
            {
                Vector2 distanceVector = o.transform.position - transform.position;
                if (distanceVector.magnitude > 0)
                {
                    float explosionForce = ExplosionForceMulti / distanceVector.magnitude;
                    o_rigidbody.AddForce(distanceVector.normalized * explosionForce);
                }
            }
        }
    }

}
