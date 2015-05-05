using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Text label;

	public int id = 0;
	public string player_name = "None";
	public int level = 0;
	public int money = 0;
	private DatabaseConnection db;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadData(){
		DataStructs.playerData tempData = db.getAvatarInfo (id);

		name = "Player_" + tempData.name;
		player_name = tempData.name;
		level = tempData.level;
		money = tempData.money;
		transform.position = new Vector3 (tempData.posx, tempData.posy, tempData.posz);

		updateLabel ();
	}

	public void updateLabel(){
		string newText = player_name + "\nLvl " + level + "\n" + money + " monies";
		label.text = newText;
	}
	
	public void setDB(DatabaseConnection database) {
		db = database;
	}
}
