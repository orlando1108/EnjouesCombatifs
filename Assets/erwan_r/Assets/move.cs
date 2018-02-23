using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

   // public Sprite[] cavernManList;
    private SpriteRenderer spRenderer;
    private Vector2 position;
    private float speed;
    public ParticleSystem exhaust;
    
    public KeyCode turnRight;
    public KeyCode turnLeft;
    public KeyCode mForward;
    public KeyCode mBack;
    public KeyCode bracking;
    private float acceleration;
    //private Vector2 direction;
    private float moveForwardCoef;
    private float autoDeccelCoef;
    private float brakingCoef;
    private float moveBackCoef;
    private float rotationSpeed;
    private float rotationSpeed_max;
    private float rotationSpeed_min;
    private float speed_max;


    // Use this for initialization
    void Start()
    {
        //exhaust = GetComponent<ParticleSystem>();
        exhaust.emissionRate = 0;
        spRenderer = GetComponent<SpriteRenderer>();
        speed = 0;
        speed_max = 0.12f;
        rotationSpeed = 2.5f;
        moveForwardCoef = 40f;
        autoDeccelCoef = 50f;
        moveBackCoef= 20f;
        brakingCoef = 100f;
        position = new Vector2(transform.localPosition.x, transform.localPosition.y);
        acceleration = 0.00001f;
        rotationSpeed_max = 1.8f;
        rotationSpeed_min = 0.5f;
        //transform.Rotate(Vector3.forward, 90);


    }

    // Update is called once per frame
    void Update()
    {
        bool isTurnedRight = Input.GetKey(turnRight);
        bool isTurnedLeft = Input.GetKey(turnLeft);
        bool isMovedForward = Input.GetKey(mForward);
        bool isMovedBack = Input.GetKey(mBack);
        bool isBraked = Input.GetKey(bracking);

        transform.position = transform.position + (transform.rotation * Vector3.up) * speed;

        if (speed > 0.004 && !Input.GetKey(mForward) && !Input.GetKey(mBack))
        {
            speed -= acceleration * autoDeccelCoef;
            rotationSpeed =  getSpeedRotationFromSpeed(speed);
            exhaust.emissionRate = 2;
        }
        
        
        //avancer 
        if (isMovedForward && speed < speed_max)
        {

            //exhaust.Play();
            exhaust.emissionRate = 15;
            speed += acceleration * moveForwardCoef;
            rotationSpeed = getSpeedRotationFromSpeed(speed);
        }
        //reculer
        if (isMovedBack && speed > -0.03)
        {
            //brakingCoef -= 0.5f;
            speed -= acceleration * moveBackCoef;
        }
        //freiner
        if (isBraked && speed > 0)
        {
            //brakingCoef -= 0.5f;
            speed -= acceleration * brakingCoef;
            rotationSpeed = getSpeedRotationFromSpeed(speed);
        }
       
        //tourner à droite
        if (isTurnedRight && speed >0.0005)
        {
            transform.Rotate(Vector3.back * rotationSpeed);

            if (isBraked)
            {
                //transform.Rotate(Vector3.back * rotationSpeed);
                //transform.Rotate(-Vector3.forward * 3);
            }
            
            
        }
        //tourner à gauche
        if (isTurnedLeft && speed > 0.0005)
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
            //transform.Rotate(Vector3., 2f );
        }

        

    }
    //vitesse de virage inversement proportionnelle à la vitesse
    private float getSpeedRotationFromSpeed(float speedX) 
    {
        float rapport = speed_max * rotationSpeed_min;
        return Mathf.Clamp(Mathf.Abs(rapport * (1 / speedX)), rotationSpeed_min, rotationSpeed_max) * 3f;
         
    }
}
