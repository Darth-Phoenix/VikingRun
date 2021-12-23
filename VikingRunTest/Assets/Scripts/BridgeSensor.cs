using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class BridgeSensor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.name == "viking")
        {
            Debug.Log("viking enter");
        }
    }

    //OnTriggerStay
    //OnTriggerExit

  

    // Update is called once per frame
    void Update()
    {
        
    }
}
