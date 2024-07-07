using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public HealthSO health;
    public Image[] hearts;

    void Awake()
    {
        GameState.OnDeadState += UpdateHealthUI;
        UpdateHealthUI();
    }

    private void OnDestroy() 
    {
        GameState.OnDeadState -= UpdateHealthUI;
    }

    private void OnEnable()
    {
        GameState.OnPlayState += ActivateAnimOnCurrentHeart;
        GameState.OnWaitingToStart += DisableAllAnimOnHearts;
        GameState.OnDeadState += DisableAllAnimOnHearts;
    }

    private void OnDisable()
    {
        GameState.OnPlayState -= ActivateAnimOnCurrentHeart;
        GameState.OnWaitingToStart -= DisableAllAnimOnHearts;
        GameState.OnDeadState -= DisableAllAnimOnHearts;
    }

    /*
    private void UpdateHealthUI() 
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health.currentHealth)
            {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }
    */

    private void DisableAllAnimOnHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            // Enable or disable the animator component for each heart
            Animator animator = hearts[i].GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = false;
            }

        }
    }

    private void ActivateAnimOnCurrentHeart()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            // Enable or disable the animator component for each heart
            Animator animator = hearts[i].GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = (i == health.currentHealth - 1);
            }
            
        }
    }

    private void UpdateHealthUI() 
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            // Enable or disable the animator component for each heart
            Animator animator = hearts[i].GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = (i == health.currentHealth - 1);
            }

            if (i < health.currentHealth)
            {
                hearts[i].enabled = true;
            } 
            else 
            {
                hearts[i].enabled = false;
            }

            
        }
    }
}

