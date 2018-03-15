using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        /*CarLapCounter carLapCounter = other.gameObject.GetComponent<CarLapCounter>();
		if (carLapCounter) {
			Debug.Log("lap trigger " + gameObject.name);
			carLapCounter.OnLapTrigger(this);
		}*/
        if (other.gameObject.tag == "carPlayer")
        {
            GameObject.Find("Win").GetComponent<TrackLapTrigger>().verif = true;
        }
    }
}
