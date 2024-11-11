using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyProjectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;


    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Projectile collision with " + other.gameObject);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
        }
    }

}
