using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    BoxCollider2D bodyCollider;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;
    public bool isAlive = true;
    public bool isJumping;
    [SerializeField] float jumpSpeed = 7f;
    [SerializeField] Vector2 deathJump = new Vector2 (1f, .05f);
    [SerializeField] GameObject leftSpawner;
    [SerializeField] GameObject rightSpawner;
    [SerializeField] BackgroundScroller scrollingBG;
    [SerializeField] GameObject destroyer;
    [SerializeField] CursorMovement cursorMovement;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] UIAnimation uIAnimation;
    public AudioSource sfxJump;
    public AudioSource sfxDie;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<BoxCollider2D>();

        sr = GetComponent<SpriteRenderer>();
        sfxJump.volume = .5f;
        sfxDie.volume = .5f;
    }

    void Update()
    {
        if (!isAlive) {return;}
                ScoreUpdate();
    }
    void OnJump()
    {
        if (!isAlive) {return;}
            rb.velocity += new Vector2 (-jumpSpeed, 0f);
            animator.SetBool("isJumping", true);
            isJumping = true;
            sfxJump.Play();

    }
    
    void OnCollisionEnter2D(Collision2D other){
    
        if (other.gameObject.tag == "RightWallCollider")
        {
            transform.localScale = new Vector2 (-1, 1f);
            jumpSpeed = -jumpSpeed;
            animator.SetBool("isJumping", false);
            isJumping = false;

        }

        if (other.gameObject.tag == "LeftWallCollider")
        {
            transform.localScale = new Vector2 (1, 1f);
            jumpSpeed = -jumpSpeed;
            animator.SetBool("isJumping", false);
            isJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
         if (other.gameObject.tag == "Obstacle")
         {
            Destroy(other.gameObject);
            Die();
         }
    }

    void Die()
    {
        Time.timeScale = 1f;
        sfxDie.Play();
        Destroy(bodyCollider);
        Destroy(leftSpawner);
        Destroy(rightSpawner);
        Instantiate(destroyer, new Vector3 (0f, 3f), Quaternion.identity);
        scrollingBG.vertSpeed = 0;
        isAlive = false;

        rb.gravityScale = 3f;

        rb.velocity = deathJump;
    
        animator.SetBool("isDead", true);


        Invoke("LoadGOScene",3f);
    }

    void LoadGOScene() 
    {
        if (PlayerPrefs.GetInt("goodEnd", 0) == 2) {
            SceneManager.LoadScene("GameOver2");
        }
        else {
            SceneManager.LoadScene("GameOver");
        }
    }
    void ScoreUpdate () {
        if (Time.frameCount % 640 == 0) {
            Debug.Log(scoreManager.score.ToString());
            if ( uIAnimation.spritePerFrame > 70) {uIAnimation.spritePerFrame -= 20;}
            Time.timeScale += .2f;
            print ("hello");
        }
    }
}




