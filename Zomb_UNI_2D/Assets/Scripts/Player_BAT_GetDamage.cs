using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_BAT_GetDamage : MonoBehaviour {

	public Scrollbar HP;
	Rigidbody2D rigid;
	float time;
	float time2;
	Vector2 startPOS;
	bool startTime;
	bool startImmune;
	float damage;
	float timeImmune;
	Animator anim;
	bool End = true;
	public Button ATK;
	bool canDamage;

	// Use this for initialization
	void Start () {

		rigid = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		startPOS = transform.position;
		startTime = true;
		time = 1f;
		time2 = 1.4f;
		canDamage = true;
		timeImmune = 6f;

	
	}

	void FixedUpdate () {


		if (time < 0) {

			rigid.velocity = new Vector3 (1f, 0, 0) * Time.deltaTime * 10f;

			if (transform.position.x > 1.75f && End ) {

				rigid.velocity = new Vector3(0,0,0);
			}
			
		}

		if (startImmune) {

			timeImmune -= Time.deltaTime;

		}

			if (time < -2.5f && End) {

				Reset();

			}

		if (startTime) {

			time -= Time.deltaTime;

		}

		if (HP.size == 0) {
			time2 -= Time.deltaTime;
			anim.SetBool("Dead", true);


			if (time2 < 0) {

				//anim.SetBool("Dead", false);
				End = false;
				rigid.velocity = new Vector3(transform.position.x, transform.position.y, 0) * 0;
				anim.enabled = false;

			}

		}
	
	}

	void Reset() {

		time = 1f;
		transform.position = startPOS; 
		print ("Reset");
		startTime = false;
		time2 = 1f;
		GetComponent<Player_ATK>().enabled = true;


	}

	public void Damage(float dam) {

		this.damage = dam;
	}

	public void Attack (bool ATK ) {
		
		
		if (ATK) {

			startImmune = true;
			
		}
		
	}


	void OnCollisionEnter2D (Collision2D col) {

		if (timeImmune >= 4) {

			startTime = true;
			HP.size -= 50 / 100f;
			GetComponent<Player_ATK> ().enabled = false;
			print ("canDam");
			timeImmune = 8f;

		}



	}
}
