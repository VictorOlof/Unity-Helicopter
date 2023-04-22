using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public HealthSO health;
    public TextMeshProUGUI healthText;

    private void Start()
    {
        UpdateHealthUI();
    }

    private void Update()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthText.text = "Health: " + health.currentHealth.ToString() + "/" + health.maxHealth.ToString();
    }
}