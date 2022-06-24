using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float timer = 0;
    int speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((-transform.right * speed * Time.deltaTime));
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerControllerScript>().HitPlayer();
            Destroy(gameObject);
        }
        
    }
}
