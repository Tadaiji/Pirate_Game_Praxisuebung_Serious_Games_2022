using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractScript : MonoBehaviour
{
    private bool hold = false;
    [SerializeField] public GameObject anim;
    Animator animation;
    float prograss = 0f;
    public Slider slider;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject obstical;

    // Start is called before the first frame update
    void Start()
    {
        animation = anim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void activateSlider()
    {
        Debug.Log("Canone betreten");
        Debug.Log(canvas.activeSelf);
        canvas.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (Input.anyKey && other.gameObject.tag == "Player")
        {
            hold = true;
            // Debug.Log("Hold");
            prograss += .5f;
            slider.value = prograss;

                if (prograss >= 100)
                {
                    other.GetComponent<PlayerControllerScript>().haltPlayer();
                    obstical.SetActive(false);
                    other.GetComponent<PlayerControllerScript>().unhaltPlayer();
                    canvas.SetActive(false);
                    //animation.  ["Block_Stack"].wrapMode = WrapMode.Once;
                    animation.Play("Block_Stack", 0, 0f);
                    animation.enabled = false;
                    anim.SetActive(false);
            }
        }

       

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (hold)
        {
            hold = false;
            // Debug.Log("TriggerExit");
        }

    }
}

