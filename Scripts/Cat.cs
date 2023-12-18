using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Cat : Entity
{
    [SerializeField] private float speed = 3f; // скорость движени€
    [SerializeField] private int health;
    [SerializeField] private int lives;
    [SerializeField] private float jumpForce = 12f; // сила прыжка
    private bool isGrounded = false;

    [SerializeField] private Image[] hearts;

 
    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject WinPanel;

    public Joystick joystick;
 


    private Rigidbody2D rb; // ссылка на компонент RigidBody2D
    private Animator anim; // ссылка на компонент Animator
    private SpriteRenderer sprite; // ссылка на компонент ScriotRenderer

    public States State // метод дл€ анимации
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    public override void GetDamage() // метод, отвечающий за получение урона
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

    private void Awake() // в методе Awake мы получаем компоненты RigidBody2D, Animator, SpriteRenderer
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

    private void Update() // ћетод Update служит дл€ проверки нажати€ кнопок бега и прыжка, а также дл€ замены полных сердец на пустые при получении урона
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

    private void Run() // метод Run отвечает за передвижение по горизонтали
    {
        if (isGrounded) State = States.run;

        Vector3 dir = transform.right * joystick.Horizontal;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    public void Jump() // метод Jump отвечает за прыжок
    {
        if (isGrounded)
            rb.velocity = Vector2.up * jumpForce;   
    }

    private void CheckGround() // метод CheckGround служит дл€ ограничени€ прыжка, не позвол€€ делать прыжок, пока объект не каснетс€ земли
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;

        if (!isGrounded && health > 0) State  = States.jump;
    }

 
    
}

public enum States // основные позиции дл€ анимации
{
    idle, // без движени€
    run, // бег
    jump, // прыжок
    death
}
