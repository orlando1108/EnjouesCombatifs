using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCar : MonoBehaviour {

    public ParticleSystem exhaust;
    public ParticleSystem skidEffect;
    public ParticleSystem boostEffect;
   // public SparkPool sparkPool;
    float speedForce = 9f;
    float torqueForce = -200f;
    float driftFactorSticky = 0.6f;
    float driftFactorSlippy = 0.9f;
    float maxStickyVelocity = 2.5f;
    float minStickyVelocity = 1.5f;
    //float audioClipSpeed = 6f;
    AudioSource motorSound;
    Rigidbody2D car;
    float audioClipSpeed = 6f;

    public KeyCode RIGHT;
    public KeyCode LEFT;
    public KeyCode FORWARD;
    public KeyCode BACK;
    

    
    void Start()
    {
       // this.GetComponent<ParticleSystem>().enableEmission = false;
        exhaust.emissionRate = 0;
        skidEffect.emissionRate = 0;
        boostEffect.emissionRate = 0;
        //sparkEffect.enableEmission = false;
        motorSound = GetComponent<AudioSource>();
        motorSound.Play();
        car = GetComponent<Rigidbody2D>();

    }

    //check for button up/down then set a bool will used in fixedUpdate
    void Update()
    {
    }
        // Update is called once per frame
        void FixedUpdate()
    {
        float driftFactor = driftFactorSticky;
        float pitch = car.velocity.magnitude / audioClipSpeed;
        skidEffect.emissionRate = 0;
        exhaust.emissionRate = 2;

        
        motorSound.pitch = Mathf.Clamp(pitch, 0.5f, 3f);


        if (RightVelocity().magnitude > maxStickyVelocity)
        {
            driftFactor = driftFactorSlippy;
            skidEffect.emissionRate = 15;

        }

        car.velocity = ForwardVelocity() + RightVelocity() * driftFactorSlippy;

        if (Input.GetButton("Accelerate") || Input.GetKey(KeyCode.UpArrow))
        {
            car.AddForce(transform.up * speedForce);
            exhaust.emissionRate = 15;

        }
        if (Input.GetButton("Brakes") || Input.GetKey(KeyCode.LeftControl))
        {
            car.AddForce(transform.up * -speedForce/2);
            skidEffect.emissionRate = 15;

        }
        if (Input.GetButton("Boost") || Input.GetKey(KeyCode.Space))
        {
           // car.AddForce(transform.up * speedForce);
            speedForce = 12;
            boostEffect.emissionRate = 25;
        }
        else
        {
            boostEffect.emissionRate = 0;
            speedForce = 9;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            car.angularVelocity += 0.1f;
        }
        else
        {
            float tf = Mathf.Lerp(0, torqueForce, car.velocity.magnitude / 5);
            car.angularVelocity = Input.GetAxis("Horizontal") * tf;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            car.angularVelocity -= 0.1f;
        }
        else
        {
            float tf = Mathf.Lerp(0, torqueForce, car.velocity.magnitude / 5);
            car.angularVelocity = Input.GetAxis("Horizontal") * tf;
        }


        
        //car.AddTorque(Input.GetAxis("Horizontal") * torqueForce);
    }

    Vector2 ForwardVelocity()
    {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }
    Vector2 RightVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }

}
