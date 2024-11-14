using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public InputAction MoveAction;

    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed = 3.0f;


    public int maxHealth = 5;
    int currentHealth;
    public int health { get { return currentHealth; } }


    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;


    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);


    public GameObject projectilePrefab;
    public InputAction launchAction;

    public InputAction talkAction;

    AudioSource audioSource;

    public AudioClip hitAudio;

    void Start()
    {
        MoveAction.Enable();
        launchAction.Enable();
        launchAction.performed += Launch;
        talkAction.Enable();
        talkAction.performed += FindFriend;

        audioSource = GetComponent<AudioSource>();

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();


        currentHealth = maxHealth - 1;

    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        if (!Mathf.Approximately(move.x, .0f) || !Mathf.Approximately(move.y, .0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

    }
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position + speed * Time.deltaTime * move;
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            damageCooldown = timeInvincible;
            animator.SetTrigger("Hit");
            audioSource.PlayOneShot(hitAudio);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        MyUIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);

    }

    void Launch(InputAction.CallbackContext context)
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * .5f, Quaternion.identity);
        MyProjectile projectile = projectileObject.GetComponent<MyProjectile>();
        projectile.Launch(moveDirection, 300);
        animator.SetTrigger("Launch");
    }

    void FindFriend(InputAction.CallbackContext context)
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * .2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {
            Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
            MyNonPlayerCharacter character = hit.collider.GetComponent<MyNonPlayerCharacter>();
            Debug.Log(character);
            if (character != null)
            {
                MyUIHandler.instance.DisplayDialogue();
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        Debug.Log(clip);
        audioSource.PlayOneShot(clip);
    }
}
