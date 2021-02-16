using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ball : MonoBehaviour
{

    public float bounce = 3;
    public Rigidbody pongBall;

    public Text rightScore;
    public Text leftScore;

    public double timer = 3.0;
    public double startTimer = 3.0;
    public double blockTimer = 5;
    public double powerupT = 10;
    bool pTimer = false;
    bool bTimer = false;
    private bool stActivated = false;
    private bool activated = false;
    public bool lHit = true;
     
    float scoreL = 0;
    float scoreR = 0;

    public AudioSource paddle;
    public AudioSource wall;
    public AudioSource score;
    public AudioSource win;

    public GameObject teleport;
    public GameObject rBlockT;
    public GameObject lBlockT;
    public GameObject resetT;

    public Rigidbody blocker;
    public Rigidbody powerUp;

    float speedup = 1;

    Vector3 latestVelocity;

    public bool gameOver = false;
    // Start is called before the first frame update

    void Start()
    {
        pongBall = GetComponent<Rigidbody>();
        
        pongBall.velocity = new Vector3(bounce, 0, 0);

        pTimer = true;

    }

    // Update is called once per frame
    void Update()
    {

        latestVelocity = pongBall.velocity;
        
        if (stActivated)
        {
            startTimer -= Time.deltaTime;
            if (startTimer < 0)
            {
                stActivated = false;
                if (lHit)
                {
                    bounce = 3;
                    pongBall.velocity = new Vector3(bounce, 0, 0);
                }
                else
                {
                    bounce = 3;
                    pongBall.velocity = new Vector3(-bounce, 0, 0);
                }
                startTimer = 3;
                pTimer = true;

            }
            
        }

        if (activated)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                activated = false;
                timer = 3;
                pongBall.position = teleport.transform.position;
                stActivated = true;
            }
        }

        if (bTimer)
        {
            
            blockTimer -= Time.deltaTime;
            if(blockTimer < 0)
            {
                blocker.position = resetT.transform.position;
                blockTimer = 5;
                pTimer = true;
                bTimer = false;
            }

        }


        if(pTimer)
        {
            powerupT -= Time.deltaTime;
            if(powerupT < 0)
            {
                int num = Random.Range(1, 10);
                if(num < 10)
                {
                    powerUp.position = teleport.transform.position;
                    pTimer = false;
                }

                powerupT = 10;
            }
        }
    }

   

    

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "PaddleLeft" || collision.gameObject.name == "PaddleRight")
        {
            paddle.pitch = speedup;
            paddle.Play();
            speedup+= .3f;
            bounce += 1f;

            if (collision.gameObject.name == "PaddleLeft")
            {
                lHit = true;
            }
            else
            {
                lHit = false;
            }
            
            float bounceLocation = transform.position.z - collision.transform.position.z;
            float maxHeight = collision.collider.bounds.extents.z;
            float bounceAngle = bounceLocation / maxHeight;

           

            
            float newHorizontalSpeed = (lHit) ? bounce : -bounce;

            Vector3 newVelocity = new Vector3(newHorizontalSpeed, 0f, bounceAngle * 4f).normalized * bounce;
            pongBall.velocity = newVelocity;
           
        }

        if (collision.gameObject.name == "Goal Right" || collision.gameObject.name == "Goal Left")
        {
            pTimer = false;
            powerUp.position = resetT.transform.position;
            speedup = 1;

            if (collision.gameObject.name == "Goal Right")

            {
                scoreL++;
                leftScore.text = scoreL.ToString();
                pongBall.velocity = new Vector3(0, 0, 0);
            }
            if (collision.gameObject.name == "Goal Left")

            {
                scoreR++;
                
                rightScore.text = scoreR.ToString();
                pongBall.velocity = new Vector3(0, 0, 0);
            }

            if (scoreR < scoreL)
            {
                rightScore.color = new Color(1f, .5f - (scoreL-scoreR)/20f, .5f - (scoreL - scoreR) / 20f);
                leftScore.color = new Color(0f, 1f, 0f);
            }
            else if(scoreR > scoreL)
            {
                leftScore.color = new Color(1f, .5f - (scoreR - scoreL) / 20f, .5f - (scoreR - scoreL) / 20f);
                rightScore.color = new Color(0f, 1f, 0f);
            }
            else
            {
                leftScore.color = new Color(1f, 1f, 1f);
                rightScore.color = new Color(1f, 1f, 1f);
            }

            if (scoreL == 11)
            {
                win.Play();
                leftScore.text = "WIN";
                leftScore.color = new Color(1f, 1f, 0f);
                rightScore.color = new Color(1f, 0f, 0f);
                rightScore.text = "---";
                pongBall.velocity = new Vector3(0, 0, 0);
                pongBall.position = teleport.transform.position;
                gameOver = true;
            }
            if (scoreR == 11)
            {
                win.Play();
                rightScore.text = "WIN";
                rightScore.color = new Color(1f, 1f, 0f);
                leftScore.color = new Color(1f, 0f, 0f);
                leftScore.text = "---";
                pongBall.velocity = new Vector3(0, 0, 0);
                pongBall.position = teleport.transform.position;
                gameOver = true;
            }

            if(!gameOver)
            {
                activated = true;
                score.Play();
            }
            

            
            

        }

        if (collision.gameObject.name == "Wall" || collision.gameObject.name == "Block")
        {
            wall.Play();
            //var direction = Vector3.Reflect(latestVelocity.normalized, collision.contacts[0].normal);
            //pongBall.velocity = direction * bounce;
        }

        
         

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Power")
        {
            Debug.Log($"before collision ({Time.frameCount}) = {transform.position}");
            powerUp.position = resetT.transform.position;
            if (lHit)
            {
                blocker.transform.position = lBlockT.transform.position;

            }
            else if (!lHit)
            {
                blocker.transform.position = rBlockT.transform.position;
            }
            bTimer = true;
            Debug.Log($"after collision ({Time.frameCount}) = {transform.position}");
        }
    }
}


