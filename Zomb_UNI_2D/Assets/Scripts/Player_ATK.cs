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
	public bool Attacking;
	bool startTime;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody2D> ();
		current = transform.position;
		currentTarget = target.position;
		time2 = 1f;
		time = 2f;
		slap = false;
		back = false;
		anim.SetBool ("IDLE", false);
		startTime = false;

	
	}
	// Reset the values when the turn is ended
	void EndAttack() {

		time2 = 1f;
		time = 2f;
		slap = false;
		back = false;
		Attacking = false;
		startTime = false;
		//rigid.MovePosition (current);


	}

	public void Update () {


		//When the slap animation is activate we start a timer
		if (anim.GetBool ("Slap")) {
			startTime = true;
		}

		if (startTime) {
			time -= Time.deltaTime;
		}
		//when the time is ended deactive the slap animation and start the next animation (slap)

		if (time < 0 ) {

			slap = true;
			StopCoroutine(PlayOneShot("Slap"));

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


			comeBack();
			print (time2);

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
			StartCoroutine(PlayOneShot("IDLE"));
			slap = false;
			time2-=Time.deltaTime;

		}


	}

	
	//attack when the attack button was clicked
	public IEnumerator Attack () {

		print ("HOHO");
		StartCoroutine(PlayOneShot("WalkL"));
		rigid.velocity = new Vector3 (target.position.x, target.position.y, 0) * Time.deltaTime * 50f;
		anim.SetBool ("IDLE", false);
		Attacking = true;
		return null;
		

	}
	
	
	//activate the attack animation when it hit the object, only work if the attack button was pressed
	public void OnCollisionEnter2D (Collision2D col) {

		if (Attacking) {
			anim.SetBool ("WalkL", false);
			StopCoroutine(PlayOneShot("WalkL"));
			StartCoroutine(PlayOneShot("Slap"));
			time = 2f;
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
