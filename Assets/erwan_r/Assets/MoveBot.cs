using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBot : MonoBehaviour
{
    public ParticleSystem exhaust;
    public ParticleSystem skidEffect;
    public ParticleSystem boostEffect;
    public List<Transform> nodes;
    private List<Transform> FindedNodes;
    float speedForce;
    float torqueForce;
    float driftFactorSticky;
    float driftFactorSlippy ;
    float maxStickyVelocity;
    float minStickyVelocity;
    float audioClipSpeed;

    private int currentNode = 0;
    float targetRot;
    AudioSource motorSound;
    Rigidbody2D bot;
    Vector3 direction;
    private List<Node> nodeList = new List<Node> { };
    Vector3 target;

    void Start()
    {
        bot = GetComponent<Rigidbody2D>();

        if (GameManager.isBot)
        {
            speedForce = 9f;
            torqueForce = -200f;
            driftFactorSticky = 1.9f;
            driftFactorSlippy = 0.8f;
            maxStickyVelocity = 3f;
            minStickyVelocity = 1.3f;
            audioClipSpeed = 6f;
            direction = (nodes[currentNode].position - bot.transform.position);

            Node localNode1a = new Node(nodes[0].name, 1);
            localNode1a.point = nodes[0];
            localNode1a.isActive = true;
            localNode1a.cout = 1;
            nodeList.Add(localNode1a);

            Node localNode1b = new Node(nodes[1].name, 1);
            localNode1b.point = nodes[1];
            localNode1b.isActive = true;
            localNode1b.cout = 1;
            nodeList.Add(localNode1b);

            Node localNode2a = new Node(nodes[2].name, 1);
            localNode2a.point = nodes[2];
            localNode2a.isActive = true;
            localNode2a.cout = 1;
            nodeList.Add(localNode2a);

            Node localNode2b = new Node(nodes[3].name, 1);
            localNode2b.point = nodes[3];
            localNode2b.isActive = true;
            localNode2b.cout = 1;
            nodeList.Add(localNode2b);

            Node localNode3a = new Node(nodes[4].name, 1);
            localNode3a.point = nodes[4];
            localNode3a.cout = 1;
            localNode3a.isActive = true;
            nodeList.Add(localNode3a);

            Node localNode3b = new Node(nodes[5].name, 1);
            localNode3b.point = nodes[5];
            localNode3b.cout = 1;
            localNode3b.isActive = true;
            localNode3b.heuristique = 1;
            nodeList.Add(localNode3b);

            Node localNode4a = new Node(nodes[6].name, 1);
            localNode4a.point = nodes[6];
            localNode4a.cout = 1;
            localNode4a.isActive = true;
            nodeList.Add(localNode4a);

            Node localNode4b = new Node(nodes[7].name, 1);
            localNode4b.point = nodes[7];
            localNode4b.isActive = true;
            localNode4b.cout = 1;
            nodeList.Add(localNode4b);

            Node localNode5a = new Node(nodes[8].name, 1);
            localNode5a.point = nodes[8];
            localNode5a.isActive = true;
            localNode5a.cout = 1;
            nodeList.Add(localNode5a);

            Node localNode5b = new Node(nodes[9].name, 1);
            localNode5b.point = nodes[9];
            localNode5b.isActive = true;
            localNode5b.cout = 1;
            nodeList.Add(localNode5b);

            Node localNode6a = new Node(nodes[10].name, 1);
            localNode6a.point = nodes[10];
            localNode6a.isActive = true;
            localNode6a.cout = 1;
            nodeList.Add(localNode6a);

            Node localNode6b = new Node(nodes[11].name, 1);
            localNode6b.point = nodes[11];
            localNode6b.isActive = true;
            localNode6b.cout = 1;
            nodeList.Add(localNode6b);

            Node localNode7a = new Node(nodes[12].name, 1);
            localNode7a.point = nodes[12];
            localNode7a.isActive = true;
            localNode7a.cout = 1;
            nodeList.Add(localNode7a);

            Node localNode7b = new Node(nodes[13].name, 1);
            localNode7b.point = nodes[13];
            localNode7b.isActive = true;
            localNode7b.cout = 1;
            nodeList.Add(localNode7b);

            Node localNode8a = new Node(nodes[14].name, 1);
            localNode8a.point = nodes[14];
            localNode8a.isActive = true;
            localNode8a.cout = 1;
            nodeList.Add(localNode8a);

            Node localNode8b = new Node(nodes[15].name, 1);
            localNode8b.point = nodes[15];
            localNode8b.isActive = true;
            localNode8b.cout = 1;
            nodeList.Add(localNode8b);

            Node localNode9a = new Node(nodes[16].name, 1);
            localNode9a.point = nodes[16];
            localNode9a.isActive = true;
            localNode9a.cout = 1;
            nodeList.Add(localNode9a);

            Node localNode9b = new Node(nodes[17].name, 1);
            localNode9b.point = nodes[17];
            localNode9b.isActive = true;
            localNode9b.cout = 1;
            nodeList.Add(localNode9b);

            Node localNode10a = new Node(nodes[18].name, 1);
            localNode10a.point = nodes[18];
            localNode10a.isActive = true;
            localNode10a.cout = 1;
            nodeList.Add(localNode10a);

            Node localNode10b = new Node(nodes[19].name, 1);
            localNode10b.point = nodes[19];
            localNode10b.isActive = true;
            localNode10b.cout = 1;
            nodeList.Add(localNode10b);

            Node localNode11a = new Node(nodes[20].name, 1);
            localNode11a.point = nodes[20];
            localNode11a.isActive = true;
            localNode11a.cout = 1;
            nodeList.Add(localNode11a);

            Node localNode11b = new Node(nodes[21].name, 1);
            localNode11b.point = nodes[21];
            localNode11b.isActive = true;
            localNode11b.cout = 1;
            nodeList.Add(localNode11b);

            Node localNode12a = new Node(nodes[22].name, 1);
            localNode12a.point = nodes[22];
            localNode12a.isActive = true;
            localNode12a.cout = 1;
            nodeList.Add(localNode12a);

            Node localNode12b = new Node(nodes[23].name, 1);
            localNode12b.point = nodes[23];
            localNode12b.isActive = true;
            localNode12b.cout = 1;
            nodeList.Add(localNode12b);

            Node localNode13a = new Node(nodes[24].name, 1);
            localNode13a.point = nodes[24];
            localNode13a.isActive = true;
            localNode13a.cout = 1;
            nodeList.Add(localNode13a);

            Node localNode13b = new Node(nodes[25].name, 1);
            localNode13b.point = nodes[25];
            localNode13b.isActive = true;
            localNode13b.cout = 1;
            nodeList.Add(localNode13b);

            Node localNode14a = new Node(nodes[26].name, 1);
            localNode14a.point = nodes[26];
            localNode14a.isActive = true;
            localNode14a.cout = 1;
            nodeList.Add(localNode14a);

            Node localNode14b = new Node(nodes[27].name, 1);
            localNode14b.point = nodes[27];
            localNode14b.isActive = true;
            localNode14b.cout = 1;
            nodeList.Add(localNode14b);

            Node localNode15a = new Node(nodes[28].name, 1);
            localNode15a.point = nodes[28];
            localNode15a.isActive = true;
            localNode15a.cout = 1;
            nodeList.Add(localNode15a);

            Node localNode15b = new Node(nodes[29].name, 1);
            localNode15b.point = nodes[29];
            localNode15b.isActive = true;
            localNode15b.cout = 1;
            nodeList.Add(localNode15b);


            localNode1a.destinations = new Destination[] { new Destination(localNode2a, 2), new Destination(localNode2b, 1) };
            localNode1b.destinations = new Destination[] { new Destination(localNode2a, 2), new Destination(localNode2b, 1) };

            localNode2a.destinations = new Destination[] { new Destination(localNode3a, 2), new Destination(localNode3b, 1) };
            localNode2b.destinations = new Destination[] { new Destination(localNode3a, 2), new Destination(localNode3b, 1) };

            localNode3a.destinations = new Destination[] { new Destination(localNode4a, 2), new Destination(localNode4b, 1) };
            localNode3b.destinations = new Destination[] { new Destination(localNode4a, 2), new Destination(localNode4b, 1) };

            localNode4a.destinations = new Destination[] { new Destination(localNode5a, 2), new Destination(localNode5b, 1) };
            localNode4b.destinations = new Destination[] { new Destination(localNode5a, 2), new Destination(localNode5b, 1) };

            localNode5a.destinations = new Destination[] { new Destination(localNode6a, 1), new Destination(localNode6b, 2) };
            localNode5b.destinations = new Destination[] { new Destination(localNode6a, 1), new Destination(localNode6b, 2) };

            localNode6a.destinations = new Destination[] { new Destination(localNode7a, 1), new Destination(localNode7b, 2) };
            localNode6b.destinations = new Destination[] { new Destination(localNode7a, 1), new Destination(localNode7b, 2) };

            localNode7a.destinations = new Destination[] { new Destination(localNode8a, 2), new Destination(localNode8b, 1) };
            localNode7b.destinations = new Destination[] { new Destination(localNode8a, 2), new Destination(localNode8b, 1) };

            localNode8a.destinations = new Destination[] { new Destination(localNode9a, 2), new Destination(localNode9b, 1) };
            localNode8b.destinations = new Destination[] { new Destination(localNode9a, 2), new Destination(localNode9b, 1) };

            localNode9a.destinations = new Destination[] { new Destination(localNode10a, 2), new Destination(localNode10b, 1) };
            localNode9b.destinations = new Destination[] { new Destination(localNode10a, 2), new Destination(localNode10b, 1) };

            localNode10a.destinations = new Destination[] { new Destination(localNode11a, 2), new Destination(localNode11b, 1) };
            localNode10b.destinations = new Destination[] { new Destination(localNode11a, 2), new Destination(localNode11b, 1) };

            localNode11a.destinations = new Destination[] { new Destination(localNode12a, 2), new Destination(localNode12b, 1) };
            localNode11b.destinations = new Destination[] { new Destination(localNode12a, 2), new Destination(localNode12b, 1) };

            localNode12a.destinations = new Destination[] { new Destination(localNode13a, 1), new Destination(localNode13b, 2) };
            localNode12b.destinations = new Destination[] { new Destination(localNode13a, 1), new Destination(localNode13b, 2) };

            localNode13a.destinations = new Destination[] { new Destination(localNode14a, 1), new Destination(localNode14b, 2) };
            localNode13b.destinations = new Destination[] { new Destination(localNode14a, 1), new Destination(localNode14b, 2) };

            localNode14a.destinations = new Destination[] { new Destination(localNode15a, 2), new Destination(localNode15b, 1) };
            localNode14b.destinations = new Destination[] { new Destination(localNode15a, 2), new Destination(localNode15b, 1) };

            localNode15a.destinations = new Destination[] { new Destination(localNode1a, 1), new Destination(localNode1b, 2) };
            localNode15b.destinations = new Destination[] { new Destination(localNode1a, 1), new Destination(localNode1b, 2) };

            List<Node> close = new List<Node> { };
            List<Node> open = new List<Node> { };
            randomObstacles(nodeList);
            generateObstacles();
            nodes = IaManager2.astar(nodeList[0], nodeList[29], new List<Node>(), new List<Node>());



        }
        else
        {
            speedForce = 9f;
            torqueForce = -200f;
            driftFactorSticky = 0.9f;
            driftFactorSlippy = 0.8f;
            maxStickyVelocity = 3.3f;
            minStickyVelocity = 1.6f;
        }

        exhaust.emissionRate = 0;
        skidEffect.emissionRate = 0;
        boostEffect.emissionRate = 0;
        motorSound = GetComponent<AudioSource>();
        motorSound.Play();

    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        if (GameManager.isBot)
        {
            float driftFactor = driftFactorSticky;
            float pitch = bot.velocity.magnitude / audioClipSpeed;

            CheckDistance();
            SteerTowardsTarget();

            skidEffect.emissionRate = 0;
            exhaust.emissionRate = 2;

            // ACCELERATE
            bot.velocity = ForwardVelocity() + RightVelocity() * driftFactorSlippy;
            direction = (nodes[currentNode].position - bot.transform.position);
            bot.AddForce(direction.normalized * speedForce);
            exhaust.emissionRate = 15;
            
            //MOTOR SOUND
            motorSound.pitch = Mathf.Clamp(pitch, 0.5f, 3f);
            if (RightVelocity().magnitude > maxStickyVelocity)
            {
                driftFactor = driftFactorSlippy;
                skidEffect.emissionRate = 15;

            }
            if (currentNode == 0 || currentNode == 14 || currentNode == 9 || currentNode == 8)
            {
                speedForce = 12;
                //bot.AddForce(transform.up * speedForce);
                boostEffect.emissionRate = 25;
            }
            else
            {
                boostEffect.emissionRate = 0;
                speedForce = 9;

            }
            // always accelerate
            float velocity = bot.velocity.magnitude;
            // apply car movement
            /* bot.velocity = transform.up * velocity;
             bot.angularVelocity = 0.0f;*/

        }
        else
        {
            float driftFactor = driftFactorSticky;
            float pitch = bot.velocity.magnitude / audioClipSpeed;
            skidEffect.emissionRate = 0;
            exhaust.emissionRate = 2;
             

            motorSound.pitch = Mathf.Clamp(pitch, 0.5f, 3f);


            if (RightVelocity().magnitude > maxStickyVelocity)
            {
                driftFactor = driftFactorSlippy;
                skidEffect.emissionRate = 15;

            }

            bot.velocity = ForwardVelocity() + RightVelocity() * driftFactorSlippy;

            if (Input.GetButton("Accelerate2"))
            {
                bot.AddForce(transform.up * speedForce);
                exhaust.emissionRate = 15;

            }
            if (Input.GetButton("Brakes2"))
            {
                bot.AddForce(transform.up * -speedForce / 2);
                skidEffect.emissionRate = 15;

            }
            if (Input.GetButton("Boost2"))
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
            
            float tf = Mathf.Lerp(0, torqueForce, bot.velocity.magnitude / 5);
            bot.angularVelocity = Input.GetAxis("Horizontal2") * tf;
        }
        



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

         bot.transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.localEulerAngles.z, targetRot - 90, 0.2f));

    }

    private void CheckDistance()
    {
        

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

    private List<Node> randomObstacles(List<Node> entry)
    {
        System.Random rnd = new System.Random();
        int rand1 = rnd.Next(1, 30);
        int rand2 = rnd.Next(1, 30);
        while (rand1 - rand2 < 3 && rand1 - rand2 > -3)
        {
            rand2 = rnd.Next(1, 30);
        }

        entry[rand1].isActive = false;
        


        entry[rand2].isActive = false;
        return entry;
    }

    private void generateObstacles()
    {
        foreach(Node point in nodeList.FindAll(elem => elem.isActive == false)){
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = point.point.position;
           
        }
        foreach (Node point in nodeList.FindAll(elem => elem.isActive == true))
        {
            point.point.GetComponent<CircleCollider2D>().isTrigger = true;
        }
            


    }








}
