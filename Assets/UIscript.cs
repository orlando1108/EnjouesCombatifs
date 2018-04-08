using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIscript : MonoBehaviour {

    public Text txtLaps;
    public Text txtTime;
    public int Laps = 3;
    float startTime;

	void Start () {
        txtLaps.text = "Laps : " + Laps;
        startTime = Time.time;

    }
	
	// Update is called once per frame
	void Update () {
        
        int min = Mathf.FloorToInt((Time.time - startTime) / 60);
        int sec = Mathf.FloorToInt((Time.time - startTime) - min * 60);
        txtTime.text = "Time : " + string.Format("{0:0}:{1:00}", min, sec);
	}

    public void MajLaps()
    {
        Laps -= 1;
        txtLaps.text = "Laps : " + Laps;

        //end
        if(Laps == 0)
        {
            
            GameObject.Find("TextInfos").GetComponent<GameManager>().StopGame();
        }
    }
}
