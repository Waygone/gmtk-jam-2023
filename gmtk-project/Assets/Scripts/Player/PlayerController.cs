using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator animator;
    SpriteRenderer spriteRenderer;
    public TextMeshProUGUI cleanedText;

    float cleaned = 0f;

    public float speed = 7.0f;
    public float cleanSpeed = 2.0f;
    public Transform limitMin, limitMax;


    public GameObject allObjectsInScene;
    public AudioSource levelMusic;
    public AudioSource headphonesMusic;


    [NonSerialized] public bool isCleaning = false;
    bool isMoving = false;

    bool canClean = false;
    bool startCleanTimer = false;
    float amountAddedToText = 0f;
    GameObject toClean;
    float cleanTimer;

    float vertical;
    float horizontal;

    String currentLevel;


    #region Callback
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        cleanTimer = cleanSpeed;
    }

    private void Start()
    {
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        currentLevel = PlayerPrefs.GetString("CurrentLevel");
        PlayerPrefs.SetFloat(currentLevel, cleaned);
    }


    private void Update()
    {
        GetInput();
        SetSprite();
        CleanTime();
    }

    private void FixedUpdate()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Cleaning"))
        {
            GetMovement();
        }
        else
        {
            rb.velocity = Vector3.zero;
            transform.position = transform.position;
        }
    }

    #endregion

    void SetSprite()
    {
        animator.SetBool("isCleaning", isCleaning);
        animator.SetBool("isMoving", isMoving);

        if(horizontal != 0)
            spriteRenderer.flipX = horizontal < 0;
    }

    void CleanTime()
    {
        if (startCleanTimer)
        {
            cleanTimer -= Time.deltaTime;
            if (cleanTimer <= 0.0f)
            {
                startCleanTimer = false;
                toClean.GetComponent<Garbage>().Clean();
                cleanTimer = cleanSpeed;
                
                cleaned += amountAddedToText;
                cleanedText.text = "Cleaned: x" + cleaned.ToString();

                PlayerPrefs.SetFloat(currentLevel,cleaned);

                CheckIfFinished();
            }
        }
    }


    private void CheckIfFinished()
    {
        if (allObjectsInScene.transform.childCount <= 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && canClean)
        {
            Debug.Log("cleaned");
            isCleaning = true;
            startCleanTimer = true;
            canClean = false;
            animator.SetBool("isCleaning", isCleaning);
        }
    }
    void GetMovement()
    {
        //Vector3 movement = new Vector3(horizontal * speed, vertical * speed, 0.0f);
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        //Set velocity to movement (input) * speed
        rb.velocity = new Vector2(horizontal * speed, vertical * speed / 1.2f);

        //Limit position to "borders"
        transform.position = new Vector3(Mathf.Clamp(
            transform.position.x, limitMin.position.x, limitMax.position.x),
            Mathf.Clamp(transform.position.y, limitMin.position.y, limitMax.position.y),
            transform.position.z);

        //Change animations depending on movement
        isMoving = horizontal != 0 || vertical != 0;
    }

    public void ChangeCanClean(bool c, GameObject tc, float amount)
    {
        canClean = c;
        toClean = tc;
        amountAddedToText = amount;
    }

    public void Die()
    {
        SceneManager.LoadScene("GameOver");
    }
}
