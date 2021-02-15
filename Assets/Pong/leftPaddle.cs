using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class leftPaddle : MonoBehaviour 
{
    GameObject ballReference;


    public Rigidbody paddleL;
    public Rigidbody paddleR;

    public bool over;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
            if (Input.GetKey(KeyCode.W))
            {

                paddleL.AddForce(Vector3.forward);
            }
            if (Input.GetKey(KeyCode.S))
            {
                paddleL.AddForce(Vector3.back);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {

                paddleR.AddForce(Vector3.forward);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                paddleR.AddForce(Vector3.back);
            }

        


    }

    
}
