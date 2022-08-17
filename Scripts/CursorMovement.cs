using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class CursorMovement : MonoBehaviour
{
    Vector2 moveInput;
    BoxCollider2D bodyCollider;
    public AudioSource sfxTheme;
    public AudioSource sfxError;
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] float runSpeed = 2f;
    public bool isCutscene = false;
    [SerializeField] DialogueController dialogueController;
    public bool goodEnd = false;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isCutscene) {
            sfxError.Play();
        }
        
        if (dialogueController.endCutscene) {
            Run();
        }
        if (isCutscene) return;
        Run();
    }

    void Run() 
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;            
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.tag == "Cutscene") {
            rb.velocity = new Vector2 (0f, 0f);
            isCutscene = true;
            sfxTheme.Stop();

        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Restart") {
            if (dialogueController.yes) {
                PlayerPrefs.SetInt("goodEnd", 1);
                SceneManager.LoadScene("BadRestart2");
            } else {
                PlayerPrefs.SetInt("goodEnd", 2);
                SceneManager.LoadScene("Start");
            }
        }
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}

