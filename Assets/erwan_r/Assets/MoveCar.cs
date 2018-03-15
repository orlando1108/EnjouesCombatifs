using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCar : MonoBehaviour {

    public ParticleSystem exhaust;
    public ParticleSystem skidEffect;
    public ParticleSystem boostEffect;
    float speedForce = 6f;
    float torqueForce = -200f;
    float driftFactorSticky = 0.5f;
    float driftFactorSlippy = 0.9f;
    float maxStickyVelocity = 2.5f;
    float minStickyVelocity = 1.5f;
    AudioSource motorSound;
    Rigidbody2D car;
    float audioClipSpeed = 6f;

    
    void Start()
    {
        exhaust.emissionRate = 0;
        skidEffect.emissionRate = 0;
        boostEffect.emissionRate = 0;
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

        if (Input.GetButton("Accelerate"))
        {
            car.AddForce(transform.up * speedForce);
            exhaust.emissionRate = 15;

        }
        if (Input.GetButton("Brakes"))
        {
            car.AddForce(transform.up * -speedForce/2);
            skidEffect.emissionRate = 15;

        }
        if (Input.GetButton("Boost"))
        {
           // car.AddForce(transform.up * speedForce);
            speedForce = 10;
            boostEffect.emissionRate = 25;
        }
        else
        {
            boostEffect.emissionRate = 0;
            speedForce = 6;
        }


        //if you are using positionnal wheel in your physics, then you probably instead of aadding angular momentum or torque,
        // you'll instead want to add left/right force at the position of the two front tire/types proportional to your current forward speed.
        // we are converting some forward speed into sideway force).
        float tf = Mathf.Lerp(0, torqueForce, car.velocity.magnitude / 5);
        car.angularVelocity = Input.GetAxis("Horizontal") * tf;
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
   /* Vector2 LeftVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }*/


}
