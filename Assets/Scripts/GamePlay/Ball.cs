using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float initialSpeed;
    public float realTimeSpeed;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private BoolReference hitDeadRegion;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    private void Setup()
    {
        initialSpeed = realTimeSpeed;
    }

    public void Launch()
    {
        rigidbody.velocity = Vector3.up * realTimeSpeed;
    }

    private void OnCollisionEnter(Collision otherCollider)
    {
        Brick brick = otherCollider.gameObject.GetComponent<Brick>();

        if (brick != null)
            brick.TakeDamage();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dead"))
        {
            hitDeadRegion.Value = true;
        }
    }
}
