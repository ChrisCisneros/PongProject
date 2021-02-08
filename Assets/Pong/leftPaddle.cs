using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftPaddle : MonoBehaviour
{
    
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody paddle = GetComponent<Rigidbody>();
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("you clicked me!");
            paddle.AddForce(Vector3.forward);
        }
        if(Input.GetKey(KeyCode.S))
        {
            paddle.AddForce(Vector3.back);
        }

        
    }
}
