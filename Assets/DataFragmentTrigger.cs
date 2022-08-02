using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFragmentTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    DataFragment parent;
    void Start()
    {
        parent = transform.parent.GetComponent<DataFragment>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("DataFragment collide with " + other.tag);
        if (other.tag == "PlayerShell")
        {
            parent?.Absorbed();
        }
    }
}
