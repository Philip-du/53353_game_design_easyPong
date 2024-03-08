using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    public string upButton;
    public string downButton;

    private float speed = 50.0f;
    private float offset = 18.0f;

    private EasyPong gameManager;
    
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<EasyPong>();

    }

    // Update is called once per frame
    void Update()
    {

        if ((gameManager.currentState == GameState.Playing) || (gameManager.currentState == GameState.PreRound))
        {
            MovePaddle();
        }


    }

    private void MovePaddle()
    {
        // get the current position of the paddle
        Vector3 currentPosition = transform.position;

        // check the buttons
        if (Input.GetKey(upButton))
        {
            currentPosition.z += speed * Time.deltaTime;
        }

        if (Input.GetKey(downButton))
        {
            currentPosition.z -= speed * Time.deltaTime;
        }

        currentPosition.z = Mathf.Clamp(currentPosition.z, -offset, offset);

        // update our position
        transform.position = currentPosition;
    }
    // 2023 53353 UGE F23
}
