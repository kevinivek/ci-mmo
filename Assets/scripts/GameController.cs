﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using MySql.Data.MySqlClient;


public class GameController : MonoBehaviour {

	public GameObject loginWindow;
	public InputField emailInput;
	public InputField passwordInput;
	public GameObject playerPrefab;
	public List<GameObject> players;

	DatabaseConnection db;

	// Use this for initialization
	void Start () {
		loginWindow.SetActive (true); 
		db = new DatabaseConnection ();
	}

	public void loginPlayer() {
		loginWindow.SetActive (false);
		setupPlayers ();
	}

	public void setupPlayers(){
		List<int> idList = db.getAccountIDs ();
		foreach (int id in idList) {
			GameObject player = Instantiate (playerPrefab);
			players.Add (player);
			PlayerController playerScript = player.GetComponent<PlayerController> ();
			playerScript.id = db.getFirstAvatarFromAcc(id);
			playerScript.setDB (db);
			playerScript.loadData ();

		}
	}
	
	public void testConnect() {
		//int value = db.getValue<int> ("id", Convert.ToString(1), "height");
		//transform.Translate (Vector2.up * value);
	}

	void OnApplicationQuit()
	{
		db.closeConnection ();
	}

}
