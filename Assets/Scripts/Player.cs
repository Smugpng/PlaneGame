using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public float playerSpeed;
    private float horizontalScreenLimit = 10f;
    private float verticalScreenLimit = 4f;
    public int lives;
    public int maxLives;
    private bool _isShieldActive;
    [SerializeField]
    private GameObject _shieldVisual;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = 6f;
        lives = 3;
        maxLives = 3;
        _shieldVisual.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    void Movement()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * playerSpeed);
        if (transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1f, transform.position.y, 0);
        }
        if (transform.position.y < -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, -verticalScreenLimit, 0);
        }
        else if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
public void ShieldActive()
{
    _isShieldActive = true;
    _shieldVisual.SetActive(true);
}
    public void LoseLife()
    {
        if (_isShieldActive == true)
        {
            _shieldVisual.SetActive(false);
            _isShieldActive = false;
            return;
        }
        GameObject.Find("GameManager").GetComponent<GameManager>().LoseLives(1);
        lives--;
        Debug.Log(lives);
        //lives -= 1;
        //lives = lives - 1;
        if (lives <= 0)
        {
            //Game Over
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    public void AddLife()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().AddLives(1);
        //lives--;
        Debug.Log(lives);
        //lives -= 1;
        lives = lives + 1;
    }    
    private void OnTriggerEnter2D(Collider2D collision)
    {
             switch (collision.name)
             {
                    case "Barrier(Clone)": GameObject.Find("Player").GetComponent<Player>().ShieldActive();
                    Destroy(collision.gameObject);
                    break;
             }
        
    }      
}