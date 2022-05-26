using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallMove : MonoBehaviour
{
    private Vector2 startPosition;
    
    public float speed;
    private float xSpeed;
    private float ySpeed;

    private int score;
    private int highScore;
    private int scoreMultiplier;

    private string lastHit;

    private float relativeDirectionX;
    private float relativeDirectionY;

    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI multiplierText;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        highScore = 0;
        scoreMultiplier = 1;
        lastHit = "none";

        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.position;
        xSpeed = 0;
        ySpeed = speed;
        moveVelocity = new Vector2(xSpeed, ySpeed);
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    private void FixedUpdate()
    {

               

        if (rb.position.y < 5.4 && rb.position.y > -5.5 && rb.position.x < 8.4 && rb.position.x > -8.5) //Make sure ball is still in-bounds
        {
            rb.velocity = moveVelocity;
        }
        else
        {
            Debug.Log("out of bounds");
            rb.MovePosition(startPosition);
            scoreMultiplier = 1;
            multiplierText.text = scoreMultiplier.ToString();
            xSpeed = 0;
            ySpeed = speed;
            moveVelocity = new Vector2(xSpeed, ySpeed);

            if (score > highScore)
            {
                highScore = score;
                highScoreText.text = highScore.ToString();
            }
            
            score = 0;
            scoreText.text = score.ToString();
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        


        if (collision.gameObject.tag == "Top")
        {
            ySpeed = -ySpeed;
            scoreMultiplier = 1;
            multiplierText.text = scoreMultiplier.ToString();
            lastHit = "Wall";

        }
        else if (collision.gameObject.tag == "Side")
        {
            xSpeed = -xSpeed;
            scoreMultiplier = 1;
            multiplierText.text = scoreMultiplier.ToString();
            lastHit = "Wall";
        }
        else if (collision.gameObject.tag == "Paddle")
        {

            if (collision.gameObject.name == "Paddle2")
            {
                if (lastHit == "Paddle1" && scoreMultiplier < 3)
                {
                    scoreMultiplier++;
                    multiplierText.text = scoreMultiplier.ToString();
                }
                else if (lastHit == "Paddle2")
                {
                    scoreMultiplier = 1;
                    multiplierText.text = scoreMultiplier.ToString();
                }
                lastHit = "Paddle2";
            }
            else if (collision.gameObject.name == "Paddle")
            {
                if (lastHit == "Paddle2" && scoreMultiplier < 3)
                {
                    scoreMultiplier++;
                    multiplierText.text = scoreMultiplier.ToString();
                }
                else if (lastHit == "Paddle1")
                {
                    scoreMultiplier = 1;
                    multiplierText.text = scoreMultiplier.ToString();
                }
                lastHit = "Paddle1";
            }

            ySpeed = -ySpeed;

            relativeDirectionX = collision.gameObject.transform.position.x - transform.position.x;   //Used to figure out which part of the paddle the ball hit.
            
            if (relativeDirectionX > 0.1)
            {
                xSpeed = -speed * Mathf.Abs(relativeDirectionX * 2);
            }
            else if (relativeDirectionX < -0.1)
            {
                xSpeed = speed * Mathf.Abs(relativeDirectionX) * 2;
            }
            else
            {

            }

        }
        else if (collision.gameObject.tag == "Enemy")
        {
            score += 1 * scoreMultiplier;
            Debug.Log("multiplier: " + scoreMultiplier);
            scoreMultiplier = 1;
            multiplierText.text = scoreMultiplier.ToString();
            scoreText.text = score.ToString();  //Update GUI


            //Calculate position of ball relative to enemy square
            relativeDirectionX = collision.gameObject.transform.position.x - transform.position.x;
            relativeDirectionY = collision.gameObject.transform.position.y - transform.position.y;

            
            //Bounce off the correct trajectory from enemy square
            if (relativeDirectionX > .49 || relativeDirectionX < -.49)
            {
                xSpeed = -xSpeed;
            }
            else if (relativeDirectionY > .49 || relativeDirectionY < -.49)
            {
                ySpeed = -ySpeed;
            }
        }
        
       
        if (xSpeed > 20)        //Speed cap to make sure the ball doesn't move too fast.
        {
            xSpeed = 20;
        }
        
        moveVelocity = new Vector2(xSpeed, ySpeed);
        rb.velocity = moveVelocity;



    }

}
