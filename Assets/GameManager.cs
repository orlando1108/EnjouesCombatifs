using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    TextMesh textInfos;
    public AudioClip startSound;
    public GameObject CanvasGame;
    private bool started;

	void Start () {
        CanvasGame.SetActive(false);
        textInfos = GetComponent<TextMesh>();
        textInfos.text = "Press A to Start !";
        StopGame();
        started = false;
	}

    public void StopGame()
    {
        GameObject carBot;
        GameObject carPlayer;
        carBot = GameObject.FindGameObjectWithTag("carBot");
        carPlayer = GameObject.FindGameObjectWithTag("carPlayer");

        carBot.GetComponent<MoveBot>().enabled = false;
        carPlayer.GetComponent<MoveCar>().enabled = false;

        carPlayer.GetComponent<AudioSource>().Stop();
        carBot.GetComponent<AudioSource>().Stop();

    }

    // Update is called once per frame
    void Update () {
		
        if(Input.GetButtonDown("Start") && !started)
        {
            StartCoroutine(Counter());
            started = true;
        }
	}

    private IEnumerator Counter()
    {
        textInfos.GetComponent<Animator>().enabled = false;
        textInfos.GetComponent<MeshRenderer>().enabled = true;

        for(int i =3; i>=0; i--)
        {
            yield return new WaitForSeconds(1);
            textInfos.text = "         " + i;
            GetComponent<AudioSource>().PlayOneShot(startSound);
        }

        textInfos.text = "GO MANOLO !";
        StartGame();
        yield return new WaitForSeconds(1);
        textInfos.text = "";
        CanvasGame.SetActive(true);
    }

    private void StartGame()
    {
        GameObject carBot;
        GameObject carPlayer;
        carBot = GameObject.FindGameObjectWithTag("carBot");
        carPlayer = GameObject.FindGameObjectWithTag("carPlayer");

        carBot.GetComponent<MoveBot>().enabled = true;
        carPlayer.GetComponent<MoveCar>().enabled = true;

        carPlayer.GetComponent<AudioSource>().Play();
        carBot.GetComponent<AudioSource>().Play();
    }
    
}
