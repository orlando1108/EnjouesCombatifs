using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIscript : MonoBehaviour {

    public Text txtLaps;
    public Text txtTime;
    public int Laps = 10;
    float startTime;

	void Start () {
        txtLaps.text = "Laps : " + Laps;
        startTime = Time.time;

    }

    //calcul + affichage du temps ecoulé 
	void Update () {

        if (Laps > 0)
        {
            int min = Mathf.FloorToInt((Time.time - startTime) / 60);
            int sec = Mathf.FloorToInt((Time.time - startTime) - min * 60);
            txtTime.text = "Time : " + string.Format("{0:0}:{1:00}", min, sec);

        }
            
	}

    //affichage des tours et arret du jeu
    public void MajLaps()
    {
        Laps -= 1;
        txtLaps.text = "Laps : " + Laps;

        if (Laps == 0)
        {
            
            GameObject.Find("TextInfos").GetComponent<GameManager>().StopGame();
        }
    }
}
