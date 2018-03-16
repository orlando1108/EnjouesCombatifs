using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent : MonoBehaviour {

    public ParticleSystem sparkEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        Rigidbody2D car = this.GetComponent<Rigidbody2D>();
      //  ParticleSystem sparkEffect = this.GetComponent<ParticleSystem>();

        sparkEffect.transform.position = contact.point;
        sparkEffect.enableEmission = true;
        car.AddForce(contact.normal * contact.relativeVelocity.magnitude * 0.55f, ForceMode2D.Impulse);

        StartCoroutine(StopSparkles());



    }

    IEnumerator  StopSparkles()
    {
        yield return new WaitForSeconds(.2f);
        sparkEffect.enableEmission = false;
    }
}
