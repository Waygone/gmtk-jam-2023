using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Object : MonoBehaviour
{
    public AudioSource beam;
    bool isAttracted = false;

    public enum Objects
    {
        Car,
        Can,
        Pole,
        Glass,
        Building,
        Box
    };
    public Objects type;

    public float timeRange = 10.0f;
    public float speed = 5.0f;
    float timer = 0.0f;

    [NonSerialized] public GameObject mainCharacter;

    private void Start()
    {
        timer = timeRange;//UnityEngine.Random.Range(timeRange / 2, timeRange);
        mainCharacter = GameObject.FindGameObjectWithTag("MainCharacter");
    }

    private void Update()
    {
        if(timer <= 0.0f)
        {
            transform.position = Vector3.Lerp(transform.position,mainCharacter.transform.position, Time.deltaTime * speed);
            if(!isAttracted )
            {
                isAttracted=true;
                beam.Play();
            }
        }
        else
            timer -= Time.deltaTime;
    }
}
