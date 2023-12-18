using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Cat : Entity
{
    [SerializeField] private float speed = 3f; // �������� ��������
    [SerializeField] private int health;
    [SerializeField] private int lives;
    [SerializeField] private float jumpForce = 12f; // ���� ������
    private bool isGrounded = false;

    [SerializeField] private Image[] hearts;

 
    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject WinPanel;

    public Joystick joystick;
 


    private Rigidbody2D rb; // ������ �� ��������� RigidBody2D
    private Animator anim; // ������ �� ��������� Animator
    private SpriteRenderer sprite; // ������ �� ��������� ScriotRenderer

    public States State // ����� ��� ��������
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    public override void GetDamage() // �����, ���������� �� ��������� �����
    {
        health -= 1;
        Debug.Log(health);
        if (health == 0)
        {
            foreach (var h in hearts)
                h.sprite = deadHeart;
            Die();  
        }

            
    }

    public override void Win()
    {
        Invoke("SetWinPanel", 1.1f);
    }

    private void SetWinPanel()
    {
        WinPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public override void Die()
    {
        State = States.death;
        Invoke("SetLosePanel", 1.1f);
    }

    
    private void SetLosePanel()
    {
        LosePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public static Cat Instance { get; internal set; }

    private void Awake() // � ������ Awake �� �������� ���������� RigidBody2D, Animator, SpriteRenderer
    {
        lives = 9;
        health = lives;
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate() 
    {
        CheckGround();
    }

    private void Update() // ����� Update ������ ��� �������� ������� ������ ���� � ������, � ����� ��� ������ ������ ������ �� ������ ��� ��������� �����
    {
        if (isGrounded && health > 0) State = States.idle; 

        if (joystick.Horizontal != 0 && health > 0)
            Run();
        

        if (health > lives)
            health = lives;


        for(int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = aliveHeart;
            else
                hearts[i].sprite = deadHeart;
            if (i < lives)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;

        }
    }

    private void Run() // ����� Run �������� �� ������������ �� �����������
    {
        if (isGrounded) State = States.run;

        Vector3 dir = transform.right * joystick.Horizontal;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    public void Jump() // ����� Jump �������� �� ������
    {
        if (isGrounded)
            rb.velocity = Vector2.up * jumpForce;   
    }

    private void CheckGround() // ����� CheckGround ������ ��� ����������� ������, �� �������� ������ ������, ���� ������ �� �������� �����
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;

        if (!isGrounded && health > 0) State  = States.jump;
    }

 
    
}

public enum States // �������� ������� ��� ��������
{
    idle, // ��� ��������
    run, // ���
    jump, // ������
    death
}
