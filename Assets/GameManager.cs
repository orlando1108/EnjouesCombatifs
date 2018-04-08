using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Gestion de demmarrage du jeu + arret du jeu à la fin des tours
    
    TextMesh textInfos;
    public AudioClip startSound;
    public GameObject CanvasGame;
    private bool started;
    public static bool isBot = true;

	void Start () {
        CanvasGame.SetActive(false);
        textInfos = GetComponent<TextMesh>();
        textInfos.GetComponent<Animator>().enabled = false;
        textInfos.text = "Press X to Start ! \n manette ( A,B, joystick, R1 ) \n clavier ( R-arrow, L-arrow, ctrl left, space ) ";
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
		
        if((Input.GetButtonDown("Start") || Input.GetKey(KeyCode.Space)) && !started)
        {
            StartCoroutine(Counter());
            started = true;
        }
	}

    private IEnumerator Counter()
    {
        //yield return new WaitForSeconds(1);
        textInfos.GetComponent<Animator>().enabled = true;
        textInfos.GetComponent<MeshRenderer>().enabled = true;

        for(int i =3; i>=0; i--)
        {
            
            textInfos.text = "         " + i;
            GetComponent<AudioSource>().PlayOneShot(startSound);
            yield return new WaitForSeconds(1);
        }

        textInfos.text = "GO MANY !";
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
