using UnityEngine;
using System.Collections;

public class BasePlayerStat : MonoBehaviour {


	private string playerName;
	private int playerLevel;
	private int playerIntelligence;
	private int playerStrength;
	private int playerAgility;

	public string PlayerName
	{
		get{ return playerName;}
		set{ playerName = value;}
	}

	public int PlayerLevel
	{
		get{return playerLevel;}
		set{ playerLevel = value;}
	}
	public int PlayerStr
	{
		get{return playerStrength;}
		set{ playerStrength = value;}
	}
	public int PlayerAgi
	{
		get{return playerAgility;}
		set{ playerAgility = value;}
	}
	public int PlayerInt
	{
		get{return playerIntelligence;}
		set{ playerIntelligence = value;}
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
