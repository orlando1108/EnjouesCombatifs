using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent : MonoBehaviour {
    
    // private GameObject particleObject;

    //désactivé les particules car ca fonctionne à moitié, au bout d'un moment elles ne se desactivent plus.

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        Rigidbody2D car = this.GetComponent<Rigidbody2D>();
      //  particleObject = SparkPool.activateSparkParticle(contact.point);
        car.AddForce(contact.normal * contact.relativeVelocity.magnitude * 0.4f, ForceMode2D.Impulse);
        StartCoroutine(StopSparkles());
    }

    IEnumerator  StopSparkles()
    {
        yield return new WaitForSeconds(.3f);
      //  SparkPool.Destroy(particleObject);
    }
}
