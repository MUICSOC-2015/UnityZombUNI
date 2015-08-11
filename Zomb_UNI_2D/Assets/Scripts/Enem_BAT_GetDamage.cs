using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enem_BAT_GetDamage : MonoBehaviour {

	public Scrollbar HP;
	Rigidbody2D rigid;
	float time;
	float time2;
	Vector2 startPOS;
	bool startTime;
	float damage;
	Animator anim;
	bool End = true;

	// Use this for initialization
	void Start () {

		rigid = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		startPOS = transform.position;
		startTime = false;
		time = 1f;
		time2 = 0.5f;

	
	}

	void FixedUpdate () {


		if (time < 0) {

			rigid.velocity = new Vector3 (-1f, 0, 0) * Time.deltaTime * 85;



			if (transform.position.x < -1.75f && End ) {

				rigid.velocity = new Vector3(0,0,0);
			}
			
		}



			if (time < -2.5f && End) {

				Reset();

			}

		if (startTime) {

			time -= Time.deltaTime;

		}
		//when health is zero the character dies
		if (HP.size <= 0) {
			time2 -= Time.deltaTime;
			anim.SetBool("Dead", true);

			if (time2 < 0) {
				anim.SetBool("Dead", false);
				End = false;
				rigid.velocity = new Vector3(transform.position.x, transform.position.y, 0) * 0;

			}

		}
	
	}

	//move the character back to its original position
	void Reset() {

		time = 1f;
		rigid.velocity = new Vector3 (-startPOS.x, 0, 0) * Time.deltaTime * 50; 
		startTime = false;
		time2 = 1f;


	}
	// decrease health according to the button i.e. Attack
	public void Damage(float dam) {

		this.damage = dam;
	}

	// decrease health when we collide
	void OnCollisionEnter2D (Collision2D col) {

		if (!anim.GetBool ("WalkL") && !anim.GetBool ("WalkR") && !anim.GetBool ("Slash")) {

			startTime = true;
			HP.size -= damage / 100f;


		}


	}
}
