using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject carPrefab;
    public GameObject polePrefab;
    public GameObject canPrefab;
    public GameObject glassPrefab;
    public GameObject buildingPrefab;
    public GameObject boxPrefab;


    GameObject player;
    float waitTime = 5.0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        waitTime = Random.Range(waitTime/2, waitTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Object")
        {

            var t = collision.gameObject.GetComponent<Object>().type.ToString();
            GameObject o = null;
            switch (t)
            {
                case "Car":
                    o = Instantiate(carPrefab, transform.position, Quaternion.identity);
                    break;

                case "Pole":
                    o = Instantiate(polePrefab, transform.position, Quaternion.identity);
                    break;

                case "Glass":
                    o = Instantiate(glassPrefab, transform.position, Quaternion.identity);
                    break;

                case "Can":
                    o = Instantiate(canPrefab, transform.position, Quaternion.identity);
                    break;

                case "Building":
                    o = Instantiate(buildingPrefab, transform.position, Quaternion.identity);
                    break;

                case "Box":
                    o = Instantiate(boxPrefab, transform.position, Quaternion.identity);
                    break;

                default: break;

            }
            Destroy(collision.gameObject);
            o.transform.parent = GameObject.Find("AllGarbage").transform;
            o.GetComponent<Garbage>().Launch(player, waitTime);
        }
    }
}
