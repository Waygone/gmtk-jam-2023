using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    TextMeshPro text;
    Color textColor;

    const float DISAPPEAR_MAX_TIME = 0.5f;
    float disappearTimer;


    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        textColor = text.color;
        disappearTimer = DISAPPEAR_MAX_TIME;
    }

    private void Update()
    {
        float ySpeed = 1.5f;
        transform.position += new Vector3(0, ySpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;

        if (disappearTimer > DISAPPEAR_MAX_TIME * 0.5f)
        {
            transform.localScale += Vector3.one * 1.5f * Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.one * 1.5f * Time.deltaTime;
        }

        if (disappearTimer <= 0f)
        {
            textColor.a -= ySpeed * Time.deltaTime;
            text.color = textColor;
        }
    }

}
