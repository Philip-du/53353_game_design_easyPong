using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HardPong : MonoBehaviour
{
    // ball variables
    public GameObject ball;  // the game object of the ball

    public Vector3 direction;  // the vector indicating heading
    public float speed = 1.0f;  // value to move as speed/velocity


    // paddle variables
    public GameObject leftPaddle, rightPaddle;
    public float paddleSpeed = 1.0f;

    // boundary variables
    private float FIELD_LEFT = -48.5f;
    private float FIELD_RIGHT = 48.5f;
    private float FIELD_TOP = 23.5f;
    private float FIELD_BOTTOM = -23.5f;


    // Start is called before the first frame update
    void Start()
    {
        ball.transform.position = Vector3.zero;
        direction = Random.insideUnitCircle.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        
        // call move paddles
        MovePaddles();

        // move the ball by a small offset  (speed * direction)
        ball.transform.position = ball.transform.position + (direction * speed * Time.deltaTime);

        // check to see if the ball should bounce
        CheckBallPosition();
    }

    void MovePaddles()
    {
        // Where are the paddles
        Vector3 leftPaddlePosition = leftPaddle.transform.position;
        Vector3 rightPaddlePosition = rightPaddle.transform.position;

        // figure out how far each should move
        float paddleDistance = paddleSpeed * Time.deltaTime;

        // where the paddles should move to

        // left paddle
        if(Input.GetKey("w"))
        {
            leftPaddlePosition.z = leftPaddlePosition.z + paddleDistance;
        } 

        if (Input.GetKey("s"))
        {
            leftPaddlePosition.z = leftPaddlePosition.z - paddleDistance;
        }

        // right paddle
        if (Input.GetKey("up"))
        {
            rightPaddlePosition.z += paddleDistance;
        }

        if (Input.GetKey("down"))
        {
            rightPaddlePosition.z -= paddleDistance;
        }

        // update the paddle position
        leftPaddle.transform.position = leftPaddlePosition;
        rightPaddle.transform.position = rightPaddlePosition;


    }

    void CheckBallPosition()
    {
        // get the current position of the ball
        Vector3 currentPosition = ball.transform.position;

        // test the boundaries
        if (currentPosition.z >= FIELD_TOP)
        {
            // bounce off of the top
            BounceBall(currentPosition, "top");
        } else if (currentPosition.z <= FIELD_BOTTOM)
        {
            // bounce off of the bottom
            BounceBall(currentPosition, "bottom");
        }

        if (currentPosition.x >= FIELD_RIGHT)
        {
            // bounce off of right
            BounceBall(currentPosition, "right");
        } else if (currentPosition.x <= FIELD_LEFT)
        {
            // bounce off of left
            BounceBall(currentPosition, "left");
        }

    }

    void BounceBall(Vector3 ballPosition, string bounceObject)
    {
        // process the bounce of the ball, reflect it based upon direction

        switch(bounceObject)
        {
            case "top":
                // bounce off of the top
                ballPosition.z = FIELD_TOP - (ballPosition.z - FIELD_TOP);
                direction.z = -direction.z;
                break;

            case "bottom":
                // bounce off of the bottom
                ballPosition.z = FIELD_BOTTOM - (ballPosition.z - FIELD_BOTTOM);
                direction.z = -direction.z;
                break;

            case "left":
                // bounce off of the left side
                ballPosition.x = (2 * FIELD_LEFT) - ballPosition.x;
                direction.x *= -1;
                break;

            case "right":
                // bounce off of the right
                ballPosition.x = (2 * FIELD_RIGHT) - ballPosition.x;
                direction.x *= -1;
                break;

             default:
                Debug.Log("Invalid Bounce Object: " + bounceObject); break;

        }

        // update the ball position with new value
        ball.transform.position = ballPosition;


    }

}
