using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_ATK : MonoBehaviour {

	Animator anim;
	Rigidbody2D rigid;
	Vector2 current;
	Vector2 currentTarget;
	public Transform target;
	public Button ATK;
	float time;
	float time2;
	bool slap;
	bool back;
	bool prevState;
	GameObject connect;
	Component getDam;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody2D> ();
		current = transform.position;
		currentTarget = target.position;
		time2 = 1f;
		time = 1.2f;
		slap = false;
		back = false;
		anim.SetBool ("IDLE", false);

	
	}
	// Reset the values when the turn is ended
	void EndAttack() {

		time2 = 1f;
		time = 1.2f;
		slap = false;
		back = false;
		//rigid.MovePosition (current);


	}

	public void Update () {


		//When the slap animation is activate we start a timer
		if (anim.GetBool ("Slap")) {
			time -= Time.deltaTime;
		}

		//when the time is ended deactive the slap animation and start the next animation (slap)

		if (time < 0 ) {

			anim.SetBool ("Slap", false);
			slap = true;

		}

		//start the walk back to the original position

		if (slap) {

			StartCoroutine(PlayOneShot("WalkR"));
			//anim.SetBool ("WalkR", true);
			rigid.velocity = new Vector3 (current.x, current.y, 0) * Time.deltaTime * 50f;
			//back = true;

		}

		// when we come back to the original position call the comeBack function move to original position and reset according to time2

		if (back) {


			Invoke("comeBack", 1.5f);

			if (time2 <= 0 ){


				EndAttack();

			}

		}



	}

	
	//move the character back to its original position
	public void comeBack() {


		if (transform.position.x >= current.x) {
			rigid.velocity = new Vector3 (0, 0, 0) * 0;
			anim.SetBool ("WalkR", false);
			anim.SetBool ("IDLE", true);
			slap = false;
			time2-=Time.deltaTime;

		}


	}

	
	//attack when the attack button was clicked
	public void Attack (bool ATK) {


		if (ATK) {

			StartCoroutine(PlayOneShot("WalkL"));
			//anim.SetBool ("WalkL", true);
			rigid.velocity = new Vector3 (target.position.x, target.position.y, 0) * Time.deltaTime * 50f;
			anim.SetBool ("IDLE", false);
			time2 = 0.1f;
			time = 1.2f;


		}

	}
	
	
	//activate the attack animation when it hit the object, only work if the attack button was pressed
	public void OnCollisionEnter2D (Collision2D col) {

		if (anim.GetBool("WalkL")) {
			anim.SetBool ("WalkL", false);
			StopCoroutine(PlayOneShot("WalkL"));
			StartCoroutine(PlayOneShot("Slap"));
			//anim.SetBool ("Slap", true);
			time = 1.2f;
			Vector3 v = rigid.velocity;
			v.y = 0.0f;
			rigid.velocity = v;
			back = true;


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
