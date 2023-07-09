using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{

    Garbage garbage;

    private void Awake()
    {
        garbage = GetComponentInParent<Garbage>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!garbage.isOnFloor)
            {
                collision.GetComponent<PlayerController>().Die();
            }
        }
    }
}
