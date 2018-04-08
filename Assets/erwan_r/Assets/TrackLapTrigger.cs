using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrackLapTrigger : MonoBehaviour {
    
    // mise à jour des tours + affichage du gagnant 
    public bool verif = false;
    
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

