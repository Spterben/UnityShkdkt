using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pchela : Entity
{
    private int lives = 1;
    private float speed = 1.5f;
    private Vector3 dir;
    private SpriteRenderer sprite;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject == Cat.Instance.gameObject)
        {
            Cat.Instance.GetDamage();
            lives--;
            Debug.Log("Σ οχελϋ " + lives);
        }

        if (lives < 1)
            Die();
    }
     private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        dir = transform.right;
    }
        
    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * dir.x * 0.7f, 0.1f);

        if (colliders.Length > 0) dir *= -1f;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }
    private void Update()
    {
        Move();
    }
}
