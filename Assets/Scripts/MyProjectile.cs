using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyProjectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    AudioSource audioSource;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
        audioSource.Play();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log("Projectile collision with " + other.gameObject);
        EnemyController enemy = other.collider.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.Fix();
        }
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
