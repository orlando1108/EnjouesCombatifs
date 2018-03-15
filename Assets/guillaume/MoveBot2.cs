using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MoveBot2 : MonoBehaviour
{
    //public ParticleSystem exhaust;
    //public ParticleSystem skidEffect;
   // public ParticleSystem boostEffect;
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
    //AudioSource motorSound;
    Rigidbody2D bot;
    Vector3 direction;
    Vector3 target;
    private List<Node> nodeList = new List<Node> { };


    void Start()
    {
        //exhaust.emissionRate = 0;
        //skidEffect.emissionRate = 0;
        //boostEffect.emissionRate = 0;
        //motorSound = GetComponent<AudioSource>();
        //motorSound.Play();
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
        localNode11.cout = 1;
        nodeList.Add(localNode11);


        
        localNode0.destinations = new Destination[] { new Destination(localNode1, 1) };
       /* localNode0.destinations = new Destination[] { new Destination(localNode11, 1) };


        localNode10.destinations = new Destination[] { new Destination(localNode9, 1) };
        localNode11.destinations = new Destination[] { new Destination(localNode9, 1) };*/


        localNode1.destinations = new Destination[] { new Destination(localNode2, 1) };
        localNode2.destinations = new Destination[] { new Destination(localNode3, 1) };
        localNode3.destinations = new Destination[] { new Destination(localNode4, 1) };

        localNode4.destinations = new Destination[] { new Destination(localNode5, 2), new Destination(localNode6, 3) };
        //localNode4.destinations = new Destination[] { new Destination(localNode6, 1) };

        localNode5.destinations = new Destination[] { new Destination(localNode7, 1) };
        localNode6.destinations = new Destination[] { new Destination(localNode7, 1) };
        localNode7.destinations = new Destination[] { new Destination(localNode8, 1) };
        localNode8.destinations = new Destination[] { new Destination(localNode9, 1) };
        localNode9.destinations = new Destination[] { new Destination(localNode10, 1) };
        localNode10.destinations = new Destination[] { new Destination(localNode11, 1) };
        localNode11.destinations = new Destination[] { new Destination(localNode0, 1) };

        List<Node> close = new List<Node> { };
        List<Node> open = new List<Node> { };
        nodes = IaManager2.astar(nodeList[10], nodeList[9], new List<Node>(), new List<Node>());

        

    }
    
    void Update()
    {
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float driftFactor = driftFactorSticky;
        float pitch = bot.velocity.magnitude / audioClipSpeed;

        //skidEffect.emissionRate = 0;
        //exhaust.emissionRate = 2;
        SteerTowardsTarget();
        CheckDistance();

        // ACCELERATE
         direction = (nodes[currentNode].position - bot.transform.position);
         bot.AddForce(direction.normalized);
         //exhaust.emissionRate = 15;

        //ROTATE
        //float targetRot = Vector2.Angle(bot.transform.up, direction);
        //Debug.Log("ANGLE  " + targetRot);
        //bot.transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, targetRot, 0.05f));

        //MOTOR SOUND
        //motorSound.pitch = Mathf.Clamp(pitch, 0.5f, 3f);

       // bot.velocity = ForwardVelocity() + RightVelocity() * driftFactorSlippy;

        /*if (RightVelocity().magnitude > maxStickyVelocity)
        {
            driftFactor = driftFactorSlippy;
            skidEffect.emissionRate = 15;

        }*/

        if (Input.GetButton("Brakes"))
        {
            bot.AddForce(transform.up * -speedForce / 2);
            //skidEffect.emissionRate = 15;

        }
        if (Input.GetButton("Boost"))
        {
            // car.AddForce(transform.up * speedForce);
            speedForce = 10;
            //boostEffect.emissionRate = 25;
        }
        else
        {
            //boostEffect.emissionRate = 0;
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
        //Debug.Log("ROT  " + targetRot);


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
