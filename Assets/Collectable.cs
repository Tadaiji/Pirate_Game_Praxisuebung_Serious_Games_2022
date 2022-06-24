using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    bool collect_state = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteObject()
    {
        if (!collect_state)
        {
            collect_state = true;
            Destroy(this.gameObject);
        }

    }
}
