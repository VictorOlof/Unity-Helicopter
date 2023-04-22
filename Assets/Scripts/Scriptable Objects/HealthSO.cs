using UnityEngine;

[CreateAssetMenu(fileName = "New Health", menuName = "Health")]
public class HealthSO : ScriptableObject
{
    public int maxHealth = 3;
    public int currentHealth = 3;

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 3;
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}