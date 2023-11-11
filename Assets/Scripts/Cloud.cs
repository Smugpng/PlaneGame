using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float cloudSpeed;
    private GameObject gM;

    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.Find("GameManager");
        cloudSpeed = Random.Range(2f, 6f);
        float tempValue = Random.Range(0.1f, 1f);
        transform.localScale = new Vector3(1, 1, 1) * tempValue;
        tempValue = Random.Range(0.1f, 0.3f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, tempValue);
    }

    // Update is called once per frame
    void Update()
    {
        int shoudIMove = gM.GetComponent<GameManager>().cloudsMove;
        transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * cloudSpeed * shoudIMove);
        if (transform.position.y < -11f)
        {
            transform.position = new Vector3(Random.Range(-11f, 11f), 7.5f, 0);
        }
    }
}