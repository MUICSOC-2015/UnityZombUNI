using UnityEngine;
using System.Collections;

public class TurnBase_Manager : MonoBehaviour {

	public event TurnEnded enemyTurnEnded;
	public event TurnEnded playerTurnEnded;
	public delegate void TurnEnded(TurnInfo tI);
	
	
	// Use this for initialization
	void Start () {
		StartCoroutine(UpdateState());
		//Immediately start our loop
	}
	
	// Update is called once per frame
	IEnumerator UpdateState () {
		for(;;) {
			//This is short hand for infinity loop.  Same as while(true).   
			
			yield return StartCoroutine(PlayerTurn());
			//Start our player loop, and wait for it to finish
			//Before we continue.
			
			yield return StartCoroutine(EnemyTurn());
			//Do enemy loop, finish, restart loop
			
		}
	}
	
	IEnumerator PlayerTurn() {
		
		Transform target = null;
		//This could be placed in a higher scope for memory purposes.
		
		bool objectSelected = false;
		//have we selected an object.
		
		TurnInfo tI = new TurnInfo();
		//create a new turn info tracker.
		
		yield return StartCoroutine(SelectObject(target));
		//Wait until we find a target before continuing.
		
		if(target != null) 
			yield return StartCoroutine(Attack(target));
		//Wait until we find a target before continuing.
		
		
		Debug.Log("Attacked");
		
		if(playerTurnEnded != null) 
			playerTurnEnded(tI);
		
	}
	
	IEnumerator EnemyTurn() {
		
		Transform target = null;
		//This could be placed in a higher scope for memory purposes.
		
		bool objectSelected = false;
		//have we selected an object.
		
		TurnInfo tI = new TurnInfo();
		//create a new turn info tracker.
		
		yield return StartCoroutine(SelectObject(target));
		//Wait until we find a target before continuing.
		
		if(target != null) 
			yield return StartCoroutine(Attack(target));
		//Wait until we find a target before continuing.
		
		
		Debug.Log("Attacked1");
		
		if(enemyTurnEnded != null) 
			playerTurnEnded(tI);
		
	}
	
	IEnumerator SelectObject (Transform target) {
		bool objectSelected = false;
		
		while (!objectSelected) {
			target = SomeMethodForFindingATarget();
			if(target != null) {
				objectSelected = true;
				Debug.Log("object selected");
			}
			
			yield return null;
		}
	}
	
	Transform SomeMethodForFindingATarget() {
		return null;
		//return a transform
	}
	
	IEnumerator Attack (Transform target) {
		bool attacked = false;
		
		while(!attacked) {
			
			//Attack the targeted object.
			target.SendMessage("Attacked", SendMessageOptions.DontRequireReceiver);
			
			//the last value should always be true.
			//If the attack was successful and the turn will end.
			attacked = true;
			
			yield return null;
		}   
		
	}
	
	
	
	
	
	
}

public struct TurnInfo { //custom Turn info struct.
	//Feel free add or remove any fields.
	
	private float m_DamageDone;
	private string m_MoveUsed;
	private float m_HealthLeft;             
	private bool m_TurnOver;
	
	public float DamageDone
	{
		get {
			return m_DamageDone;
		}
		
		set {
			m_DamageDone = value;
		}
	}
	//How much damage did we do.
	
	public string MoveUsed
	{
		get {
			return m_MoveUsed;
		}
		
		set {
			m_MoveUsed = value;
		}
	}
	//What move did we use.
	
	public float HealthLeft
	{
		get {
			return m_HealthLeft;
		}
		
		set {
			m_HealthLeft = value;
		}
	}
	//How much health do we have left.
	
	public bool TurnOver
	{
		get {
			return m_TurnOver;
			//read only
		}
	}
	//Should the turn end?
	
	//Constructor.
	public TurnInfo(float damage, string moveUsed, float healthLeft, bool turnOver) {   
		m_DamageDone = damage;
		m_MoveUsed = moveUsed;
		m_HealthLeft = healthLeft;
		m_TurnOver = turnOver;
