using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_BAT_GetDamage : MonoBehaviour {

	public Scrollbar HP;
	Rigidbody2D rigid;
	float time;
	float time2;
	float time3;
	Vector2 startPOS;
	bool startTime;
	bool startImmune;
	float damage;
	float timeImmune;
	Animator anim;
	bool End = true;
	public Button ATK;
	bool canDamage;
	bool enableReset;
	bool Attacking;

	// Use this for initialization
	void Start () {

		rigid = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		startPOS = transform.position;
		startTime = false;
		time = 1f;
		time2 = 1f;
		time3 = 1.2f;
		canDamage = true;
		bool enableReset = false;
		Attacking = false;

	
	}

	void FixedUpdate () {

		//start the animation when the delay is over
		if (time < 0) {

			rigid.velocity = new Vector3 (1f, 0, 0) * Time.deltaTime * 20f;

			if (transform.position.x > 1.75f && End ) {

				rigid.velocity = new Vector3(0,0,0);
			}
			
		}


		if (time < -3f && End) {

				Reset();

			}

		if (startTime) {

			time -= Time.deltaTime;

		}

		// if character is dead

		if (HP.size == 0) {
			time2 -= Time.deltaTime;
			//anim.SetBool("Dead", true);
			StartCoroutine(PlayOneShot("Dead"));


			if (time2 < 0) {

				End = false;
				rigid.velocity = new Vector3(transform.position.x, transform.position.y, 0) * 0;
				anim.enabled = false;
				Time.timeScale = 0;

			}

		}

		if (enableReset) {

			time3 -= Time.deltaTime;
			rigid.velocity = new Vector3 (-startPOS.x, 0, 0) * time3;


		}

		if (time3 <= 0 && enableReset) {

			enableReset = false;
			rigid.velocity = new Vector3 (0,0,0) * 0;
			time3 = 0;

		}



	
	}

	//move character back to the original position

	void Reset() {

		time = 1f;
		enableReset = true;
		startTime = false;
		time2 = 1f;
		time3 = 1.2f;
		Attacking = false;

	}

	

	public void Damage(float dam) {

		this.damage = dam;
	}




	void OnCollisionEnter2D (Collision2D col) {

		if (!GetComponent<Player_ATK>().Attacking) {

			startTime = true;
			HP.size -= 25 / 100f;


		}


	}

	public IEnumerator PlayOneShot ( string paramName ) {
				
				{
					anim.SetBool (paramName, true);
					yield return null;
					anim.SetBool (paramName, false);
				}
				
			}
}
