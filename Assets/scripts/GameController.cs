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
	public string emailInput = "none";
	public string passwordInput = "none";
	public GameObject loginFailedText;
	public GameObject playerPrefab;
	public List<GameObject> players;

	DatabaseConnection db;

	// Use this for initialization
	void Start () {
		loginWindow.SetActive (true); 
		db = new DatabaseConnection ();
	}

	public void loginPlayer() {
		if (db.login (emailInput, passwordInput) > 0) {
			loginWindow.SetActive (false);
			setupPlayers ();
		} else {
			loginFailedText.SetActive (true);
		}
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

	public void setEmailInput(string input) {
		emailInput = input;
	}

	public void setPasswordInput(string input) {
		passwordInput = input;
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
