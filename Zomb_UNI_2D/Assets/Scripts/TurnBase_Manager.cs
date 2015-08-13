using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnBase_Manager : MonoBehaviour {

	public event TurnEnded enemyTurnEnded;
	public event TurnEnded playerTurnEnded;
	public delegate void TurnEnded(TurnInfo tI);
	public Scrollbar PlayerTime;
	public Scrollbar EnemyTime;
	public Scrollbar PlayerHP;
	public Scrollbar EnemyHP;
	bool pATK;
	bool isCollided = false;
	public GameObject Enemy;
	public GameObject Player;
	Rigidbody2D rigid;
	Animator anim;
	Vector2 playTrans ;


	// Use this for initialization
	void Start () {

		EnemyTime.size = 0;
		PlayerTime.size = 0;

	}
	
	void Update () {


		if (PlayerTime.size == 1) {
			StartCoroutine (PlayerTurn ());
		} else if (EnemyTime.size == 1) {
			StartCoroutine (EnemyTurn ());
			
		} else {

			PlayerTime.size += 0.25f * Time.deltaTime;
			EnemyTime.size += 0.25f * Time.deltaTime;

		}
	}
	// Update is called once per frame
	
	public void Attack1 (bool ATK) {
		if (PlayerTime.size == 1) {
			pATK = ATK;
		}
	}
	
	IEnumerator PlayerTurn() {

		Collider2D pcol = Player.GetComponent<Collider2D> ();
		Collider2D ecol = Enemy.GetComponent<Collider2D> ();
		anim = Player.GetComponent<Animator> ();
		rigid = Player.GetComponent<Rigidbody2D> ();
		TurnInfo tI = new TurnInfo();
		//create a new turn info tracker.
		if (pATK) {
			yield return StartCoroutine (PATK ());
		} 

		if (pcol.IsTouching(ecol)) {

			StopCoroutine(PlayOneShot("WalkL"));
			StartCoroutine(PlayOneShot("Slap"));
			yield return StartCoroutine(PCB());
			
		}

		if (Player.transform.position.x >= 1.1f) {

	
			StartCoroutine(PlayOneShot("IDLE"));
			rigid.velocity = new Vector3 (0,0,0) * 0;
			tI.DamageDone = 25;
			//playerTurnEnded (tI);
			PlayerTime.size = 0;
			print (tI.DamageDone);
		}
		//Debug.Log("Attacking");

	}
	
	IEnumerator EnemyTurn() {


		TurnInfo tI = new TurnInfo();
		//create a new turn info tracker.
		//yield return StartCoroutine(Enem_ATK.EATK());
		Debug.Log("Attacked1");
		if(enemyTurnEnded != null) 
			playerTurnEnded(tI);
		yield return null;
		
	}

	IEnumerator PATK () {

		anim = Player.GetComponent<Animator> ();
		rigid = Player.GetComponent<Rigidbody2D> ();
		StartCoroutine(PlayOneShot("WalkL"));
		rigid.velocity = new Vector3 (Enemy.transform.position.x, Enemy.transform.position.y, 0) * Time.deltaTime * 50f;
		anim.SetBool ("IDLE", false);
		pATK = false;
		return null;
	}

	IEnumerator PCB () {
		int i = 0 ;
		playTrans = Player.transform.position;
		rigid = Player.GetComponent<Rigidbody2D> ();
		yield return new WaitForSeconds (1f);
		yield return StartCoroutine(PlayOneShot ("WalkR"));
		rigid.velocity = new Vector3 (-playTrans.x, 0, 0) * Time.deltaTime * 50f; 
		yield return null;
	
	}




	
	public IEnumerator PlayOneShot ( string paramName ) {
		
		{
			anim.SetBool (paramName, true);
			yield return null;
			anim.SetBool (paramName, false);
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

	}
	}
}

