using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enem_ATK : MonoBehaviour {

	Animator anim;
	Rigidbody2D rigid;
	Vector2 startPOS;
	Vector2 TargetPOS;
	public Transform target;
	float time;
	float time2;
	float time3;
	bool slap;
	bool back;
	bool back2;
	bool start;
	bool barActive;
	public GameObject connect;
	public Scrollbar bar;
	bool ATK;
	public Button ATK2;

	
	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody2D> ();

		startPOS = transform.position;
		TargetPOS = target.position;
		time2 = 1f;
		time = 4f;
		time3 = 2f;
		slap = false;
		back = false;
		anim.SetBool ("IDLE", false);
		bar.size = 0;
		ATK = false;
		start = true;
		back2 = false;
		barActive = true;
	
	}

	void Reset() {

		time2 = 1f;
		time = 4f;
		time3 = 2f;
		slap = false;
		back = false;
		anim.SetBool ("IDLE", false);
		bar.size = 0;
		ATK = false;
		start = true;
		back2 = false;
		barActive = true;
		connect.GetComponent<Player_ATK>().enabled = true;
		GetComponent<Enem_BAT_GetDamage>().enabled = true;


	}

	public void Attack (bool ATK2 ) {
		
		
		if (ATK2) {
			
			barActive = false;

		}
		
	}

	void Update() {
	
		if (barActive) { 
			bar.size += Time.deltaTime * 0.25f;
		}

		if (bar.size == 1) {
			
			ATK = true;
			
		}

		if (ATK) {

			bar.size = 0;

		}

		if (ATK) {


			anim.SetBool("WalkR", true);
			anim.SetBool("IDLE", false);
			bar.size = 0;
			barActive = false;
			time -= Time.deltaTime;
			if (time < 3.1) {
			rigid.velocity = new Vector3(TargetPOS.x, TargetPOS.y, 0) * Time.deltaTime * 50f;
			}
			connect.GetComponent<Player_ATK>().enabled = false;
			GetComponent<Enem_BAT_GetDamage>().enabled = false;

		}


		if (anim.GetBool ("Slash")) {
			
			time2 -= Time.deltaTime;
			
		}

		if (time3 < 0) {

			Reset ();

		}


	}

	public void FixedUpdate () {

		if (time2 < 0) {

			anim.SetBool("Slash", false);
			anim.SetBool("WalkL", true);
			rigid.velocity = new Vector3(startPOS.x, startPOS.y, 0);

		}


		if (transform.position.x <= startPOS.x && back) {

			anim.SetBool("WalkL",false);
			rigid.velocity = new Vector3(0,0,0);
			anim.SetBool("IDLE", true);
			back2 = true;
			time3 -= Time.deltaTime;

		}


	
	}


	

	public void OnCollisionEnter2D (Collision2D col) {


		anim.SetBool ("Slash", true);
		anim.SetBool ("WalkR", false);
		ATK = false;
		Vector3 v = rigid.velocity;
		v.y = 0.0f;
		rigid.velocity = v;
		back = true;

	

	}

}
