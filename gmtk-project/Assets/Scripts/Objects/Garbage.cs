using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    const float RANGE_AROUND_PLAYER = 0.0001f;

    public GameObject textPopupPrefab;
    public int amount = 100;
    public float speed = 0.8f;
    public AudioSource warningSound;
    public AudioSource crashingSound;

    Target indicator;
    GameObject bottomLimit;
    Vector3 playerPositionLaunch;
    bool canClean = false;

    [NonSerialized] public bool isOnFloor = false;

    Vector3 target = Vector3.zero;

    GameObject player;
    Collider2D collision;
    Rigidbody2D rb;
    ParticleSystem particles;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        collision = GetComponentInChildren<Collider2D>();
        collision.enabled = false;

        particles = GetComponentInChildren<ParticleSystem>();
        var p = particles.main;
        p.playOnAwake = false;

        indicator = GetComponent<Target>();
        bottomLimit = GameObject.FindGameObjectWithTag("LimitBottom");

        speed = UnityEngine.Random.Range(speed - (speed/2),speed);
        isOnFloor = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        player = GameObject.FindGameObjectWithTag("Player");

        amount = UnityEngine.Random.Range(amount-(amount/2),amount+(amount*2));
    }

    private void FixedUpdate()
    {
        if (transform.position.y < bottomLimit.transform.position.y || Vector2.Distance(transform.position, playerPositionLaunch) <= 0.2f)
        {
            indicator.targetColor = Color.green;
            particles.Play();
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            if (!isOnFloor)
            {
                isOnFloor = true;
                crashingSound.Play();
            }
        }
    }

    public void Launch(GameObject player)
    {
        isOnFloor = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        //"Shoot" to the player
        this.player = player;
        playerPositionLaunch = player.transform.position;

        target = player.transform.position - transform.position;
        //Debug.Log("Player: " + target);

        warningSound.pitch = UnityEngine.Random.Range(0.8f,1f);
        warningSound.Play();
        rb.AddForce(Vector2.Distance(player.transform.position,transform.position) > 5f ? (target*speed) : (target * speed*2f), ForceMode2D.Impulse);
        //transform.Translate(playerPositionLaunch * sp);
    }

    public void Clean()
    {
        player.GetComponent<PlayerController>().isCleaning = false;
        SetTextPopup(transform.position,amount);
        Destroy(this.gameObject);
    }

    private void SetTextPopup(Vector3 position, int damage)
    {
        var tp = Instantiate(textPopupPrefab, position, Quaternion.identity);
        tp.GetComponent<TextMeshPro>().text = damage.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canClean = true;
            collision.GetComponent<PlayerController>().ChangeCanClean(canClean, this.gameObject, amount);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canClean = false;
            collision.GetComponent<PlayerController>().ChangeCanClean(canClean, null, amount);
        }
    }
}
