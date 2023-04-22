using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthSO health;

    void Awake()
    {
        GameState.OnDeadState += HandleDeadState;
    }

    void Start()
    {
        
    }

    void OnDestroy() {
        GameState.OnDeadState -= HandleDeadState;
    }

    private void HandleDeadState()
    {
        health.TakeDamage(1);
    }
}
