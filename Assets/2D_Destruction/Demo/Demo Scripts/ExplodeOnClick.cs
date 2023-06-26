using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Explodable))]
public class ExplodeOnClick : MonoBehaviour {

	private Explodable _explodable;
	private ExplosionForce ef;

	void Start()
	{
		_explodable = GetComponent<Explodable>();
		ef = GameObject.FindObjectOfType<ExplosionForce>();
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
		_explodable.explode();
		ef.doExplosion(transform.position);
	}

	
}
