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
		startTime = false;
		time = 1f;
		time2 = 1.4f;
		canDamage = true;

	
	}

	void FixedUpdate () {


		if (time < 0) {

			rigid.velocity = new Vector3 (1f, 0, 0) * Time.deltaTime * 10f;
			print ("WHY");

			if (transform.position.x > 1.75f && End ) {

				rigid.velocity = new Vector3(0,0,0);
			}
			
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

				End = false;
				rigid.velocity = new Vector3(transform.position.x, transform.position.y, 0) * 0;
				anim.enabled = false;
				Time.timeScale = 0;

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




	void OnCollisionEnter2D (Collision2D col) {


			startTime = true;
			HP.size -= 50 / 100f;
			GetComponent<Player_ATK> ().enabled = false;
			print ("canDam");




	}
}
