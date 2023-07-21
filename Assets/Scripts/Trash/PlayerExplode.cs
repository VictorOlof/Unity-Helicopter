using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerExplode : MonoBehaviour
{

    // Explosion
    Collider2D[] inExplosionRadius = null;
    [SerializeField] private float explosionForceMulti = 5;
    [SerializeField] private float explosionRadius = 5;

    void Explode()
    {
        inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D o in inExplosionRadius)
        {
            Rigidbody2D o_rigidbody = o.GetComponent<Rigidbody2D>();
            if (o_rigidbody != null)
            {
                Vector2 distanceVector = o.transform.position - transform.position;
                if (distanceVector.magnitude > 0)
                {
                    float explosionForce = explosionForceMulti / distanceVector.magnitude;
                    o_rigidbody.AddForce(distanceVector.normalized * explosionForce);
                }
            }
        }
    }
}