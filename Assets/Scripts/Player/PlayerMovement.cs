using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    // Physics
    private Rigidbody2D rb;

    // Speed
    [SerializeField] float upForce = 14f;
    [SerializeField] float playerSpeed = 6f;

    // Player tilt
    [SerializeField] float tiltSpeed;
    Vector3 currentEulerAngles;

    // Explosion
    Collider2D[] inExplosionRadius = null;
    [SerializeField] private float explosionForceMulti = 5;
    [SerializeField] private float explosionRadius = 5;

    // Sound
    private bool movingUpwards = false;

    // Level
    public float nextLevelPos = 100;
    LineManager lineManager;

    void Awake()
    {
        lineManager = FindObjectOfType<LineManager>();

        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;

        GameState.PlayerState = PlayerStates.WaitingToStart;
        GameState.OnDeadState += HandleDeadState;
        LevelManager.OnLevelParamChanged += UpdateSpeed;

        LevelTimer.OnLevelTimerComplete += GetNextLevelPos;
    }

    private void OnDestroy() 
    {
        GameState.OnDeadState -= HandleDeadState;
        LevelManager.OnLevelParamChanged -= UpdateSpeed;

        LevelTimer.OnLevelTimerComplete += GetNextLevelPos;
    }

    private void GetNextLevelPos()
    {
        nextLevelPos = lineManager.latestSpawnedLinePosition.x;
        Debug.Log("nextlevelPos : " + nextLevelPos);
    }

    private void UpdateSpeed(LevelParameters currentLevelParameters)
    {
        playerSpeed = currentLevelParameters.playerSpeed;
    }

    void MovedTroughNextLevel()
    {
        // todo - bool on levelmanager if nextlevel has occured.
        // if not, raise an startNextLevel event
        // let that event set bool to false again?
        // or let the LevelTimer do that
        //return (transform.position.x > nextLevelPos && !levelManager.eventOccurred);
    }


    void Update()
    {
        switch (GameState.PlayerState)
        {
            case PlayerStates.Playing:
                TiltPlayer();
                //if (MovedTroughNextLevel)
                break;
        }
    }

    void FixedUpdate()
    {
        switch (GameState.PlayerState)
        {
            case PlayerStates.WaitingToStart:
                MovePlayerRight(playerSpeed / 3);
                if (TouchInput())
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    GameState.PlayerState = PlayerStates.Playing;
                    GameState.TriggerPlayStateEvent();
                }
                break;

            case PlayerStates.Playing:
                PlayHelicopterSound();
                MovePlayerRight(playerSpeed);
                if (TouchInput())
                {
                    MovePlayerUp();
                }
                break;
        }
    }

    void Start() {
        Application.targetFrameRate = 60;

        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            playerSpeed = levelManager.getCurrentLevelParameters().playerSpeed;
        }
        
    }

    private void HandleDeadState() 
    {
        //Explode();
        // Invoke("LoadNewGameScene", (float)2); moved to exoplodeonclick
    }

    private void MovePlayerRight(float speed)
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0);
    }

    private void PlayHelicopterSound()
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

    private void MovePlayerUp()
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


    private void TiltPlayer()
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (GameState.PlayerState == PlayerStates.Playing)
        {
            GameState.TriggerDeadStateEvent();
            GameState.PlayerState = PlayerStates.Dead;
        }
    }

    void LoadNewGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    void Explode()
    {
        inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D o in inExplosionRadius)
        {
            Rigidbody2D o_rigidbody = o.GetComponent<Rigidbody2D>();
            if (o_rigidbody != null)
            {
                Vector2 distanceVector = o.transform.position - transform.position;
                if (distanceVector.magnitude > 0)
                {
                    float explosionForce = explosionForceMulti / distanceVector.magnitude;
                    o_rigidbody.AddForce(distanceVector.normalized * explosionForce);
                }
            }
        }
    }

}
