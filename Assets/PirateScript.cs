using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

public class PirateScript : MonoBehaviour
{
    private bool moving = false;
    private float speed = 200f;
    private int turnes = 0;
    private bool dir = false;
    private Rigidbody2D body;
    float timer = 0;
    float shoot_timer = 0;
    bool move = false;

    public GameObject bulletPrefab;
    [SerializeField] bool shooting = false;
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.AddComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (move)
        {
            body.velocity = (Vector2.left * speed * Time.deltaTime);
            timer += Time.deltaTime;
            if (timer >= 5) {
                move = false;
                Destroy(gameObject);
            }

            shoot_timer += Time.deltaTime;
            if (shooting && shoot_timer >= 1) 
            {
                shoot_timer = 0;
                Instantiate(bulletPrefab, transform.position, transform.rotation,transform);
            }
        }

    }
    IEnumerator movement()
    {
        Vector2 direction;
        while (moving)
        {
            if (turnes != 0 && moving)
            {

                if (dir)
                {
                    direction = (Vector2.right * speed * Time.deltaTime);
                }
                else
                {
                    direction = (Vector2.left * speed * Time.deltaTime);
                }
                turnes -= 1;
                body.velocity += direction;
            }
            else if (turnes == 0 && moving)
            {
                yield return new WaitForSeconds(1);
                moving = false;

            }
        }
        yield return null;


    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            move = true;
        }

    }


}
