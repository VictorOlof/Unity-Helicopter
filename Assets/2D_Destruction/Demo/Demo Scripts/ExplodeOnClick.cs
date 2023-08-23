using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Explodable))]
public class ExplodeOnClick : MonoBehaviour {

	private Explodable explodable;
	private ExplosionForce explosionForce;

	void Start()
	{
		explodable = GetComponent<Explodable>();
		explosionForce = GameObject.FindObjectOfType<ExplosionForce>();
	}

	void Awake()
    {
        GameState.OnDeadState += Explode;
    }

    private void OnDestroy() 
    {
        GameState.OnDeadState -= Explode;
    }

	void Explode()
	{
		explodable.explode();
		explosionForce.DoExplosion(transform.position);
	}

	
}
