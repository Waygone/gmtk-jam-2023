using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Components
    Rigidbody2D rb;
    public Animator animator;
    SpriteRenderer spriteRenderer;
    
    public TextMeshProUGUI cleanedText;
    public TextMeshProUGUI timerText;

    public GameObject allObjectsInScene;
    public AudioSource levelMusic;
    public AudioSource headphonesMusic;
    #endregion

    #region Vars
    public float speed = 7.0f;
    public float cleanSpeed = 2.0f;
    public Transform limitMin, limitMax;

    float vertical;
    float horizontal;
    bool isMoving = false;
    #endregion

    #region Level Vars
    public int itemsToClean = 10;
    int itemsCleaned = 0;
    float levelTimer = 60f;
    bool timerRunning = false;
    float cleaned = 0f;
    [NonSerialized] public bool isCleaning = false;

    bool canClean = false;
    bool startCleanTimer = false;
    float amountAddedToText = 0f;
    GameObject toClean;
    float cleanTimer;
    #endregion

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
        PlayerPrefs.SetFloat(currentLevel+"i", 0);

        timerRunning = true;
        cleanedText.text = "Cleaned: 0/" + itemsToClean.ToString();
    }


    private void Update()
    {
        GetInput();
        SetSprite();
        CleanTime();
        RunTimer();
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

    #region Timers
    void RunTimer()
    {
        if (timerRunning)
        {
            levelTimer -= Time.deltaTime;
            timerText.text = TimeSpan.FromSeconds(levelTimer).ToString("ss\\.ff") + "s";
            if (levelTimer <= 0.0f)
                Die();
        }
    }


    public void ChangeCanClean(bool c, GameObject tc, float amount)
    {
        canClean = c;
        toClean = tc;
        amountAddedToText = amount;
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

                itemsCleaned += 1;
                
                cleaned += amountAddedToText;
                cleanedText.text = "Cleaned: " + itemsCleaned.ToString() + "/" + itemsToClean.ToString();

                PlayerPrefs.SetFloat(currentLevel,cleaned);
                PlayerPrefs.SetFloat(currentLevel+"i", itemsCleaned);

                CheckIfFinished();
            }
        }
    }


    private void CheckIfFinished()
    {
        if (allObjectsInScene.transform.childCount <= 0 || itemsCleaned >= itemsToClean)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }

    #endregion

    #region Handlers
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
    #endregion


    public void Die()
    {
        SceneManager.LoadScene("GameOver");
    }
}
