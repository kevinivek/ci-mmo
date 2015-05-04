using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DatabaseConnection
{
	string connString = "";
    MySqlConnection con = null;
	MySqlCommand cmd = null;
	MySqlDataReader rdr = null;

	public DatabaseConnection() {
		openConnection ();
	}
	
	public void openConnection(){
		try
		{
			con = new MySqlConnection(getConnString ());
			con.Open();
			Debug.Log("DB: Connection State: " + con.State);
		}
		catch (Exception ex)
		{
			Debug.Log(ex.ToString());
		}
	}

	public void closeConnection(){
		Debug.Log("DB: Closing db connection");
		if (con != null) {
			if (con.State.ToString() != "Closed")
				con.Close();
			con.Dispose();
		}
	}

	public String getConnString() {
		FileInfo sourceFile;
		sourceFile = new FileInfo ("Assets/scripts/database_connection_config.txt");
		StreamReader reader = sourceFile.OpenText ();
		
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

	
	// Read all entries from the table
	public T getValue<T>(String key, String value, String target)
	{
		String query = string.Empty;

		T returnValue = default(T);

		try
		{
			query = "SELECT " + target + " FROM object WHERE " + key + "=" + value + ";";
			Debug.Log("DB: Query(" + query + ")");
			if (con.State.ToString() != "Open")
				con.Open();
			using (con)
			{
				using (cmd = new MySqlCommand(query, con))
				{
					rdr = cmd.ExecuteReader();
					if (rdr.HasRows) {
						while (rdr.Read()) {
						rdr.Read();
							switch(typeof(T).Name) {
								case "Int32":
									returnValue = (T)(object) int.Parse (rdr[target].ToString ());
									break;
								case "Float":
									returnValue = (T)(object) float.Parse (rdr[target].ToString ());
									break;
								case "String":
									returnValue = (T)(object) rdr[target].ToString ();
									break;
							}
							//Debug.Log(target + "(" + typeof(T).Name.ToString() + "): " + returnValue); 
						}
					rdr.Dispose();
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.ToString());
		}

		return returnValue;

	}
	
}