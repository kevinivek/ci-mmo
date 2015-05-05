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

	/*
	// Read all entries from the table
	public T getValue<T>(String key, String value, String target, String table)
	{
		String query = string.Empty;

		T returnValue = default(T);

		try
		{
			query = "SELECT " + target + " FROM " + table + " WHERE " + key + "=" + value + ";";
			Debug.Log("DB: Query(" + query + ")");
			if (con.State.ToString() != "Open")
				con.Open();
			using (con)
			{
				using (cmd = new MySqlCommand(query, con))
				{
					rdr = cmd.ExecuteReader();
					if (rdr.HasRows) {
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
*/
	
	// Read all entries from specified avatar
	public DataStructs.playerData getAvatarInfo(int avatarID)
	{
		string query = string.Empty;
		DataStructs.playerData pdata = new DataStructs.playerData();

		try
		{
			query = "SELECT * FROM avatar where id=" + avatarID + ";";
			Debug.Log (query);
			if (con.State.ToString() != "Open")
				con.Open();
			using (con)
			{
				using (cmd = new MySqlCommand(query, con))
				{
					rdr = cmd.ExecuteReader();
					if (rdr.HasRows) {
						rdr.Read();
						pdata.id = int.Parse(rdr["id"].ToString());
						pdata.acc_id = int.Parse(rdr["acc_id"].ToString());
						pdata.weapon = int.Parse(rdr["weapon"].ToString());
						pdata.helmet = int.Parse(rdr["helmet"].ToString());
						pdata.armor = int.Parse(rdr["armor"].ToString());
						pdata.vehicle = int.Parse(rdr["vehicle_id"].ToString());
						pdata.minion = int.Parse(rdr["minion_id"].ToString());
						pdata.currentQuest = int.Parse(rdr["curr_quest_id"].ToString());
						pdata.money = int.Parse(rdr["money"].ToString());
						pdata.level = int.Parse(rdr["a_level"].ToString());

						pdata.name = rdr["a_name"].ToString();

						pdata.posx = float.Parse(rdr["last_x"].ToString());
						pdata.posy = float.Parse(rdr["last_y"].ToString());
						pdata.posz = float.Parse(rdr["last_z"].ToString());
						rdr.Dispose();
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.ToString());
		}
		return pdata;
	}

	// Read all entries from all avatars
	public List<DataStructs.playerData> getAllAvatarsInfo()
	{
		string query = string.Empty;
		List<DataStructs.playerData> dataList = new List<DataStructs.playerData>();

		try
		{
			query = "SELECT * FROM avatar;";
			Debug.Log (query);
			if (con.State.ToString() != "Open")
				con.Open();
			using (con)
			{
				using (cmd = new MySqlCommand(query, con))
				{
					rdr = cmd.ExecuteReader();
					if (rdr.HasRows) {
						while (rdr.Read()) {
							DataStructs.playerData pdata = new DataStructs.playerData();
							pdata.id = int.Parse(rdr["id"].ToString());
							pdata.acc_id = int.Parse(rdr["acc_id"].ToString());
							pdata.weapon = int.Parse(rdr["weapon"].ToString());
							pdata.helmet = int.Parse(rdr["helmet"].ToString());
							pdata.armor = int.Parse(rdr["armor"].ToString());
							pdata.vehicle = int.Parse(rdr["vehicle_id"].ToString());
							pdata.minion = int.Parse(rdr["minion_id"].ToString());
							pdata.currentQuest = int.Parse(rdr["curr_quest_id"].ToString());
							pdata.money = int.Parse(rdr["money"].ToString());
							pdata.level = int.Parse(rdr["a_level"].ToString());
							
							pdata.name = rdr["a_name"].ToString();
							
							pdata.posx = float.Parse(rdr["last_x"].ToString());
							pdata.posy = float.Parse(rdr["last_y"].ToString());
							pdata.posz = float.Parse(rdr["last_z"].ToString());
							dataList.Add(pdata);
							rdr.Dispose();
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.ToString());
		}
		return dataList;
	}

	// Get list of all avatar IDs
	public List<int> getAvatarIDs()
	{
		string query = string.Empty;
		List<int> idList = new List<int> ();
		
		try
		{
			query = "SELECT id FROM avatar;";
			Debug.Log (query);
			if (con.State.ToString() != "Open")
				con.Open();
			using (con)
			{
				using (cmd = new MySqlCommand(query, con))
				{
					rdr = cmd.ExecuteReader();
					if (rdr.HasRows) {
						while (rdr.Read()) {
							idList.Add(int.Parse(rdr["id"].ToString()));
						}
					}
					rdr.Dispose();
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.ToString());
		}
		return idList;
	}

	// Get list of all avatar IDs
	public List<int> getAccountIDs()
	{
		string query = string.Empty;
		List<int> idList = new List<int> ();

		try
		{
			query = "SELECT acc.id FROM avatar a inner join account acc on acc.id=a.acc_id group by acc.id;";
			Debug.Log (query);
			if (con.State.ToString() != "Open")
				con.Open();
			using (con)
			{
				using (cmd = new MySqlCommand(query, con))
				{
					rdr = cmd.ExecuteReader();
					if (rdr.HasRows) {
						while (rdr.Read()) {
							idList.Add(int.Parse(rdr["id"].ToString()));
						}
					}
					rdr.Dispose();
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.ToString());
		}
		return idList;
	}

	// Get list of all avatar IDs
	public int getFirstAvatarFromAcc(int accountId)
	{
		string query = string.Empty;
		int avatarId = 0;
		
		try
		{
			query = "SELECT a.id FROM avatar a inner join account acc on a.acc_id=acc.id where acc.id="+accountId+";";
			Debug.Log (query);
			if (con.State.ToString() != "Open")
				con.Open();
			using (con)
			{
				using (cmd = new MySqlCommand(query, con))
				{
					rdr = cmd.ExecuteReader();
					if (rdr.HasRows) {
						rdr.Read();
						avatarId = int.Parse(rdr["id"].ToString());
					}
					rdr.Dispose();
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.ToString());
		}
		return avatarId;
	}

	// Get list of all avatar IDs
	public int login(String email, String password)
	{
		string query = string.Empty;
		int accountID = 0;
		
		try
		{
			query = "SELECT acc.id FROM account acc where acc.email=" + email + " and acc.password=" + password + ";";
			Debug.Log (query);
			if (con.State.ToString() != "Open")
				con.Open();
			using (con)
			{
				using (cmd = new MySqlCommand(query, con))
				{
					rdr = cmd.ExecuteReader();
					if (rdr.HasRows) {
						rdr.Read();
						accountID = int.Parse(rdr["id"].ToString());
					}
					rdr.Dispose();
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.ToString());
		}
		return accountID;
	}

	
}