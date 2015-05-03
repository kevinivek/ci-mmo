using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;


public class test_database : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string connString = getConnString ();
		SqlConnection conn = new SqlConnection(connString);
		conn.Open();
		conn.Close();
	}

	public String getConnString() {

		FileInfo sourceFile;
		sourceFile = new FileInfo ("Assets/scripts/database_connection_config.txt");
		StreamReader reader = sourceFile.OpenText ();

		String connString = "";
		String line = "";
		while ((line = reader.ReadLine ()) != null) {
			if(line.Length<2)
				continue;
			if (line[0] == '#') {
				if(line[1] == '#') {
					continue;
				}
				String name = line.Substring(1,line.Length-1);
				String value = reader.ReadLine(); 
				connString += name + "=" + value + ";";
			}
		}

		reader.Close ();

		return connString;
	}

}
