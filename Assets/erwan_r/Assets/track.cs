using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    // gestion de la camera avec effet smooth léger
    public Transform target;
    private float smoothSpeed = 0.32f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 specificVector;

    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }
    void FixedUpdate()
    {
        if (target)
        {
            specificVector = new Vector3(target.position.x, target.position.y, cam.transform.position.z);
            cam.transform.position = Vector3.Lerp(cam.transform.position, specificVector, 0.1f);
        }

    }

}
