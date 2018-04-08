using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{

    public Transform target;
    private float smoothSpeed = 0.32f;

    /*
    void FixedUpdate()
    {
        Vector2 finalPosition = target.position - this.transform.position;
        
        this.transform.position = new Vector3(target.position.x , target.position.y, this.transform.position.z);
                

        this.transform.LookAt(target);

        

    }*/


    //private float smoothSpeed = 0.85f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 specificVector;

    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }
    // public Transform target;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            /* Vector3 point = cam.WorldToViewportPoint(target.position);
             Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
             Vector3 destination = transform.position + delta;
             transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);*/

            // specificVector = Vector3(transform.position.x, transform.position.y, cameraObj.transform.position.z);
            specificVector = new Vector3(target.position.x, target.position.y, cam.transform.position.z);
            cam.transform.position = Vector3.Lerp(cam.transform.position, specificVector, 0.1f);
        }

    }

}
