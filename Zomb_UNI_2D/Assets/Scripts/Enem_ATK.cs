using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enem_ATK : MonoBehaviour {

	Animator anim;
	Animator conanim;
	Rigidbody2D rigid;
	Vector2 startPOS;
	Vector2 TargetPOS;
	public Transform target;
	//float time;
	float time2;
	float time3;
	float time4;
	bool slap;
	bool back;
	bool back2;
	bool start;
	bool startTime;
	bool barActive;
	public GameObject connect;
	public Scrollbar bar;
	bool ATK;
	public Button ATK2;

	
	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody2D> ();
		conanim = connect.GetComponent<Animator> ();
		startPOS = transform.position;
		TargetPOS = target.position;
		time2 = 1f;
//		time = 4f;
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
//		time = 4f;
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
			time4 = 4f;
			startTime = true;


		}
		
	}

	void Update() {

	
		if (barActive) { 
			bar.size += Time.deltaTime * 0.25f;
		}

		if (startTime) {

			time4 -= Time.deltaTime;

		}

		if (time4 <= 0 && !ATK) {

			barActive = true;

		}


		if (bar.size == 1) {
			
			ATK = true;
			
		}

		if (ATK) {

			bar.size = 0;

		}

		if (ATK) {

			StartCoroutine(PlayOneShot("WalkR"));
			anim.SetBool("IDLE", false);
			bar.size = 0;
			barActive = false;
			rigid.velocity = new Vector3(TargetPOS.x, TargetPOS.y, 0) * Time.deltaTime * 50f;

		}


		if (back) {
			
			time2 -= Time.deltaTime;
			barActive = false;
			
		}

		if (time3 < 0) {

			Reset ();

		}


	}

	public void FixedUpdate () {

		if (time2 < 0) {

			//anim.SetBool("Slash", false);
			StopCoroutine(PlayOneShot("Slash"));
			StartCoroutine(PlayOneShot("WalkL"));
			//anim.SetBool("WalkL", true);
			rigid.velocity = new Vector3(startPOS.x, startPOS.y, 0);
			barActive = false;
		}


		if (transform.position.x <= startPOS.x && back) {

			anim.SetBool("WalkL",false);
			rigid.velocity = new Vector3(0,0,0);
			anim.SetBool("IDLE", true);
			back2 = true;
			time3 -= Time.deltaTime;
			barActive = false;

		}


	
	}


	

	public void OnCollisionEnter2D (Collision2D col) {

		if (ATK) {
			//anim.SetBool ("Slash", true);
			StartCoroutine(PlayOneShot("Slash"));
			anim.SetBool ("WalkR", false);
			ATK = false;
			Vector3 v = rigid.velocity;
			v.y = 0.0f;
			rigid.velocity = v;
			back = true;

		}

	}


		public IEnumerator PlayOneShot ( string paramName ) {

		{
			anim.SetBool( paramName, true );
			yield return null;
			anim.SetBool( paramName, false );
		}


	

	}

}
