using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCar : MonoBehaviour {

    public KeyCode turnRight;
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

    private Vector3 targetAngle;
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

      /*  this.transform.eulerAngles += new Vector3(0, 0, Input.GetAxis("Horizontal"));
        if (Input.GetAxis("Horizontal") == 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(this.transform.eulerAngles.z, 0, 0.02f));
        }*/

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
      

    }
}
