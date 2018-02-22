using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{

   // public Sprite[] cavernManList;
    private SpriteRenderer spRenderer;
    private int currentSpriteId = 0;
    private int speedFrame;
    private Vector2 position;
    private float speedX;
    private float speedY;
    private float speed;
    public KeyCode turnRight;
    public KeyCode turnLeft;
    public KeyCode mForward;
    public KeyCode mBack;
    private float acceleration;
    private Vector2 direction;
    private float accelCoef;
    private float deccelCoef;


    // Use this for initialization
    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        speedX = 0.1f;
        speedY = 0.1f;
        speedFrame = 0;
        speed = 0;
        accelCoef = 30f;
        deccelCoef = 50f;
        position = new Vector2(transform.localPosition.x, transform.localPosition.y);
        acceleration = 0.00001f;
        //transform.Rotate(Vector3.forward, 90);


    }

    // Update is called once per frame
    void Update()
    {
        if(speed > 0.005 && !Input.GetKey(mForward))
        {
            speed -= acceleration * deccelCoef;
        }
       
        transform.position = transform.position + (transform.rotation * Vector3.up) * speed;
        //avancer 
        if (Input.GetKey(mForward))
        {
            speed += acceleration * accelCoef;
        }
        //reculer
        if (Input.GetKey(mBack))
        {
            deccelCoef -= 0.005f;
        }
        //tourner à droite
        if (Input.GetKey(turnRight))
        {
            transform.Rotate(Vector3.back * 2.5f);
            
        }
        //tourner à gauche
        if (Input.GetKey(turnLeft))
        {
            transform.Rotate(Vector3.forward * 2.5f);
        }
       


    }
}
