using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
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
    //private Vector2 direction;
    private float accelCoef;
    private float autoDeccelCoef;
    private float brakingCoef;


    // Use this for initialization
    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        speedX = 0.1f;
        speedY = 0.1f;
        speedFrame = 0;
        speed = 0;
        accelCoef = 30f;
        autoDeccelCoef = 50f;
        brakingCoef = 80f;

        position = new Vector2(transform.localPosition.x, transform.localPosition.y);
        acceleration = 0.00001f;
        //transform.Rotate(Vector3.forward, 90);


    }

    // Update is called once per frame
    void Update()
    {
        if(speed > 0.004 && !Input.GetKey(mForward) && !Input.GetKey(mBack))
        {
            speed -= acceleration * autoDeccelCoef;
        }
       
        transform.position = transform.position + (transform.rotation * Vector3.up) * speed;
        //avancer 
        if (Input.GetKey(mForward) && speed <0.05)
        {
            speed += acceleration * accelCoef;
        }
        //reculer
        if (Input.GetKey(mBack) && speed > -0.03)
        {
            //brakingCoef -= 0.5f;
            speed -= acceleration * brakingCoef;
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
