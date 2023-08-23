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

    // Sound
    private bool movingUpwards = false;

    // Level
    float lineCKPTXPos;
    bool lineCKPT;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;

        GameState.PlayerState = PlayerStates.WaitingToStart;

        LevelEvents.OnSetPlayerCKPT += UpdateCKPT;
        LevelEvents.OnNewLevel += UpdateSpeed;
    }

    private void OnDestroy() 
    {
        LevelEvents.OnSetPlayerCKPT -= UpdateCKPT;
        LevelEvents.OnNewLevel -= UpdateSpeed;
    }

    private void UpdateSpeed(LevelParameters levelParameters)
    {
        playerSpeed = levelParameters.playerSpeed;
    }

    private void UpdateCKPT(Vector2 latestSpawnedLinePosition)
    {
        lineCKPTXPos = latestSpawnedLinePosition.x;
        lineCKPT = true;
    }

    private bool MovedTroughCKPT()
    {
        return (transform.position.x > lineCKPTXPos && lineCKPT == true);
    }

    void Start() 
    {
        Application.targetFrameRate = 60;
        GameState.TriggerWaitingToStartStateEvent();

        LevelParameters levelParameters = LevelManager.Instance.GetCurrentLevelParameters();
        UpdateSpeed(levelParameters);
    }

    void Update()
    {
        switch (GameState.PlayerState)
        {
            case PlayerStates.Playing:
                TiltPlayer();
                if (MovedTroughCKPT())
                {
                    LevelEvents.InvokeOnPlayerCKPT();
                    lineCKPT = false;
                }
                break;
        }
    }

    void FixedUpdate()
    {
        switch (GameState.PlayerState)
        {
            case PlayerStates.WaitingToStart:
                MovePlayerRight(playerSpeed);
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
}