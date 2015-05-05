using UnityEngine;
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
	public InputField userID;
	public GameObject playerPrefab;
	public GameObject player;

	DatabaseConnection db;

	// Use this for initialization
	void Start () {
		loginWindow.SetActive (true); 
	}

	public void loginPlayer() {
		loginWindow.SetActive (false);

		setupPlayers ();

		player = Instantiate (playerPrefab);
		player.GetComponent<PlayerController> ().id = int.Parse(userID.text);
		player.GetComponent<PlayerController> ().loadData ();
	}

	public void setupPlayers(){

	}
	
	public void testConnect() {
		db = new DatabaseConnection ();
		int value = db.getValue<int> ("id", Convert.ToString(1), "height");
		transform.Translate (Vector2.up * value);
	}

	void OnApplicationQuit()
	{
		db.closeConnection ();
	}

}
