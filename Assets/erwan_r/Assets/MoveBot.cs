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
    float speedForce = 1f;
    float torqueForce = -200f;
    float driftFactorSticky = 0.5f;
    float driftFactorSlippy = 0.9f;
    float maxStickyVelocity = 2.5f;
    float minStickyVelocity = 1.5f;
    float audioClipSpeed = 6f;
    public float steering = 4.0f;
    private int currentNode = 0;
    AudioSource motorSound;
    Rigidbody2D bot;
    Vector3 direction;
    Vector3 target;



    void Start()
    {
        exhaust.emissionRate = 0;
        skidEffect.emissionRate = 0;
        boostEffect.emissionRate = 0;
        motorSound = GetComponent<AudioSource>();
        motorSound.Play();
        bot = GetComponent<Rigidbody2D>();

    }
    
    void Update()
    {
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float driftFactor = driftFactorSticky;
        float pitch = bot.velocity.magnitude / audioClipSpeed;

        skidEffect.emissionRate = 0;
        exhaust.emissionRate = 2;
        SteerTowardsTarget();
        CheckDistance();

        // ACCELERATE
         direction = (nodes[currentNode].position - bot.transform.position);
         bot.AddForce(direction.normalized);
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

        if (Input.GetButton("Brakes"))
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
        }
        
        float tf = Mathf.Lerp(0, torqueForce, bot.velocity.magnitude / 5);
       // bot.angularVelocity = Input.GetAxis("Horizontal") * tf;
       // bot.AddTorque(Input.GetAxis("Horizontal") * torqueForce);


    }

    Vector2 ForwardVelocity()
    {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }
    Vector2 RightVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }

    private void CheckDistance() {
       // Debug.Log(" DISTANCE " + Vector3.Distance(bot.transform.position, nodes[currentNode].position));
        if(Vector3.Distance(bot.transform.position, nodes[currentNode].position) < 0.3f)
        {
            if(currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
                


            }
        }

    }

    void SteerTowardsTarget()
    {
       // Vector2 towardNextTrigger = target - bot.transform.position;
        float targetRot = Vector2.Angle(bot.transform.up, direction);
        Debug.Log("ROT  " + targetRot);


         /*if (direction.y < 0.0f)
         {
             targetRot = -targetRot;
         }*/
        //float rot = Mathf.MoveTowardsAngle(transform.localEulerAngles.z, targetRot, 0.5f);
        // bot.MoveRotation(targetRot);
        /*float rot = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetRot, steering);
        transform.eulerAngles = new Vector3(0.0f, 0.0f, rot);*/
        //bot.transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, targetRot, 0.5f));
        transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, targetRot, 0.01f));

    }


}
