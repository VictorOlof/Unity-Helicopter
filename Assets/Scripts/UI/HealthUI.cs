using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; // Add this line

public class HealthUI : MonoBehaviour
{
    public HealthSO health;
    public Image[] hearts;


    private void OnEnable()
    {
        GameState.OnWaitingToStart += UpdateHealthUI;
        //GameState.OnPlayState += ActivateAnimOnCurrentHeart;
        GameState.OnWaitingToStart += ActivateAnimOnCurrentHeart;
        GameState.OnDeadState += DisableAllAnimOnHearts;
        GameState.OnDeadState += DisableCurrentHeart;
    }

    private void OnDisable()
    {
        GameState.OnWaitingToStart -= UpdateHealthUI;
        //GameState.OnPlayState -= ActivateAnimOnCurrentHeart;
        GameState.OnWaitingToStart -= ActivateAnimOnCurrentHeart;
        GameState.OnDeadState -= DisableAllAnimOnHearts;
        GameState.OnDeadState -= DisableCurrentHeart;
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

    private void DisableCurrentHeart()
    {
        hearts[hearts.Length - 1].enabled = false;
        RemoveLastHeart();
    }

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

    public void RemoveLastHeart()
    {
        if (hearts.Length == 0)
        {
            Debug.LogWarning("Hearts array is already empty.");
            return;
        }

        // Convert the array to a list
        List<Image> heartsList = new List<Image>(hearts);

        // Remove the last element
        heartsList.RemoveAt(heartsList.Count - 1);

        // Convert the list back to an array
        hearts = heartsList.ToArray();
    }
}

