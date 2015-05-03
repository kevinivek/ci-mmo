using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


public class test_database : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string connString = "server=server;uid=user;pwd=pwd;";
		SqlConnection conn = new SqlConnection(connString);
		conn.Open();
		conn.Close();	
	}

}