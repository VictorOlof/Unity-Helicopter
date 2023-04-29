using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthSO health;

    void Awake()
    {
        GameState.OnDeadState += LoseOneLife;
    }

    void OnDestroy() {
        GameState.OnDeadState -= LoseOneLife;
    }

    private void LoseOneLife()
    {
        health.TakeDamage(1);
    }
}
