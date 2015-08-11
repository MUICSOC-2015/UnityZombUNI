﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	private BoardManager boardScript;
	private Camera cam;
	private string levelName = "starting";

	// Use this for initialization
	void Awake ()  //creating the board
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		boardScript = GetComponent<BoardManager> ();
		cam = GetComponent<Camera> ();
		InitGame ();
	}
	void InitGame() //call boardScript to set up the game
	{
		boardScript.SetupScene (levelName);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
