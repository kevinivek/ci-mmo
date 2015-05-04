using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using MySql.Data.MySqlClient;


public class TestDatabase : MonoBehaviour {

	DatabaseConnection db;

	// Use this for initialization
	void Start () {
		db = new DatabaseConnection ();

		int value = db.getValue<int> ("id", Convert.ToString(1), "height");

		transform.Translate (Vector2.up * value);
	}

	void OnApplicationQuit()
	{
		db.closeConnection ();
	}

}
