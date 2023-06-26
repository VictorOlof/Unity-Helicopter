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
}

