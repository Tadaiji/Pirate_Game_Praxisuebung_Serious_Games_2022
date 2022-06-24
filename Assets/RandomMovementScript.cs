using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

public class RandomMovementScript: MonoBehaviour
{
    private bool moving = false;
    private float speed = 25f;
    private int turnes = 0;
    private bool dir = false;
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.AddComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        var rand = new System.Random();
        if (!moving)
        {
            turnes = rand.Next(10, 13);
            //turnes = 10;
            dir = !dir;
            //UnityEngine.Debug.Log("turnes: " + turnes);
            //UnityEngine.Debug.Log("Direction: " + dir);
            moving = true;
            StartCoroutine(movement());
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
}
