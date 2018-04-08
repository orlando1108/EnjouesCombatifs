using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        // permet d'empecher le decompte de tour si il y a une marche arriere en forcant le passage dans un deuxieme collider au milieu de la piste
        // tant que ce collider n'est pas traversé il n'y a pas de decompte
        if (other.gameObject.tag == "carPlayer")
        {
            GameObject.Find("Win").GetComponent<TrackLapTrigger>().verif = true;
        }
    }
}
