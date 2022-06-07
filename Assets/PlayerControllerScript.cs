using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{
    
    
    public PolygonCollider2D Pcollider;
    public BoxCollider2D Bcollider;
    public float speed;
    public Vector3 movement;
    public float jumpHight;
    //gravity
    public float gravity; //-9.81f;
    
    [SerializeField] public Rigidbody2D body;
    
    [SerializeField] private LayerMask groundLayerMask;

    public float time = 0;
    private float live = 3;

    private bool isGround()
    {
        
        float extraHeightText = 1f;
        RaycastHit2D groundHit = Physics2D.BoxCast(Bcollider.bounds.center , Bcollider.bounds.size, 0f,Vector2.down,  .1f,  groundLayerMask);
        
        Color rayColor;

        if (groundHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        
        Debug.DrawRay(Bcollider.bounds.center + new Vector3(Bcollider.bounds.extents.x, 0, 0), Vector2.down * (Bcollider.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(Bcollider.bounds.center - new Vector3(Bcollider.bounds.extents.x, 0, 0), Vector2.down * (Bcollider.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(Bcollider.bounds.center - new Vector3(Bcollider.bounds.extents.x, Bcollider.bounds.extents.y, 0), Vector2.right * Bcollider.bounds.extents.x, rayColor);
        
        //Debug.Log(groundHit.collider);
        //Debug.Log(groundHit.distance);
        
        return (groundHit.collider != null && groundHit.distance >= 0.01f);
        

        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //movement = new Vector3(0,0,0);
        time = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        float hight = Mathf.Sqrt(jumpHight * gravity * Time.deltaTime);
        body.velocity = Vector2.right * speed;

        if (isGround() && Input.anyKey)
        {
            Debug.Log("Jump");
            body.velocity += (Vector2.up * jumpHight * speed) ;
            //body.velocity += (Vector2.right * speed);

        }
        //movement *= Time.deltaTime;
        //transform.Translate(movement);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //6 is Enemy layer
        if(other.gameObject.layer == 6)
        {
            Debug.Log("Hit");
            if(live - 1 >= 0)
            {
                live -= 1;
            }

            if (live == 0)
            {
                SceneManager.LoadScene("EndScreen");
            }
            
        }
    }
}
