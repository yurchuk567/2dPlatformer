using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float jumpSpeed = 7;
  
     public float health = 100f;

    public float maxHealth = 100f;
    public GameObject VictoryText;

    private Rigidbody2D body;
    private Animator animator;
    private bool isLeftMovement;
    private bool isRunning;
    private bool isJumping;
    private bool isGameOvered;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = maxHealth;  
    }

    void Update()
    {
        var horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (horizontalMovement != 0)
        {
            isRunning = true;
            body.position += new Vector2(horizontalMovement * speed * Time.deltaTime, 0);
            if (horizontalMovement > 0 && isLeftMovement || horizontalMovement < 0 && !isLeftMovement)
            {
                var scale = transform.localScale;
                scale.x *= -1;

                transform.localScale = scale;
                isLeftMovement = !isLeftMovement;
            }
        }
        else
        {
            isRunning = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            isJumping = true;
            body.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }
        if (transform.position.y < -10)
        {
            GameOver();
        }
        // Оновлено SwitchAnimation для врахування isJumping
        SwitchAnimation();
    }

    private void SwitchAnimation()
    {
        if (isJumping)
        {
            if (body.velocity.y > 0)
            {
                animator.SetTrigger("ToUp");
            }
            else
            {
                animator.SetTrigger("ToDown");
            }
        }
        else if (isRunning)
        {
            animator.SetTrigger("ToRun");
        }
        else
        {
            animator.SetTrigger("ToIdle");
        }
    }

    private bool IsGrounded()
    {
        var raycast = Physics2D.Raycast(transform.position - transform.localScale / 2, Vector2.down, 0.1f);
        return raycast.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;

        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(10f);  // Вам слід визначити, скільки шкоди завдає ворог
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HealthBonus")
        {
            // Видаліть об'єкт бонусного здоров'я
            Destroy(collision.gameObject);

            // Додайте бонусне здоров'я
            SetHealth(20f);  // Ви можете встановити будь-яке значення бонусного здоров'я
        }
        else if (collision.gameObject.tag == "VictoryZone")
        {
            Victory();
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameOver();
        }
    }

    private void SetHealth(float bonusHealth)
    {
        health += bonusHealth;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void GameOver()
    {
        if (isGameOvered)
        {
            return;
        }

        isGameOvered = true;

        // Check if the object is active before starting the coroutine
        if (gameObject.activeSelf)
        {
            StartCoroutine(ReloadSceneAfterDelay(1f));
        }
    }

    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Victory()
    {
        VictoryText.SetActive(true);

        // Reload the scene after a delay
        StartCoroutine(ReloadSceneAfterDelay(1f));
    }

    private void OnDestroy()
    {
        GameOver();
    }
}
