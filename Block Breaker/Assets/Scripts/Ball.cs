
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    int screenWidthInUnits = 16;
    [SerializeField] Paddle myPaddle;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    Vector2 paddleBallOffset;
    bool hasStarted = false;

    Rigidbody2D ballRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        paddleBallOffset = transform.position - myPaddle.transform.position;
        ballRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted == false)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            ballRigidBody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePosition = new Vector2(myPaddle.transform.position.x, myPaddle.transform.position.y);
        //Applying paddle position + the offset into the ball's position
        transform.position = paddlePosition + paddleBallOffset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2 (Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));
        AudioClip audioClip = ballSounds[UnityEngine.Random.Range(0, 4)];
        GetComponent<AudioSource>().PlayOneShot(audioClip);
        ballRigidBody2D.velocity += velocityTweak;
    }
}
