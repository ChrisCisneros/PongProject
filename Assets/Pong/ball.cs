using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ball : MonoBehaviour
{
    private Vector3 force = new Vector3(8, 0, 0);
    
    public Rigidbody pongBall;

    public Text rightScore;
    public Text leftScore;

    int scoreL = 0;
    int scoreR = 0;
    // Start is called before the first frame update
    
    void Start()
    {
        pongBall = GetComponent<Rigidbody>();
        pongBall.velocity = new Vector3(8, 0, 0);



    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(15f * Time.deltaTime, Space.World);
        
        transform.position += force * Time.deltaTime;
        pongBall.velocity = 8 * (pongBall.velocity.normalized);
    }

    private void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.name == "Goal Right")
        {
            scoreL++;
            leftScore.text = scoreL.ToString();
            
        }
        if (col.gameObject.name == "Goal Left")
        {
            scoreR++;
            rightScore.text = scoreR.ToString();
        }
        
    }

    
}


