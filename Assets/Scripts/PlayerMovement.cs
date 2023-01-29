using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float upForce = 14f;
    [SerializeField] float speed = 14f;
    [SerializeField] float tiltSpeed;

    [SerializeField] private Slider speedSlider;
    [SerializeField] private TextMeshProUGUI speedTxt;

    [SerializeField] private Slider upForceSlider;
    [SerializeField] private TextMeshProUGUI upForceTxt;

    Vector3 currentEulerAngles;

    private Rigidbody2D rb;

    private State state;
    private enum State
    {
        WaitingToStart,
        Playing,
        Dead
    }

    private bool up = false;

    public float radius = 5.0F;
    public float power = 10.0F;

    

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
                

                if (TestInput())
                {
                    if (!up) 
                    {
                        SoundManager.Instance.PlaySoundUp();
                        Debug.Log("up");
                    }
                    up = true;
                }
                else 
                {
                    if (up) 
                    {
                        SoundManager.Instance.PlaySoundDown();
                        Debug.Log("down");
                    }
                    up = false;
                }

                

                break;
        }

        

        //Debug.Log(mySlider.value);
        //upForce = mySlider.value;
    }

    void Start() {
        speedSlider.onValueChanged.AddListener((v) => {
            speedTxt.text = v.ToString("0");
            speed = v;
        });

        upForceSlider.onValueChanged.AddListener((v) => {
            upForceTxt.text = v.ToString("0");
            upForce = v;
        });

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");

        
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
