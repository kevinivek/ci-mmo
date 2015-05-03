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
	}

	void OnApplicationQuit()
	{
		db.closeConnection ();
	}

}
