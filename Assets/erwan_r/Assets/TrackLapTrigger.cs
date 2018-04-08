using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrackLapTrigger : MonoBehaviour {

    // next trigger in the lap
    //public TrackLapTrigger next;
    public bool verif = false;

	// when an object enters this trigger
	void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "carPlayer" && verif)
        {
            GameObject.Find("Canvas").GetComponent<UIscript>().MajLaps();
            if (GameObject.Find("Canvas").GetComponent<UIscript>().Laps == 0)
            {
                GameObject.Find("Canvas").GetComponent<UIscript>().txtLaps.text = "WINNER : " + other.gameObject.tag;
                other.gameObject.GetComponent<Animator>().enabled = true;
            }
            
            verif = false;

        }
	}
}

