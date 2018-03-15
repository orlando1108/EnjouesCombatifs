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
    private List<Transform> FindedNodes;
    float speedForce = 6f;
    float torqueForce = -200f;
    float driftFactorSticky = 0.5f;
    float driftFactorSlippy = 0.7f;
    float maxStickyVelocity = 2.5f;
    float minStickyVelocity = 1.5f;
    float audioClipSpeed = 6f;

    private int currentNode = 7;
    float targetRot;
    AudioSource motorSound;
    Rigidbody2D bot;
    Vector3 direction;
    private List<Node> nodeList = new List<Node> { };




    /*public float acceleration = 0.00001f;
    public float braking = 0.1f;
    public float steering = 0.001f;*/
    Vector3 target;

    void Start()
    {
        bot = GetComponent<Rigidbody2D>();


        Node localNode0 = new Node(nodes[0].name, 1);
        localNode0.point = nodes[0];
        localNode0.cout = 1;
        nodeList.Add(localNode0);


        Node localNode1 = new Node(nodes[1].name, 1);
        localNode1.point = nodes[1];
        localNode1.cout = 1;
        nodeList.Add(localNode1);


        Node localNode2 = new Node(nodes[2].name, 1);
        localNode2.point = nodes[2];
        localNode2.cout = 1;
        nodeList.Add(localNode2);


        Node localNode3 = new Node(nodes[3].name, 1);
        localNode3.point = nodes[3];
        localNode3.cout = 1;
        nodeList.Add(localNode3);


        Node localNode4 = new Node(nodes[4].name, 1);
        localNode4.point = nodes[4];
        localNode4.cout = 1;
        nodeList.Add(localNode4);


        Node localNode5 = new Node(nodes[5].name, 1);
        localNode5.point = nodes[5];
        localNode5.cout = 1;
        localNode5.heuristique = 1;
        nodeList.Add(localNode5);


        Node localNode6 = new Node(nodes[6].name, 1);
        localNode6.point = nodes[6];
        localNode6.cout = 1;
        nodeList.Add(localNode6);


        Node localNode7 = new Node(nodes[7].name, 1);
        localNode7.point = nodes[7];
        localNode7.cout = 1;
        nodeList.Add(localNode7);


        Node localNode8 = new Node(nodes[8].name, 1);
        localNode8.point = nodes[8];
        localNode8.cout = 1;
        nodeList.Add(localNode8);


        Node localNode9 = new Node(nodes[9].name, 1);
        localNode9.point = nodes[9];
        localNode9.cout = 1;
        nodeList.Add(localNode9);


        Node localNode10 = new Node(nodes[10].name, 1);
        localNode10.point = nodes[10];
        localNode10.cout = 1;
        nodeList.Add(localNode10);

        Node localNode11 = new Node(nodes[11].name, 1);
        localNode11.point = nodes[11];
        localNode1.cout = 1;
        nodeList.Add(localNode11);

        Node localNode12 = new Node(nodes[12].name, 1);
        localNode12.point = nodes[12];
        localNode12.cout = 1;
        nodeList.Add(localNode12);

        Node localNode13 = new Node(nodes[13].name, 1);
        localNode13.point = nodes[13];
        localNode13.cout = 1;
        nodeList.Add(localNode13);

        Node localNode14 = new Node(nodes[14].name, 1);
        localNode14.point = nodes[14];
        localNode14.cout = 1;
        nodeList.Add(localNode14);

        localNode0.destinations = new Destination[] { new Destination(localNode1, 1) };
        localNode1.destinations = new Destination[] { new Destination(localNode2, 1) };
        localNode2.destinations = new Destination[] { new Destination(localNode3, 1) };
        localNode3.destinations = new Destination[] { new Destination(localNode4, 1) };
        localNode4.destinations = new Destination[] { new Destination(localNode5, 1) };
        localNode5.destinations = new Destination[] { new Destination(localNode6, 1) };
        localNode6.destinations = new Destination[] { new Destination(localNode7, 1) };
        localNode7.destinations = new Destination[] { new Destination(localNode8, 1) };
        localNode8.destinations = new Destination[] { new Destination(localNode9, 1) };
        localNode9.destinations = new Destination[] { new Destination(localNode10, 1) };
        localNode10.destinations = new Destination[] { new Destination(localNode11, 1) };
        localNode11.destinations = new Destination[] { new Destination(localNode12, 1) };
        localNode12.destinations = new Destination[] { new Destination(localNode13, 1) };
        localNode13.destinations = new Destination[] { new Destination(localNode14, 1) };
        localNode14.destinations = new Destination[] { new Destination(localNode0, 1) };

        List<Node> close = new List<Node> { };
        List<Node> open = new List<Node> { };
        nodes = IaManager2.astar(nodeList[0], nodeList[14], new List<Node>(), new List<Node>());



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

         }*/

        if (RightVelocity().magnitude > maxStickyVelocity)
        {
            driftFactor = driftFactorSlippy;
            skidEffect.emissionRate = 15;

        }
        if (currentNode == 7 || currentNode == 5 || currentNode == 9 || currentNode == 8)
        {
            speedForce = 10;
            //bot.AddForce(transform.up * speedForce);
            boostEffect.emissionRate = 25;
        }
        else
        {
            boostEffect.emissionRate = 0;
            speedForce = 6;

        }
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

        float targetRot = Vector2.Angle(Vector2.right, direction);
        if (direction.y < 0.0f)
        {
            targetRot = -targetRot;
        }

        bot.transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.localEulerAngles.z, targetRot - 90, 0.1f));

    }

    private void CheckDistance()
    {
        Debug.Log("CURRENT   " + currentNode);

        if (Vector3.Distance(bot.transform.position, nodes[currentNode].position) < 0.5f)
        {
            speedForce = 2;
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }

    }








}
