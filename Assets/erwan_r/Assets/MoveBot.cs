using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBot : MonoBehaviour
{

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
    public ParticleSystem exhaust;
    public ParticleSystem skidEffect;
    public ParticleSystem boostEffect;
    public List<Transform> nodes;
    float speedForce = 7f;
    float torqueForce = -200f;
    float driftFactorSticky = 0.5f;
    float driftFactorSlippy = 0.7f;
    float maxStickyVelocity = 2.5f;
    float minStickyVelocity = 1.5f;
    float audioClipSpeed = 6f;
    
    private int currentNode = 0;
    float targetRot;
    AudioSource motorSound;
    Rigidbody2D bot;
    Vector3 direction;
   



    /*public float acceleration = 0.00001f;
    public float braking = 0.1f;
    public float steering = 0.001f;*/
    Vector3 target;

    void Start()
    {
        exhaust.emissionRate = 0;
        skidEffect.emissionRate = 0;
        boostEffect.emissionRate = 0;
        motorSound = GetComponent<AudioSource>();
        motorSound.Play();
        bot = GetComponent<Rigidbody2D>();
        direction = (nodes[currentNode].position - bot.transform.position);
        //bot.transform.position = new Vector2(nodes[currentNode].position.x, nodes[currentNode].position.y);

    }
    
    void Update()
    {
    }

    void FixedUpdate()
    {
        float driftFactor = driftFactorSticky;
        float pitch = bot.velocity.magnitude / audioClipSpeed;

        CheckDistance();
        SteerTowardsTarget();

        skidEffect.emissionRate = 0;
        exhaust.emissionRate = 2;
        //CheckDistance();

        // ACCELERATE
        bot.velocity = ForwardVelocity() + RightVelocity() * driftFactorSlippy;
        direction = (nodes[currentNode].position - bot.transform.position);
        bot.AddForce(direction.normalized * speedForce);
        exhaust.emissionRate = 15;

        //ROTATE
        //float targetRot = Vector2.Angle(bot.transform.up, direction);
        //Debug.Log("ANGLE  " + targetRot);
        //bot.transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, targetRot, 0.05f));

        //MOTOR SOUND
        motorSound.pitch = Mathf.Clamp(pitch, 0.5f, 3f);

       // bot.velocity = ForwardVelocity() + RightVelocity() * driftFactorSlippy;

        /*if (RightVelocity().magnitude > maxStickyVelocity)
        {
            driftFactor = driftFactorSlippy;
            skidEffect.emissionRate = 15;

        }*/

       /* if (Input.GetButton("Brakes"))
        {
            bot.AddForce(transform.up * -speedForce / 2);
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
        }*/

       


        // always accelerate
        float velocity = bot.velocity.magnitude;
        //velocity += acceleration;

        // apply car movement
       /* bot.velocity = transform.up * velocity;
        bot.angularVelocity = 0.0f;*/


    }

    Vector2 ForwardVelocity()
    {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }
    Vector2 RightVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }
    void SteerTowardsTarget()
    {
       // Vector2 towardNextTrigger = direction - bot.transform.position;

        float targetRot = Vector2.Angle(Vector2.right, direction);
      /*  Debug.Log("bOT ANGLE  " + bot.transform.eulerAngles);
        Debug.Log("DIR ANGLE  " + direction);*/
        Debug.Log("ANGLE  " + targetRot);
         if (direction.y < 0.0f)
         {
             targetRot = -targetRot;
         }
        // float rot = Mathf.MoveTowardsAngle(bot.transform.localEulerAngles.z, targetRot, steering);
        //bot.transform.eulerAngles = new Vector3(0.0f, 0.0f, bot.transform.eulerAngles.z + targetRot);

         bot.transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.localEulerAngles.z, targetRot -90 , 0.1f));
       // bot.transform.up = new Vector3(Mathf.Lerp(bot.transform.up.x, direction.x, 0.5f), Mathf.Lerp(bot.transform.up.y, direction.y, 0.5f), Mathf.Lerp(bot.transform.up.z, direction.z, 0.5f));
       
    }

    private void CheckDistance()
    {
        // Debug.Log(" DISTANCE " + Vector3.Distance(bot.transform.position, nodes[currentNode].position));
        if (Vector3.Distance(bot.transform.position, nodes[currentNode].position) < 0.5f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
                /* target = Vector3.Lerp(nodes[currentNode].transform.position - nodes[currentNode].transform.up,
                               nodes[currentNode].transform.position + nodes[currentNode].transform.up,
                               Random.value);*/
            }
            else
            {
                currentNode++;
                
                /*target = Vector3.Lerp(nodes[currentNode].transform.position - nodes[currentNode].transform.up,
                              nodes[currentNode].transform.position + nodes[currentNode].transform.up,
                              Random.value);*/


            }
        }

    }








}
