using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCar : MonoBehaviour {

     /* public KeyCode turnRight;
      public KeyCode turnLeft;
      public KeyCode bracking;

      private float rotationSpeed;
      private Vector3 originalRotation;
      private SpriteRenderer spRenderer;
      private Vector3 toVector;
      private bool hasRightSkided;
      private bool hasLeftSkided;

      private float skidAngle;

      private float difAngle;

      private Vector3 targetAngle;*/

    public ParticleSystem exhaust;
    public ParticleSystem skidEffect;
    /*
      // Use this for initialization
      void Start()
      {
          spRenderer = GetComponent<SpriteRenderer>();
          rotationSpeed = 2.5f;
          hasRightSkided = false;
          hasLeftSkided = false;
          skidAngle = 0f;
          difAngle = 0f;

  }

  // Update is called once per frame
  void Update () {

          print("angle car   " + transform.rotation.z+ "  parent " + transform.parent.rotation.z);
          print(" skid left  " + hasLeftSkided);
          print(" skid right  " + hasRightSkided);
          bool isBraked = Input.GetKey(bracking);
          bool isTurnedRight = Input.GetKey(turnRight);
          bool isTurnedLeft = Input.GetKey(turnLeft);



          if (isTurnedRight && isBraked)
          {
              //transform.Rotate(Vector3.back * rotationSpeed);
              transform.localEulerAngles += Vector3.back * rotationSpeed;
               hasRightSkided = true;

          }
          if (isTurnedLeft && isBraked)
          {
              //transform.Rotate(Vector3.forward * rotationSpeed);
              transform.localEulerAngles += Vector3.forward * rotationSpeed;
              hasLeftSkided = true;
          }

          if (hasRightSkided)
          {

              if (Vector3.Distance(transform.eulerAngles, transform.localEulerAngles) > 0.01f)
              {
                  transform.localEulerAngles = new Vector3( 0,0, Mathf.LerpAngle(transform.localEulerAngles.z, 0, 0.1f));

              }
              else
              {
                  transform.eulerAngles = targetAngle;
                  hasRightSkided = false;
              }
          }
          if (hasLeftSkided)
          {
              if (Vector3.Distance(transform.eulerAngles, transform.localEulerAngles) > 0.01f)
              {
                  transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.localEulerAngles.z, 0, 0.01f));
              }
              else
              {
                  transform.eulerAngles = targetAngle;
                  hasLeftSkided = false;
              }

          }


      }*/

    float speedForce = 7f;
    float torqueForce = -200f;
    float driftFactorSticky = 0.5f;
    float driftFactorSlippy = 0.9f;
    float maxStickyVelocity = 2.5f;
    float minStickyVelocity = 1.5f;
    void Start()
    {
        exhaust.emissionRate = 0;
        skidEffect.emissionRate = 0;

    }

    //check for button up/down then set a bool will used in fixedUpdate
    void Update()
    {
    }
        // Update is called once per frame
        void FixedUpdate()
    {
        float driftFactor = driftFactorSticky;
        Rigidbody2D car = GetComponent<Rigidbody2D>();
        skidEffect.emissionRate = 0;
        exhaust.emissionRate = 2;


        if (RightVelocity().magnitude > maxStickyVelocity)
        {
            driftFactor = driftFactorSlippy;
            skidEffect.emissionRate = 15;

        }

        car.velocity = ForwardVelocity() + RightVelocity() * driftFactorSlippy;

        if (Input.GetButton("Accelerate"))
        {
            //Debug.Log("Accelerate");
            car.AddForce(transform.up * speedForce);
            exhaust.emissionRate = 15;
            ///car.AddForceAtPosition()
            // consider using car.addForcePosition to apply force twice, at the position of the real tires/types.

        }
        if (Input.GetButton("Brakes"))
        {
            //Debug.Log("Accelerate");
            car.AddForce(transform.up * -speedForce/2);
            skidEffect.emissionRate = 15;
            ///car.AddForceAtPosition()
            // consider using car.addForcePosition to apply force twice, at the position of the real tires/types.

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
