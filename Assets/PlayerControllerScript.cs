using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour
{


    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;



    bool doubleJump = true;
    public BoxCollider2D Bcollider;
    public float speed;
    public float jumpHight;
    bool hit = false;
    bool hold = false;
    float hitStart = 0;
    public int Score = 0;
    private bool halt = false;
    [SerializeField] public Rigidbody2D body;
    
    [SerializeField] private LayerMask groundLayerMask;

    public float live = 3;
    [SerializeField]
    private GameObject[] lives;

    [SerializeField]
    public Text ScoreUi;

    public GameObject GameManager;
    Vector2 movement;

    
    void FixedUpdate()
    {
        
        if (!halt)
        {
            Score += 1;
            ScoreUi.text = "Score: " + Score.ToString();
        }

        if (hit)
        {
            Debug.Log("hitStart: " + hitStart);
            hitStart += Time.deltaTime;
        }

        if (hitStart >= 1.5 && hit)
        {
            hit = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (!halt)
        {
            body.velocity = new Vector2(speed, body.velocity.y);
        }

        if (Input.anyKeyDown && !hold)
        {
            //Debug.Log("Jump");
            body.velocity = new Vector2(body.velocity.x, jumpHight);
        }
        else
        {
            if (doubleJump && Input.anyKey && !hold)
            {
                //Debug.Log("Double Juimp");
                body.velocity = new Vector2(body.velocity.x, jumpHight);
                doubleJump = false;

            }

        }

        if (body.velocity.y < 0)
        {
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (body.velocity.y > 0 && !Input.anyKey)
        {
            body.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if(other.gameObject.tag == "Enemy" && !hit)
        {
            HitPlayer();
        }
    }

    public void HitPlayer()
    {
        Debug.Log("Hit");
        if (live - 1 >= 0)
        {
            live -= 1;
            lives[(int)live].SetActive(false);
            hit = true;

        }

        if (live == 0)
        {
            SceneManager.LoadScene("EndScreen");
        }
    }

    //Muss Trigger verwenden, damit man durch den Coin durch springen kann
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("TriggerEnter");
        if (other.gameObject.tag == "Collectable")
        {
            other.GetComponent<Collectable>().DeleteObject();
            //myObject.GetComponent<MyScript>().MyFunction();
        }
        if (other.gameObject.tag == "Interact")
        {
            other.GetComponent<InteractScript>().activateSlider();
        }
    }

    void OnDisable()
    {
        //PlayerPrefs.SetInt("score", playerScore);
        GameManager.GetComponent<GameManagerScript>().setScore(Score);
    }
    
    public void haltPlayer()
    {
        halt = true;
    }

    public void unhaltPlayer()
    {
        halt = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (Input.anyKey && other.gameObject.tag == "Interact")
        {
            hold = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (hold)
        {
            hold = false;
        }

    }
}
