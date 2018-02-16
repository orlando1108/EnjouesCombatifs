using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
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
    private Vector2 direction;

    // Use this for initialization
    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        speedX = 0.1f;
        speedY = 0.1f;
        speedFrame = 0;
        speed = 0.02f;
        position = new Vector2(transform.localPosition.x, transform.localPosition.y);
        acceleration = 0f;
        //transform.Rotate(Vector3.forward, 90);


    }

    // Update is called once per frame
    void Update()
    {

        Vector3 currentAngle = transform.rotation.eulerAngles;
        float rotation = currentAngle.z;



        /*
        if (Input.GetKey(right))
        {
            speedFrame++;
            if (speedFrame == 20)
            {
                currentSpriteId = (currentSpriteId + 1) % cavernManList.Length;
                spRenderer.sprite = cavernManList[currentSpriteId];
                speedFrame = 0;
            }


        }*/
        if (Input.GetKey(mForward))
        {
            speed += 0.0001f;
            //position.x += speedX;
            /* transform.position = new Vector2(position.x, position.y);
             transform.position = new Vector2(direction.x, direction.y);*/
            //transform.position = new Vector2(direction.x += speed, direction.y += speed);
            transform.position = transform.position + (transform.rotation * Vector3.up * speed);


        }
        if (Input.GetKey(mBack))
        {
            speed -= 0.05f;
            //transform.position = new Vector2(position.x, position.y);*/
        }
        if (Input.GetKey(turnRight))
        {
            transform.Rotate(Vector3.back * 2);
           // print(transform.euler);
            //direction = new Vector2(Mathf.Sin(Mathf.Deg2Rad * transform.rotation) , Mathf.Cos(Mathf.Deg2Rad * ));
            
        }
        if (Input.GetKey(turnLeft))
        {
            transform.Rotate(Vector3.forward * 2);
            direction = new Vector2(Mathf.Sin(Mathf.Deg2Rad * transform.rotation.x), Mathf.Cos(Mathf.Deg2Rad * transform.rotation.y));
        }
       /* if (Input.GetKey(up))
        {
            position.y += speedY;
            transform.position = new Vector2(position.x, position.y);
        }
        if (Input.GetKey(down))
        {
            position.y -= speedY;
            transform.position = new Vector2(position.x, position.y);
        }*/


    }
}
