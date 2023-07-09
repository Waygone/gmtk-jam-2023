using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    GameObject enemy;

    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Object")
        {
            collision.GetComponent<Object>().mainCharacter = enemy;
        }
    }
}