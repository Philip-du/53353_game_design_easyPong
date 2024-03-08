using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameState { None, PreRound, Playing, PostRound, GameOver };

public class EasyPong : MonoBehaviour
{
    // ball variables
    public GameObject ballObject;
    private Rigidbody ballRigidbody;
    public float force;
    public Vector3 direction;
    private Vector3 ballStartPosition;
    //public TextMeshProUGUI countText;
    private Renderer ballRender;

    // game variables
    public GameState currentState = GameState.None;

    private int playerOneScore, playerTwoScore;
    private int MAX_SCORE = 3;

    // UI Variable
    public Text overlayMessage;
    public Text scoreMessage;

    private AudioSource audio;

    public AudioClip goSound, countSound;

    // Start is called before the first frame update
    void Start()
    {
        // fetch the ball components
        ballRigidbody = ballObject.GetComponent<Rigidbody>();   // assign the rigidbody TO THE BALL
        ballRender = ballObject.GetComponent<Renderer>();
        audio = GetComponent<AudioSource>(); // get the access to the audio source

        // set my initial score values
        playerOneScore = 0;
        playerTwoScore = 0;

        //SetCountText();

        // get the ball start location
        ballStartPosition = ballObject.transform.position;

        // Make sure text is turned off
        overlayMessage.enabled = false;
        //scoreMessage.enabled = false;
        // start the first round
        InitializeRound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void SetCountText() 
    //{
    //    countText.text = playerOneScore.ToString() + "-" + playerTwoScore.ToString();
    //}

    private void InitializeRound()
    {
        // set our gamestate
        currentState = GameState.PreRound;
        // Debug.Log("current state is preround");

        // move the ball back to start position
        ResetBall();



        // start the GetReady coroutine
        StartCoroutine(GetReady());

    }

    private void ResetBall()
    {
        // make sure we have control of the ball
        ballRigidbody.isKinematic = true;


        // st the ball to the original position
        ballObject.transform.position = ballStartPosition;
    }

    private void StartBall()
    {
        // turn off isKinematic
        ballRigidbody.isKinematic = false;

        direction.Normalize(); // making the vector a heading
        ballRigidbody.AddForce(direction * force, ForceMode.Impulse); // push our ball

        // start playing
        currentState = GameState.Playing;
        // Debug.Log("currentstate = playing");
    }

    public void RegisterScore(int playerNumber)
    {
        // stop the action
        ballRigidbody.isKinematic = true;
        //scoreMessage.enabled = true;
        //scoreMessage.text = playerOneScore.ToString() + "-" + playerTwoScore.ToString();
        // process the score
        if (playerNumber == 1)
        {
            playerOneScore++;
        } else if (playerNumber == 2)
        {
            playerTwoScore++;
        } else
        {
            Debug.Log("Player Number was not assigned correctly.  Value=" + playerNumber);
        }

        // Display the Score
        Debug.Log("The Score is " + playerOneScore + "-" + playerTwoScore);
        overlayMessage.enabled = true;
        overlayMessage.text = playerOneScore.ToString() + "-" + playerTwoScore.ToString();
        //SetCountText();
        // go to postround state
        currentState = GameState.PostRound;

        // go start the next round
        InitializeRound();
    }

    public IEnumerator GetReady()
    {
        // set my overlay message
        overlayMessage.text = "3";


        // turn on the message
        overlayMessage.enabled = true;

        // turn off the ball
        ballRender.enabled = false;

        MakeCountSound();

        yield return new WaitForSeconds(0.5f);

        // turn off the message
        overlayMessage.enabled = false;

        // turn on the ball
        ballRender.enabled = true;

        yield return new WaitForSeconds(0.5f);


        overlayMessage.text = "2";

        // turn on the message
        overlayMessage.enabled = true;

        // turn off the ball
        ballRender.enabled = false;

        MakeCountSound();

        yield return new WaitForSeconds(0.5f);

        // turn off the message
        overlayMessage.enabled = false;

        // turn on the ball
        ballRender.enabled = true;

        yield return new WaitForSeconds(0.5f);

        
        overlayMessage.text = "1";


        // turn on the message
        overlayMessage.enabled = true;

        // turn off the ball
        ballRender.enabled = false;

        MakeCountSound();

        yield return new WaitForSeconds(0.5f);

        // turn off the message
        overlayMessage.enabled = false;

        // turn on the ball
        ballRender.enabled = true;

        yield return new WaitForSeconds(0.5f);

        
        overlayMessage.text = "GO!!!";

        
        // turn on the message
        overlayMessage.enabled = true;

        // turn off the ball
        ballRender.enabled = false;

        MakeGoSound();

        yield return new WaitForSeconds(0.5f);

        // turn off the message
        overlayMessage.enabled = false;

        // turn on the ball
        ballRender.enabled = true;

        yield return new WaitForSeconds(0.5f);

        // turn off the message
        overlayMessage.enabled = false;

        // resume after the pause
        StartBall();

    }

    private void MakeGoSound()
    {

        // play the sound one time
        audio.PlayOneShot(goSound);

    }

    private void MakeCountSound()
    {
        audio.PlayOneShot(countSound);
    }

    // 2023 53353 UGE F23
}
